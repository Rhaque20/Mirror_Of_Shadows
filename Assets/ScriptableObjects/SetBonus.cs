using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SetBonus", menuName = "SetBonus")]
public class SetBonus : ScriptableObject
{
    public Armor.Sets setType;
    public string setName;
    [TextArea(5,10)]
    public string twoPiece;
    [TextArea(5,10)]
    public string fourPiece;
}
