using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HawkCore : EnemyCore
{
    public override void InflictDamage(GameObject target)
    {
        PlayerDefense playerDefense = target.GetComponent<PlayerDefense>();
        PlayerInterrupt playerPoise = target.GetComponent<PlayerInterrupt>();
        Stats targetStats = target.GetComponent<Stats>();
        Debug.Log("Hitting "+target.name);

        if (playerDefense.CanHit(1))
        {
            Debug.Log("Smack on "+target.name);
            targetStats.DamageCalculation(enemyStats,_activeSkill.power);
            playerPoise.StaggerDamage(_activeSkill,transform.forward);
        }
        else
        {
            Debug.Log("Perfect");
            TimeManager.slowDown?.Invoke();
        }
    }

    public override void Recover()
    {
        // Set up animator to include onground being true
        Debug.Log("Recovering!");
        _enemyMove.SetMovement(true);
        
        _rigid.useGravity = false;
        _rigid.velocity = Vector3.zero;

        if (_phaseTimer != null)
        {
            StopCoroutine(_phaseTimer);
        }

        if(_activeSkill != null)
            _phaseTimer = StartCoroutine(IdleTimer(_activeSkill.actionInterval));
        else
            _phaseTimer = StartCoroutine(IdleTimer(1.5f));
            
        _activeSkill = null;
        _activeState = CurrentState.Idle;
    }
    protected override void SkillSelect()
    {
        float distDiff = DistanceDifference();

        if (distDiff <= 5f && AngleDifference(45f))
        {
            _activeSkill = _moveSet[0];
            _canAttack = false;
        }
        else
        {
            Debug.Log("Dist diff is "+distDiff+" and Angle Diff 45 is"+AngleDifference(45f));
        }
    }

    void Update()
    {
        if (_canAttack && _activeState != CurrentState.Attack)
        {
            SkillSelect();

            if (_activeSkill != null)
            {
                SetUpAttack();
            }
        }
    }
}