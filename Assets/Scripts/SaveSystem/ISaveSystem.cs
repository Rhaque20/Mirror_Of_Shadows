using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveSystem
{
    void LoadDungeonRunData(DungeonRunData data);
    void SaveDungeonRunData(ref DungeonRunData data);
}
