using System;
using System.Collections;
using UnityEngine;

public class DungeonMaster : MonoBehaviour
{
    [SerializeField]GameObject _enemyPartyObject, _barrierObjects;
    public static Action<int> triggerWave;

    void Start()
    {
        triggerWave = TriggerWave;
    }

    void TriggerWave(int i)
    {
        GameObject g = _enemyPartyObject.transform.GetChild(i).gameObject;
        g.GetComponent<EnemyParty>().SummonMobs();
    }
}