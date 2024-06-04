using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerArmorData
{
    public string itemName;
    public float[] statval;// Index 0 is main stat
    // 0 = HP 1 = ATK 2 = DEF 3 = Potency 4 = Resistance 5 = C. Rate 6 = C. DMG 7 = SP Gain 8 = AetherPower
    public int[] stat;
    public int[] statrarity;//Follows similar grades
    public float[] upgrades;
    public int setid = 0;
    public int slot;//0 = Helmet 1 = Chestplate 2 = Boots 3 = Necklace 4 = Ring 5 = Belt 6 = Weapon
    public int level = 1;
    public int trigger = 0;
    public int grade = 0;

    public int enhancelevel;
    public int totalXP;
    public string guid;

    public PlayerArmorData(Armor a)
    {
        //Debug.Log("Transcribing armor index "+a.setid+" from "+a.name);
        setid = a.setid;
        slot = a.pieceInt;
        level = a.level;
        grade = a.rarity;
        itemName = a.itemName;
        
        trigger = a.triggerInt;
        statval = new float[a.subStats.Count];
        stat = new int[a.subStats.Count];
        statrarity = new int[a.subStats.Count];

        for(int i = 0; i < a.subStats.Count; i++)
        {
            statval[i] = a.subStats[i].value;
            stat[i] = (int)a.subStats[i].statType;
            statrarity[i] = (int)a.subStats[i].grade;
        }

        // statval = a.statval;
        // stat = a.stat;
        // statrarity = a.statrarity;
        upgrades = a.upgrades;
        enhancelevel = a.enhancelevel;
        totalXP = a.totalXP;
        guid = a.guid;

    }
}
