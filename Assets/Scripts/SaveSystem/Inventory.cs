using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour, ISaveEquipment
{
    [SerializeField]private Dictionary<string,Item> _inventory = new Dictionary<string,Item>();

    private List<string> _helmet = new List<string>();
    private List<string> _chestplate = new List<string>();
    private List<string> _boot = new List<string>();
    private List<string> _necklace = new List<string>();
    private List<string> _ring = new List<string>();
    private List<string> _belt = new List<string>();
    private List<string> _daggers = new List<string>();

    public static Func<Item,bool> appendToInventory{get;private set;}
    public int maxSize = 20;
    private InventoryDisplay _inventoryDisplay;
    private EquipmentSprites _equipSprites;
    private ItemIconManager _itemIcons;

    public int currentSize
    {
        get{return _inventory.Count;}
    }

    public EquipmentSprites equipSprite
    {
        get{return _equipSprites;}
    }

    public int ReturnStackCount(RPGMaterial r)
    {
        if (_inventory.ContainsKey(r.itemName))
        {
            return (_inventory[r.itemName] as RPGMaterial).stackCount;
        }
        return 0;
    }

    public Armor GetArmor(string key)
    {
        if (_inventory.ContainsKey(key))
        {
            return (Armor)_inventory[key];
        }

        return null;
    }

    public bool isFull
    {
        get{return _inventory.Count >= maxSize;}
    }

    public List<string> ReturnArmorList(Equipment.Piece index)
    {
        switch (index)
        {
            case Equipment.Piece.Helmet:
                return _helmet;
            case Equipment.Piece.Chestplate:
                return _chestplate;
            case Equipment.Piece.Boots:
                return _boot;
            case Equipment.Piece.Necklace:
                return _necklace;
            case Equipment.Piece.Ring:
                return _ring;
            case Equipment.Piece.Belt:
                return _belt;
            default:
                return _daggers;
        }
    }

    public Dictionary<string,Item> stock
    {
        get{return _inventory;}
    }

    // Start is called before the first frame update
    void Start()
    {
        _inventoryDisplay = GetComponent<InventoryDisplay>();
        _equipSprites = GetComponent<EquipmentSprites>();
        _itemIcons = GetComponent<ItemIconManager>();
        appendToInventory = AddtoInventory;
    }

    // Adds the guid to list of armors to store keys of gear that exists in inventory.
    public void AddtoArmorCategory(Armor armor)
    {
        if (armor == null || armor.guid == null)
        {
            Debug.Log("Armor is null");
            return;
        }
        Equipment.Piece category = armor.slot;

        switch(category)
        {
            case Equipment.Piece.Helmet:
                if (!_helmet.Contains(armor.guid))
                    _helmet.Add(armor.guid);
            break;

            case Equipment.Piece.Chestplate:
                if (!_chestplate.Contains(armor.guid))
                    _chestplate.Add(armor.guid);
            break;

            case Equipment.Piece.Boots:
                if (!_boot.Contains(armor.guid))
                    _boot.Add(armor.guid);
            break;

            case Equipment.Piece.Necklace:
                if (!_necklace.Contains(armor.guid))
                    _necklace.Add(armor.guid);
            break;

            case Equipment.Piece.Ring:
                if (!_ring.Contains(armor.guid))
                    _ring.Add(armor.guid);
            break;

            default:
                if (!_belt.Contains(armor.guid))
                    _belt.Add(armor.guid);
            break;
        }
    }

    // Implemented from ISaveEquipment interface
    public void LoadData(InventoryData data)
    {
        Armor armor;
        Weapon weapon;
        Sprite icon;

        Debug.Log("Loading data");
        // if (data.listOfArmors.Count < 1)
        // {
        //     this.gameObject.SetActive(false);
        //     return;
        // }

        _helmet = new List<string>();
        _chestplate = new List<string>();
        _boot = new List<string>();
        _necklace = new List<string>();
        _ring = new List<string>();
        _belt = new List<string>();
        
        foreach(PlayerArmorData armorData in data.listOfArmors)
        {
            icon = _equipSprites.GetArmorIcon(((Armor.Sets)armorData.setid).ToString(),((Equipment.Piece)armorData.slot).ToString());
            armor = new Armor(armorData,icon);

            _inventory.Add(armor.guid,armor);
            AddtoArmorCategory(armor);
        }

        foreach(PlayerWeaponData w in data.weapons)
        {
            weapon = new Weapon(w);
            _daggers.Add(weapon.guid);
            _inventory.Add(weapon.guid,weapon);
        }
        
        string origin;
        foreach(RPGMatData r in data.rpgMaterials)
        {
            Debug.Log("Adding in "+r.itemName);
            origin = ((EnumLib.Nation)(r.origin)).ToString();
            icon = _itemIcons.ReturnItemIcon(origin, r.itemName);
            _inventory.Add(r.itemName,new RPGMaterial(r,icon));
        }
        Start();
        _inventoryDisplay.Reload();
        this.gameObject.SetActive(false);
    }

    // Implemented from ISaveEquipment interface
    public void SaveData(ref InventoryData data)
    {
        Armor armor = null;
        Weapon weapon = null;
        RPGMaterial material = null;
        
        data.listOfArmors.Clear();
        data.weapons.Clear();
        data.rpgMaterials.Clear();

        foreach(KeyValuePair<string, Item> kvp in _inventory)
        {
            
            if (kvp.Value is Armor)
            {
                armor = (Armor)kvp.Value;
                data.AddArmor(armor);
                    
            }
            else if (kvp.Value is Weapon)
            {
                weapon = (Weapon)kvp.Value;
                data.AddWeapon(weapon);
            }
            else if (kvp.Value is RPGMaterial)
            {
                material = (RPGMaterial) kvp.Value;
                data.AddRPGMat(material);
            }
            
        }
    }

    // This method exclusively puts the armor in the inventory without taking one out.
    // As such the inventory spaces used will increment or at least attempt to.
    public bool AddUnequipped(Armor swappedOut)
    {
        if (swappedOut != null && !isFull)
        {
            Debug.Log("Successfully unequipped");
            _inventory.Add(swappedOut.guid,swappedOut);
            AddtoArmorCategory(swappedOut);
            _inventoryDisplay.Reload();
            return true;
        }
        else if (isFull)
        {
            Debug.Log("Cannot unequip, inventory full");
        }

        return false;
    }

    // This one will switch the armor from the unequipped to the armor to be equipped
    // Resulting in a net neutral of inventory spaces used. As such can be called safely
    // even with full inventory.
    public Armor SwapArmor(Armor swappedOut, string swapped)
    {
        Armor newArmor = null;
        // Check if armor being called can be swapped
        if (_inventory.ContainsKey(swapped))
        {
            newArmor = (Armor)_inventory[swapped];//Take out the piece to be swapped
            _inventory.Remove(swapped);

            if (swappedOut != null)//if equipped slot isn't empty add it to the inventory
            {
                _inventory.Add(swappedOut.guid,swappedOut);
                AddtoArmorCategory(swappedOut);
            }

            _inventoryDisplay.Reload();
        }

        // If the equipped slot isn't empty, put it in the inventory
            
        return newArmor;
    }

    // This will add the item to the inventory and update the display based on item type
    public bool AddtoInventory(Item item)
    {
        if (isFull)// if inventory is full
            return false;

        Debug.Log("Inserting.."+item.name);
        
        if (item is RPGMaterial)// Used for stackable materials
        {
            RPGMaterial rp;

            if (_inventory.ContainsKey(item.name))
            {
                Debug.Log("Dupe detected");
                rp = (RPGMaterial)_inventory[item.name];
                rp.stackCount++;
            }
            else
            {
                rp = (RPGMaterial)item;
                rp.stackCount++;
                _inventory.Add(item.itemName,item);
            }
        }
        else if (item is Armor)
        {
            Armor a = (Armor) item;
            _inventory.Add(a.guid,a);
        }
        else if (item is Weapon)
        {
            Weapon w = (Weapon) item;
            _inventory.Add(w.guid,w);
        }

        _inventoryDisplay.Reload();
        return true;
    }
}
