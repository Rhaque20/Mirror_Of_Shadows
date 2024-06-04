using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class ArmorAttributes
{
    //public static GameObject equipSprites;

    public static Item.Grade RollRarity()
    {
        float chance = Random.Range(1f,100f);

        if (chance <= 3f)
            return Item.Grade.Epic;
        else if (chance <= 13f)
            return Item.Grade.Elite;
        else if (chance <= 30f)
            return Item.Grade.Rare;
        else if (chance <= 50f)
            return Item.Grade.Uncommon;
        
        return Item.Grade.Normal;
        
    }

    public static Item.Grade RollRarity(EnumLib.RarityGroup rg)
    {
        float chance = Random.Range(1f,100f);
        Item.Grade rarity = Item.Grade.Normal;

        switch(rg)
        {
            case EnumLib.RarityGroup.Intermediate:
                if (chance <= 3f)
                    rarity = Item.Grade.Epic;
                else if (chance <= 20f)
                    rarity = Item.Grade.Elite;
                else if (chance <= 50f)
                    rarity = Item.Grade.Rare;
                else
                    rarity = Item.Grade.Uncommon;
                
            break;

            case EnumLib.RarityGroup.Advanced:
                if (chance <= 10f)
                    rarity = Item.Grade.Epic;
                else if (chance <= 55f)
                    rarity = Item.Grade.Elite;
                else
                    rarity = Item.Grade.Rare;
            break;

            default:

                if (chance <= 10f)
                    rarity = Item.Grade.Rare;
                else if (chance <= 40f)
                    rarity = Item.Grade.Uncommon;

                break;
            
        }

        return rarity;

    }

    public static int RollSubRarity()
    {
        float chance = Random.Range(1f,100f);

        if (chance <= 3f)
            return 5;
        else if (chance <= 13f)
            return 4;
        else if (chance <= 30f)
            return 3;
        else if (chance <= 50f)
            return 2;
        
        return 1;
    }

    public static Equipment.Piece RollPiece()
    {
        int piece = Random.Range(0,6);

        switch(piece)
        {
            case 1:
                return Equipment.Piece.Chestplate;
            case 2:
                return Equipment.Piece.Boots;
            case 3:
                return Equipment.Piece.Necklace;
            case 4:
                return Equipment.Piece.Ring;
            case 5:
                return Equipment.Piece.Belt;
            default:
                return Equipment.Piece.Helmet;
                
        }
    }

    public static float HelmetMain(int level, int rarity)
    {
        float val = 0f;

        if (level == 1)
        {
            val = 81f;
        }
        else if (level <= 15)
        {
            val = 141f;
        }
        else if (level <= 30)
        {
            val = 216f;
        }
        else if (level <= 45)
        {
            val = 345f;
        }
        else if (level <= 60)
        {
            val = 415f;
        }
        else if (level <= 75)
        {
            val = 500f;
        }
        else
        {
            val = 540f * (level/85f);
        }

        return val * (1 + 0.15f*rarity);
    }

    public static float ChestplateMain(int level, int rarity)
    {
        float val = 0f;

        if (level == 1)
        {
            val = 9f;
        }
        else if (level <= 15)
        {
            val = 15f;
        }
        else if (level <= 30)
        {
            val = 24f;
        }
        else if (level <= 45)
        {
            val = 35f;
        }
        else if (level <= 60)
        {
            val = 45f;
        }
        else if (level <= 75)
        {
            val = 50f;
        }
        else
        {
            val = 60f * (level/85f);
        }

        return val * (1 + 0.15f*rarity);
    }

    public static float mainStat(int stat, int level, int rarity)
    {
        float val = 0f;
        if (stat < 5)
        {
            if (level == 1)
            {
                val = 0.04f;
            }
            else if (level <= 15)
            {
                val = 0.05f;
            }
            else if (level <= 30)
            {
                val = 0.06f;
            }
            else if (level <= 45)
            {
                val = 0.07f;
            }
            else if (level <= 60)
            {
                val = 0.08f;
            }
            else if (level <= 75)
            {
                val = 0.1f;
            }
            else
            {
                val = 0.12f * (level/85f);
            }
        }
        else if (stat == 5)
        {
            if (level == 1)
            {
                val = 0.04f;
            }
            else if (level <= 15)
            {
                val = 0.06f;
            }
            else if (level <= 30)
            {
                val = 0.07f;
            }
            else if (level <= 45)
            {
                val = 0.08f;
            }
            else if (level <= 60)
            {
                val = 0.09f;
            }
            else if (level <= 75)
            {
                val = 0.11f;
            }
            else
            {
                val = 0.12f * (level/85f);
            }
        }
        else if (stat == 6)
        {
            if (level == 1)
            {
                val = 0.05f;
            }
            else if (level <= 15)
            {
                val = 0.07f;
            }
            else if (level <= 30)
            {
                val = 0.08f;
            }
            else if (level <= 45)
            {
                val = 0.095f;
            }
            else if (level <= 60)
            {
                val = 0.11f;
            }
            else if (level <= 75)
            {
                val = 0.13f;
            }
            else
            {
                val = 0.14f * (level/85f);
            }
        }
        else
        {
            if (level == 1)
            {
                val = 0.03f;
            }
            else if (level <= 15)
            {
                val = 0.04f;
            }
            else if (level <= 30)
            {
                val = 0.06f;
            }
            else if (level <= 45)
            {
                val = 0.08f;
            }
            else if (level <= 60)
            {
                val = 0.115f;
            }
            else if (level <= 75)
            {
                val = 0.15f;
            }
            else
            {
                val = 0.18f * (level/85f);
            }
        }

        return val * (1 + 0.15f*rarity);
    }

    public static float subStatVal(int subrare, float baseStat, float increment)
    {
        return Random.Range(baseStat + ((subrare-1)*increment), baseStat + (subrare*increment));
    }

    public static float subStat(int stat, int subrare, int level)
    {
        float val = 0f;
        if (stat < 6)
        {
            if (level == 1)
            {
                val = subStatVal(subrare,0.01f, 0.005f);
            }
            else if (level <= 15)
            {
                val = subStatVal(subrare,0.02f, 0.005f);
            }
            else if (level <= 30)
            {
                val = subStatVal(subrare,0.03f, 0.005f);
            }
            else if (level <= 45)
            {
                val = subStatVal(subrare,0.04f, 0.005f);
            }
            else if (level <= 60)
            {
                val = subStatVal(subrare,0.05f,0.005f);
            }
            else
            {
                val = subStatVal(subrare,0.05f,0.01f);
            }
        }
        else// SP Gain and others
        {
            if (level == 1)
            {
                val = subStatVal(subrare,0.02f, 0.005f);
            }
            else if (level <= 15)
            {
                val = subStatVal(subrare,0.03f, 0.005f);
            }
            else if (level <= 30)
            {
                val = subStatVal(subrare,0.04f, 0.005f);
            }
            else if (level <= 45)
            {
                val = subStatVal(subrare,0.05f, 0.005f);
            }
            else if (level <= 60)
            {
                val = subStatVal(subrare,0.06f,0.005f);
            }
            else
            {
                val = subStatVal(subrare,0.06f,0.01f);
            }
        }

        return val;
    }

    public static string StatName(int i)
    {
        switch(i)
        {
            case 0:
                return "Health";
            case 1:
                return "Attack";
            case 2:
                return "Defense";
            case 3:
                return "Potency";
            case 4:
                return "Resistance";
            case 5:
                return "Crit Rate";
            case 6:
                return "Crit DMG";
            case 7:
                return "MP Gain";
        }

        return "Mode Boost";
    }

    public static string StatName(EnumLib.Stats statType)
    {
        string name = statType.ToString();
        if (statType == EnumLib.Stats.SPGain)
        {
            name = "SP Gain";
        }
        else
            name = string.Concat(name.Select(x => char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
        return name;
    }

    public static int GenerateStat()
    {
        return Random.Range(0,8);
    }

    public static Armor GenerateArmor(int level,Armor.Sets settype)
    {
        Item.Grade g = RollRarity();
        Equipment.Piece p = RollPiece();
        return GenerateArmor(level,settype,g,p);
    }

    public static Armor GenerateArmor(int level, Armor.Sets settype, Item.Grade g)
    {
        Equipment.Piece p = RollPiece();
        return GenerateArmor(level,settype,g,p);
    }

    public static Armor GenerateArmor(int level, Armor.Sets settype, Item.Grade g, Equipment.Piece p)
    {
        int rarity = (int)g;
        int count = Mathf.Min(rarity, 4);
        float[] statvals = new float[count+1];
        int[] stats = new int[count+1];
        int[] statrarity;
        float[] upgrades;
        string name;
        Sprite armorIcon;

        if (EquipmentSprites.instance != null)
        {
            armorIcon = EquipmentSprites.instance.GetArmorIcon(settype.ToString(),p.ToString());
        }
        else
            armorIcon = null;
        
        statrarity = new int[count+1];
        upgrades = new float[count+1];

        name = settype.ToString();
        name = string.Concat(name.Select(x => char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');

        switch(p)
        {
            case Equipment.Piece.Helmet:
                stats[0] = 0;
                statvals[0] = HelmetMain(level,rarity);
            break;

            case Equipment.Piece.Chestplate:
                stats[0] = 2;
                statvals[0] = ChestplateMain(level, rarity);
            break;

            default:
                stats[0] = GenerateStat();
                statvals[0] = mainStat(stats[0],level,rarity);
                break;
        }

        name += " "+p.ToString();

        if (count != 0)
        {
            int i;
            List<int> statchosen = new List<int>();
            int temp = 0;
            bool dupe = true;
            //int temprarity = 0;

            statchosen.Add(stats[0]);

            for(i = 1; i < stats.Length; i++)
            {
                while (dupe)
                {
                    temp = GenerateStat();
                    foreach(int j in statchosen)
                    {
                        //Debug.Log("Checking "+j+" against "+temp);
                        dupe = (j == temp);
                        if (dupe)
                        {
                            //Debug.Log("Dupe detected");
                            break;
                        }
                    }
                }

                //Debug.Log("Adding "+temp);
                statchosen.Add(temp);
                stats[i] = temp;
                statrarity[i] = RollSubRarity();
                statvals[i] = subStat(temp,statrarity[i]-1,level);
                dupe = true;
            }
        }

        return new Armor(p,g,statvals,stats,statrarity,upgrades,name,level,settype, armorIcon);
    }
}
