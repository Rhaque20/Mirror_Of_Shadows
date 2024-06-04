using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Armor", menuName = "Armor")]
public class Armor: Equipment
{
    public enum Sets{BrightStar,DarkStar};
    // public float[] statval = new float[1];// Index 0 is main stat
    // // 0 = HP 1 = ATK 2 = DEF 3 = Potency 4 = Resistance 5 = C. Rate 6 = C. DMG 7 = SP Gain 8 = AetherPower
    // public int[] stat = new int[1];
    // public int[] statrarity = new int[1];//Follows similar grades
    public List<StatBlock> subStats = new List<StatBlock>();
    public float[] upgrades; // Keep track of the increments for each substat
    //public int setid = 0;
    public Sets set;

    public int setid
    {
        get
        {
            return (int)set;
        }
    }

    public int pieceInt
    {
        get
        {
            switch(slot)
            {
                case Piece.Chestplate:
                    return 1;
                case Piece.Boots:
                    return 2;
                case Piece.Necklace:
                    return 3;
                case Piece.Ring:
                    return 4;
                case Piece.Belt:
                    return 5;
                default:
                    return 0;
            }
        }
        set
        {
            switch(value)
            {
                case 1:
                    slot = Piece.Chestplate;
                    break;
                case 2:
                    slot = Piece.Boots;
                    break;
                case 3:
                    slot = Piece.Necklace;
                    break;
                case 4:
                    slot = Piece.Ring;
                    break;
                case 5:
                    slot = Piece.Belt;
                    break;
                default:
                    slot = Piece.Helmet;
                    break;
            }
        }
    }

    public float totalStatVal(int statIndex)
    {
        if (statIndex >= subStats.Count)
            return -1f;
        
        return subStats[statIndex].value + upgrades[statIndex];
    }


    public Armor(Piece piece, Grade rarity, float[] statValues, int[] stats, int[] statRarity, float[] statUpgrades, string name, int gearLevel,Sets setname, Sprite armorIcon)
    {
        slot = piece;
        grade = rarity;
        // statval = statValues;
        // stat = stats;
        // statrarity = statRarity;
        for(int i = 0; i < stats.Length; i++)
        {
            subStats.Add(new StatBlock((EnumLib.Stats)stats[i],statValues[i],statRarity[i]));
        }
        upgrades = statUpgrades;
        itemName = name;
        level = gearLevel;
        set = setname;
        totalXP = 0;
        enhancelevel = 0;
        set = setname;
        guid = Guid.NewGuid().ToString();
        icon = armorIcon;
    }

    public Armor(PlayerArmorData pad, Sprite armorIcon)
    {
        slot = (Piece)pad.slot;
        grade = (Grade)pad.grade;
        for (int i = 0 ; i < pad.statval.Length;i++)
        {
            subStats.Add(new StatBlock((EnumLib.Stats)pad.stat[i],pad.statval[i],pad.statrarity[i]));
        }
        // statval = pad.statval;
        // stat = pad.stat;
        // statrarity = pad.statrarity;
        upgrades = pad.upgrades;
        itemName = pad.itemName;
        level = pad.level;
        totalXP = pad.totalXP;
        enhancelevel = pad.enhancelevel;
        set = (Sets)pad.setid;

        if (guid == null && string.Compare(name,"") != 0)
            guid = Guid.NewGuid().ToString();
        else
            guid = pad.guid;
        
        icon = armorIcon;
    }
}