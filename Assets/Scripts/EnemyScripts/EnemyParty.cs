using System;
using System.Collections;
using UnityEngine;

public class EnemyParty : MonoBehaviour
{
    GameObject[] _enemyMembers;
    EnemyStats[] _enemyStats;
    bool _triggered = false;

    void Start()
    {
        _enemyMembers = new GameObject[transform.childCount];
        _enemyStats = new EnemyStats[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            _enemyMembers[i] = transform.GetChild(i).gameObject;
            _enemyMembers[i].SetActive(false);
        }
    }

    public void SummonMobs()
    {
        if(!_triggered)
        {
            _triggered = true;
            foreach(GameObject g in _enemyMembers)
                g.SetActive(true);
        }
    }
}