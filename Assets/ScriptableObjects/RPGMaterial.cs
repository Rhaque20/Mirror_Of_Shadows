using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New RPGMaterial", menuName = "RPGMaterial")]
public class RPGMaterial: Item
{
    public enum MatType{Crafting,EXP,Gemstone, Sellable};
    public MatType category;
    public EnumLib.Nation origin;
    public float val = 0f;// Used for XP Values, substat type, or sell value
    public int statVal = 0;// Used to determine mainstat type
    public int stackCount = 0;
    public int maxStack = 99;
    //string guid;

    public RPGMaterial(RPGMaterial rp)
    {
        grade = rp.grade;
        icon = rp.icon;
        name = rp.name;
        description = rp.description;
        category = rp.category;
        val = rp.val;
        statVal = rp.statVal;
        stackCount = 0;
        origin = rp.origin;
        maxStack = rp.maxStack;
    }

    public RPGMaterial(RPGMatData rp, Sprite icon)
    {
        grade = (Item.Grade)rp.grade;
        this.icon = icon;
        name = rp.itemName;
        itemName = rp.itemName;
        description = rp.description;
        category = (RPGMaterial.MatType)rp.category;
        val = rp.val;
        statVal = rp.statVal;
        origin = (EnumLib.Nation)rp.origin;
        stackCount = rp.stack;
        maxStack = rp.maxStack;

    }

}