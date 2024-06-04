using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class RPGMatData
{
    public string itemName, description;
    public int stack,maxStack,origin, grade,category,statVal;

    public float val;

    public RPGMatData(RPGMaterial r)
    {
        grade = r.rarity;
        itemName = r.name;
        description = r.description;
        category = (int)r.category;
        val = r.val;
        statVal = r.statVal;
        origin = (int)r.origin;
        stack = r.stackCount;
        maxStack = r.maxStack;
        
    }
}