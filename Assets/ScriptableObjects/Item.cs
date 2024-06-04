using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item: ScriptableObject
{
    public enum Grade{Normal,Uncommon,Rare,Elite,Epic,Mythical}
    public Grade grade = 0;
    public Sprite icon;
    public string itemName;
    public string description;
    public int rarity
    {
        get
        {
            return (int)grade;
        }

        set
        {
            grade = (Grade)value;
        }
    }
}