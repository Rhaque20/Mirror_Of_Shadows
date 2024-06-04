using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stat Growth", menuName = "Stat Growth")]
public class StatGrowth : ScriptableObject
{

    public float baseHP = 0f, baseATK = 0f, baseDEF = 0f;
    public float baseCritRate = 0.05f, baseCritDMG = 0.5f;
    public float basePotency = 0f, baseResistance = 0f;

    public float HPGrowth = 0f, ATKGrowth = 0f, DEFGrowth = 0f;
}