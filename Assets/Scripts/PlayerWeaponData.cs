using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerWeaponData
{
    //public enum WeaponType{Dagger, Mace, Lance, Longsword, Catalyst};
    public const int HP = 0,ATK = 1,DEF = 2,POT = 3, RES = 4, CRI = 5, CDMG = 6;
    // Items
    public int grade;
    public string weaponName;

    //Equipment
    public int level, triggerInt, enhancelevel, totalXP;
    public string guid;

    //Weapon
    public int wpType,weaponSeries,baseATK,subStatID;
    public float subStatVal,upgrades,subStatUpgrade;

    public PlayerWeaponData(Weapon a)
    {
        // Items
        grade = a.rarity;
        weaponName = a.itemName;
        
        //Equipment
        level = a.level;
        triggerInt = a.triggerInt;
        enhancelevel = a.enhancelevel;
        totalXP = a.totalXP;
        guid = a.guid;

        // Weapon
        upgrades = a.upgrades;
        baseATK = a.baseATK;
        subStatID = (int)a.subStat.statType;
        subStatVal = a.subStat.value;
        subStatUpgrade = a.upgrades;
        wpType = a.weaponTypeInt;
        weaponSeries = (int)a.weaponSeries;
    }
}
