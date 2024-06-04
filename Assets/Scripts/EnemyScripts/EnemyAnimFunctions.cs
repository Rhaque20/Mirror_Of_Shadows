using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimFunctions : AnimFunctions
{
    public EnemyForces enemyForces;
    public EnemyCore enemyCore;
    public AttackCollisionManager attackCollisionManager;

    private void Start()
    {
        base.Start();
        attackCollisionManager = transform.parent.GetComponent<AttackCollisionManager>();
    }
    public override void Lunge(float power)
    {
        Debug.Log("Lunging");
        enemyForces.Lunge(power);
    }

    public override void AerialLunge(float power)
    {
        enemyForces.AerialLunge(power);
    }

    public override void Recovery()
    {
        enemyCore.Recover();
    }

    public override void AttackScan(int i)
    {
        attackCollisionManager.AttackScan(i);
    }

    private void OnAnimatorMove()
    {
        if(_anim)
        {
            //Debug.Log("Animator deltaRotation is "+_anim.deltaRotation);
            if (_anim.deltaRotation != Quaternion.identity)
                transform.parent.localRotation *= _anim.deltaRotation;
            if (_anim.deltaPosition != Vector3.zero)
            {
                //Debug.Log("Setting position via root");
                transform.parent.position += _anim.deltaPosition;
            }
        }
    }
    
}
