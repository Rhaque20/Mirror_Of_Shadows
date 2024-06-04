using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterrupt : InterruptSystem
{
    PlayerForces _forces;
    PlayerMovement _playerMove;
    PlayerCore _playerCore;
    // Start is called before the first frame update

    void Start()
    {
        base.Start();
        _playerCore = GetComponent<PlayerCore>();
        _playerMove = GetComponent<PlayerMovement>();
        _forces =  GetComponent<PlayerForces>();
        // _anim = transform.GetChild(0).GetComponent<Animator>();
    }

    protected override IEnumerator StaggerReset(float timer)
    {
        _playerMove.SetMovement(false);
        isStaggered = true;
        // _anim.SetFloat("stunMultiplier",0f);
        yield return new WaitForSeconds(timer);
        isStaggered = false;
        // _anim.SetFloat("stunMultiplier",1f);
        _anim.SetTrigger("recover");
        _playerCore.Recover();
    }
    public override void StaggerDamage(Skill receivingSkill,Vector3 knockBackDir)
    {
        if (_poiseState == PoiseState.Active)
        {
            _curPoise += receivingSkill.poiseDamage * (_poiseMod * _playerCore.skillPoiseMod);

            if (_curPoise >= maxPoise)
            {
                _poiseState = PoiseState.Broken;
                _poiseResetTimer = StartCoroutine(PoiseReset(poiseResetTime));
            }
            else
                return;
        }
        
        _anim.Play("Stagger");
        Vector3 finalKnockBacDir = knockBackDir * receivingSkill.knockbackForce.x;

        if(receivingSkill.knockbackForce.y > 0)
        {
            finalKnockBacDir += (Vector3.up * receivingSkill.knockbackForce.y);
        }

        _forces.Launch(finalKnockBacDir);
        _attackCollisionManager.DisableCollision();

        if (_staggerReset != null)
            StopCoroutine(_staggerReset);
        _staggerReset = StartCoroutine(StaggerReset(0.5f));


    }
}
