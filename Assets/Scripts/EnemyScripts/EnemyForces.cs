using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyForces : Forces
{
    [SerializeField]Transform _target;
    [SerializeField]bool _isFlying = false;
    public void StartEnemyForces()
    {
        base.Start();
        transform.GetChild(0).GetComponent<EnemyAnimFunctions>().enemyForces = GetComponent<EnemyForces>();
    }
    protected override void Start()
    {
        
    }

    void OnDisable()
    {
        onLanding -= OnLanding;
    }

    void OnEnable()
    {
        onLanding += OnLanding;
    }

    // public override void AerialLunge(float force)
    // {
    //     _rigid.AddForce((_target.position - transform.position).normalized * force *_rigid.mass);
    // }

    protected override void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ground") && _readyToLand)
        {
            Debug.Log(gameObject.name+" has landed on the ground!");
            onLanding?.Invoke();
        }
        
    }
}
