using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SingleEquipMenu : MonoBehaviour
{
    [SerializeField]GameObject miniGearCard, armorList;
    [SerializeField]GameObject[] menuItems;
    const int MINIGEAR = 0,CURRENT = 1, NEW = 2;
    List<string> currentArmors = new List<string>();
    Inventory inventory;
    public static Action<Armor,string> displayArmor;
    string swapArmor = null;
    [SerializeField]int maxMiniCard = 0;
    int currentListing = -1;
    Armor activePiece = null;
    // Start is called before the first frame update

    public void SetCurrentListing(int i)
    {
        currentListing = i;
    }

    void Initialize()
    {
        Debug.Log("Initializing singleEquip");
        inventory = PauseManager.inventory;

        int n = 3, gearCardIndex = 1;
        menuItems = new GameObject[n];
        for (int i = 0; i < n; i++)
        {
            menuItems[i] = transform.GetChild(i).GetChild(gearCardIndex).gameObject;
        }

        inventory = PauseManager.inventory;
        maxMiniCard = inventory.maxSize;
        GameObject temp;
        for (int i = 0; i < maxMiniCard; i++)
        {
            temp = Instantiate(miniGearCard);
            temp.transform.SetParent(armorList.transform);
            temp.transform.localScale = new Vector3(0.5f,0.5f,1f);
            temp.GetComponent<MiniGearCard>().GetPieces();
            temp.SetActive(false);
        }
        displayArmor = SetNewCard;
    }

    public void SetNewCard(Armor armor,string activeArmor)
    {
        if (armor == null)
            return;
        swapArmor = activeArmor;
        menuItems[NEW].gameObject.SetActive(true);
        menuItems[NEW].GetComponent<GearCard>().UpdateCard((Item)armor);
        
    }

    public void EquipGear()
    {
        Armor armor = PauseManager.inventory.SwapArmor(activePiece,swapArmor);//activePiece is current piece, swapArmor is armor in inventory
        PauseManager.activeEquip.SwapArmor(armor,currentListing);
        (EquipmentMenu.returntoMain)?.Invoke();
    }

    public void UnEquipGear()
    {
        activePiece = PauseManager.activeEquip.EquipPiece(currentListing);// Get currently equipped piece
        bool successfulSwap = PauseManager.inventory.AddUnequipped(activePiece);// Attempt to put it in inventory
       // check to see if armor was put in
        if (successfulSwap)// If there is sufficient inventory space, unequip
        {
            PauseManager.activeEquip.SwapArmor(null,currentListing);
            (EquipmentMenu.returntoMain)?.Invoke();
        }
        else
            Debug.Log("Couldn't unequip");
    }

    public void InitializeMenu(Equipment.Piece category)
    {
        if (inventory == null)
            Initialize();

        currentListing = (int)category;
        currentArmors = inventory.ReturnArmorList(category);
        Armor armor;
        GameObject temp;
        string id;


        for(int i = 0; i < maxMiniCard; i++)
        {
            temp = armorList.transform.GetChild(i).gameObject;
            if (i >= currentArmors.Count)
            {
                if (temp.activeSelf)
                {
                    temp.SetActive(false);
                    continue;
                }
                else
                {
                    break;
                }
            }

            id = currentArmors[i];
            armor = inventory.GetArmor(id);
            temp.SetActive(true);
            armorList.transform.GetChild(i).GetComponent<MiniGearCard>().Initialize(armor,id);
        }


        activePiece = PauseManager.activeEquip.EquipPiece((int)category);
        
        if (activePiece == null)
        {
            Debug.Log("ActivePiece is empty");
            menuItems[CURRENT].gameObject.SetActive(false);
        }
        else
        {
            menuItems[CURRENT].gameObject.SetActive(true);
            menuItems[CURRENT].GetComponent<GearCard>().UpdateCard((Item)activePiece);
        }

        menuItems[NEW].gameObject.SetActive(false);


    }

    
}
