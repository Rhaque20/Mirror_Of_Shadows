using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//[CreateAssetMenu(fileName = "New Equipment", menuName = "Equipment")]
[Serializable]
public struct StatBlock
{
    public EnumLib.Stats statType;
    public float value;
    public int grade;

    public StatBlock(EnumLib.Stats stats, float v1, int v2) : this()
    {
        statType = stats;
        value = v1;
        grade = v2;
    }
};
public class Equipment: Item
{
    public enum Piece {Helmet,Chestplate,Boots,Necklace,Ring,Belt,Weapon};
    public enum Trigger{Passive,SkillCast,PreDMG,PostDMG,Dodge,Debuff,Buff,NA,onCrit,onDMGdealt,onBattleStart,onBattleEnd};
    public int level = 1;
    public Trigger active = Trigger.Passive;
    public int enhancelevel = 0;
    public int totalXP = 0;
    public string guid;
    public Piece slot;

    public int triggerInt
    {
        get
        {
            return (int)active;
        }

    }
}