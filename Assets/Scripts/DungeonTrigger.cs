using System;
using System.Collections;
using UnityEngine;

public class DungeonTrigger : MonoBehaviour
{
    [SerializeField]int _triggerID = 0;
    BoxCollider _collider;

    void Start()
    {
        _collider = GetComponent<BoxCollider>();
    }
    void OnTriggerEnter(Collider col)
    {
        DungeonMaster.triggerWave?.Invoke(_triggerID);
        gameObject.SetActive(false);
    }
}