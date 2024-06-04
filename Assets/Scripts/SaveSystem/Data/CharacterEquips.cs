using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterEquips : MonoBehaviour
{
    [SerializeField]private Armor[] equip = new Armor[6];
    [SerializeField]private Weapon activeWeapon;
    [SerializeField]Weapon.WeaponType weaponClass;
    public bool[] triggers = new bool[Enum.GetNames(typeof(Equipment.Trigger)).Length];
    public float[] totalStats;
    public float flatHP = 0f, flatATK = 0f, flatDEF = 0f;

    public Armor[] armors
    {
        get{return equip;}
    }

    public Weapon a_Weapon
    {
        get{return activeWeapon;}
    }

    public Armor EquipPiece(int i)
    {
        return equip[i];
    }

    public void ProcessStats(Armor a)
    {
        if (a.pieceInt == 0)
        {
            flatHP = a.subStats[0].value;
        }
        else if (a.pieceInt == 1)
        {
            flatDEF = a.subStats[0].value;
        }
        else
        {
            if ((int)a.subStats[0].statType < totalStats.Length)
            {
                totalStats[(int)a.subStats[0].statType] += a.subStats[0].value;
            }
        }

        for (int i = 1; i < a.subStats.Count; i++)
        {
            if ((int)a.subStats[i].statType < totalStats.Length)
            {
                totalStats[(int)a.subStats[i].statType] += a.subStats[i].value;
            }
        }
    }

    public void SwapArmor(Armor armor, int i)
    {
        equip[i] = armor;
        ReProcessStats();
    }

    void ReProcessStats()
    {
        Debug.Log("Reprocessed stats");
        Array.Clear(totalStats,0, totalStats.Length);
        flatHP = 0f;
        flatATK = 0f;
        flatDEF = 0f;
        foreach(Armor armor in equip)
        {
            if (armor != null)
                ProcessStats(armor);
        }
    }


    public void FillLoadOut(PlayerEquipData ped)
    {
        totalStats = new float[8];
        for (int i = 0; i < 6; i++)
        {
            if (ped.armorLoadOut[i] != null && string.Compare(ped.armorLoadOut[i].itemName,"") != 0)
            {
                equip[i] = new Armor(ped.armorLoadOut[i],null);
                equip[i].icon = EquipmentSprites.instance.GetArmorIcon(equip[i].set.ToString(),equip[i].slot.ToString());
                ProcessStats(equip[i]);
            }
        }

        if (ped.weaponLoadOut != null)
            activeWeapon = new Weapon(ped.weaponLoadOut);
    }

    public CharacterEquips(PlayerEquipData ped)
    {
        
    }
}
