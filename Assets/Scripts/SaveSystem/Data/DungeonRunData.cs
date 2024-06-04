using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class DungeonRunData
{
    //public int[] levels;
    public List<PlayerArmorData> armors = new List<PlayerArmorData>();
    public List<RPGMatData> rpgMats = new();

    public List<PlayerArmorData> listOfArmors
    {
        get{return armors;}
    }

    public DungeonRunData()
    {
        //levels = new int[6];
        
    }

    public void AddArmor(Armor a)
    {
        Debug.Log("Added "+a.name);
        armors.Add(new PlayerArmorData(a));
    }

    public void AddRPGMat(RPGMaterial r)
    {
        rpgMats.Add(new RPGMatData(r));
    }
}