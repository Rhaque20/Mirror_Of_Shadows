using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class EnemyHealthMini : HealthBar
{
    public Camera cameraFollow;

    protected override void Start()
    {
        base.Start();
        cameraFollow = Camera.main;
    }

    void Update()
    {
        transform.forward = cameraFollow.transform.forward;
    }
}