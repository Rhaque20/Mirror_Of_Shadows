using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyForces : EnemyForces
{
    bool _grounded = false;
    protected override void OnCollisionEnter(Collision col)
    {
        
    }

    public override void Launch(Vector3 force)
    {
        _rigid.useGravity = true;
        if (force.y > 0f)
        {
            _onGround = false;
            _anim.SetBool("onGround",_onGround);
            
            if (_gravityTimer != null)
                StopCoroutine(_gravityTimer);
            
            _gravityTimer = StartCoroutine(ManageTimer(0.1f));
        }


        _rigid.AddForce(force * _rigid.mass);
            
        //_rigid.AddForce(force);
    }
}