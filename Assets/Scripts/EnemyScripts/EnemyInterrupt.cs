using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EnemyForces))]
public class EnemyInterrupt: InterruptSystem
{
    EnemyForces _enemyForce;
    EnemyMove _enemyMove;
    EnemyCore _enemyCore;

    protected override IEnumerator StaggerReset(float timer)
    {
        _enemyMove.SetMovement(false);
        // _anim.SetFloat("stunMultiplier",0f);
        yield return new WaitForSeconds(timer);
        // _anim.SetFloat("stunMultiplier",1f);
        if(!_recoverOnGround)
        {
            Debug.Log("Recovering while on the ground");
            _anim.SetTrigger("recover");
            _enemyCore.Recover();
        }
        
    }

    protected override void Start()
    {
        base.Start();
        _enemyForce = GetComponent<EnemyForces>();
        _enemyMove = GetComponent<EnemyMove>();
        _enemyCore = GetComponent<EnemyCore>();
        _enemyForce.StartEnemyForces();
        _enemyForce.onLanding += RecoverOnGround;

    }

    protected override IEnumerator PoiseReset(float timer)
    {
        yield return new WaitForSeconds(timer);

        if(_enemyForce.onGround)
        {
            _poiseState = PoiseState.Active;
            _curPoise = 0f;
        }
    }

    public override void StaggerDamage(Skill receivingSkill,Vector3 knockBackDir)
    {
        if (_poiseState == PoiseState.Active)
        {
            _curPoise += receivingSkill.poiseDamage * (_poiseMod * _enemyCore.skillPoiseMod);

            if (_curPoise >= maxPoise)
            {
                _poiseState = PoiseState.Broken;
                _poiseResetTimer = StartCoroutine(PoiseReset(poiseResetTime));
            }
            else
                return;
        }

        _enemyCore.InterruptAttack();
        
        _anim.Play("Stagger");

        Vector3 finalKnockBacDir = knockBackDir * receivingSkill.knockbackForce.x;

        if(receivingSkill.knockbackForce.y > 0)
        {
            finalKnockBacDir += (Vector3.up * receivingSkill.knockbackForce.y);
            _recoverOnGround = true;
        }
        else if(!_enemyForce.onGround)
        {
            finalKnockBacDir += (Vector3.up * 150);
            _recoverOnGround = true;
        }
        
        _enemyForce.Launch(finalKnockBacDir);

        if (_staggerReset != null)
            StopCoroutine(_staggerReset);
        
        _staggerReset = StartCoroutine(StaggerReset(0.5f));


    }

    void RecoverOnGround()
    {
        if(_recoverOnGround)
        {
            _recoverOnGround = false;
            _poiseState = PoiseState.Active;
            _curPoise = 0f;
            _anim.SetTrigger("recover");
            _enemyCore.Recover();
        }
    }

    // void Update()
    // {
        
    // }
}
