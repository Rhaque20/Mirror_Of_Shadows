using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class ItemIconManager : MonoBehaviour
{
    public SpriteLibraryAsset materialIcons;

    public Sprite ReturnItemIcon(string origin, string itemName)
    {
        return materialIcons.GetSprite(origin,itemName);
    }
}