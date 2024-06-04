using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVariables : MonoBehaviour
{
    // Start is called before the first frame update
    EnemyDrops _enemyDrops;
    EnemyStats _enemyStats;

    void Awake()
    {
        _enemyDrops = GetComponent<EnemyDrops>();
        _enemyStats = GetComponent<EnemyStats>();
        _enemyDrops.HookupToStats(_enemyStats);
    }
}
