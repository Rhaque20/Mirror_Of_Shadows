using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerPartyLoadOut
{
    public List<PlayerEquipData> equips = new List<PlayerEquipData>();

    public PlayerPartyLoadOut()
    {
        
    }

    public void AddLoadOut(CharacterEquips ce, int i)
    {
        bool newLoad = (i >= equips.Count);
        if (newLoad)
            equips.Add(new PlayerEquipData(ce ,ce.gameObject.name));
        else
        {
            equips[i] = new PlayerEquipData(ce,ce.gameObject.name);
        }
    }


}