using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon: Equipment
{
    /**
    +-----+-----+-----+-----+-----+-----+-----+-----+-----+
    |  HP | ATK | DEF | POT | RES | CRI | CDMG|SPGAI| SP  |
    +-----+-----+-----+-----+-----+-----+-----+-----+-----+
    **/
    public const int HP = 0,ATK = 1,DEF = 2,POT = 3, RES = 4, CRI = 5, CDMG = 6;
    public enum WeaponType{Dagger, Mace, Lance, Longsword, Catalyst};
    public enum Series{Starter, Bronze};
    public WeaponType wpType;
    public Series weaponSeries;
    public int baseATK = 0;//Has Main and SubStat

    // public int subStatID = 0;
    // public float subStatVal = 0f;// Index 0 is main stat
    public StatBlock subStat;
    // 0 = HP 1 = ATK 2 = DEF 3 = Potency 4 = Resistance 5 = C. Rate 6 = C. DMG 7 = SP Gain
    public string weaponSkill;
    public float upgrades = 0f,subStatUpgrade = 0f;
    public GameObject weaponMesh;
    

    public int weaponTypeInt
    {
        get
        {

            return (int)wpType;
        }
    }

    public Weapon(Weapon weapon)
    {
        // Items
        grade = weapon.grade;
        itemName = weapon.itemName;
        icon = weapon.icon;

        // Equipment
        level = weapon.level;
        active = weapon.active;
        enhancelevel = weapon.enhancelevel;
        totalXP = weapon.totalXP;
        slot = Piece.Weapon;
        guid = Guid.NewGuid().ToString();

        // Weapon
        upgrades = weapon.upgrades;
        baseATK = weapon.baseATK;
        subStat.statType = weapon.subStat.statType;
        subStat.value = weapon.subStat.value;
        wpType = weapon.wpType;
        weaponSeries = weapon.weaponSeries;
        weaponMesh = weapon.weaponMesh;

        if (EquipmentSprites.instance != null)
        {
            EquipmentSprites equipSprite = EquipmentSprites.instance;
            WeaponSkill wpSkill = equipSprite.GetWeaponSkill(wpType,weaponSeries);

            weaponSkill = wpSkill.weaponSkillDesc;
            description = wpSkill.weaponLore;
        }

    }


    public Weapon(PlayerWeaponData pwd)
    {
        // Items
        rarity = pwd.grade;
        itemName = pwd.weaponName;

        // Equipment
        level = pwd.level;
        active = (Trigger)pwd.triggerInt;
        enhancelevel = pwd.enhancelevel;
        totalXP = pwd.totalXP;
        slot = Piece.Weapon;
        guid = pwd.guid;
  
        // Weapon
        upgrades = pwd.upgrades;
        baseATK = pwd.baseATK;
        subStat.statType = (EnumLib.Stats)pwd.subStatID;
        subStat.value = pwd.subStatVal;
        subStatUpgrade = pwd.subStatUpgrade;
        wpType = (WeaponType)pwd.wpType;
        weaponSeries = (Series)pwd.weaponSeries;

        if (EquipmentSprites.instance != null)
        {
            EquipmentSprites equipSprite = EquipmentSprites.instance;
            WeaponSkill wpSkill = equipSprite.GetWeaponSkill(wpType,weaponSeries);

            weaponMesh = equipSprite.GetWeaponMesh(wpType,weaponSeries);
            icon = equipSprite.GetWeaponIcon(wpType.ToString(),weaponSeries.ToString());

            weaponSkill = wpSkill.weaponSkillDesc;
            description = wpSkill.weaponLore;
        }
    }
    

}