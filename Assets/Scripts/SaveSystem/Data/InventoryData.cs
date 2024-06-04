using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InventoryData
{
    public List<PlayerArmorData> armors = new List<PlayerArmorData>();
    public List<PlayerWeaponData> weapons = new List<PlayerWeaponData>();
    
    public List<RPGMatData> rpgMaterials = new List<RPGMatData>();
    public List<PlayerArmorData> listOfArmors
    {
        get{return armors;}
    }

    public InventoryData()
    {
        
    }

    public void AddArmor(Armor a)
    {
        Debug.Log("Added "+a.itemName);
    
        armors.Add(new PlayerArmorData(a));
    }

    public void AddWeapon(Weapon w)
    {
        Debug.Log("Added "+w.itemName);
    
        weapons.Add(new PlayerWeaponData(w));
    }

    public void AddRPGMat(RPGMaterial r)
    {
        rpgMaterials.Add(new RPGMatData(r));
    }
}