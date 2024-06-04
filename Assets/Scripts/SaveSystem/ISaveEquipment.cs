using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveEquipment
{
    void LoadData(InventoryData data);
    void SaveData(ref InventoryData data);
}
