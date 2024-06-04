using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrops : MonoBehaviour
{
    [SerializeField]private Item[] lootTable;
    [SerializeField]private int[] dropChance;

    public void HookupToStats(EnemyStats stat)
    {
        stat.onDeath += RollLootTable;
    }

    public void RollLootTable()
    {
        for(int i = 0; i < lootTable.Length; i++)
        {
            if (UnityEngine.Random.Range(1,101) < dropChance[i])
            {
                (LootManager.generateLoot)?.Invoke(transform.position,lootTable[i]);
            }
        }
    }
}