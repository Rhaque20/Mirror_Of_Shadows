using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfCore : EnemyCore
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
    protected override void SkillSelect()
    {
        float distDiff = DistanceDifference();

        if (distDiff <= 5f && AngleDifference(45f))
        {
            _activeSkill = _moveSet[0];
            _canAttack = false;
        }
        else if (AngleDifference(45f))
        {
            _activeSkill = _moveSet[1];
            _canAttack = false;
        }

        if (_activeSkill != null)
        {
            _activeState = CurrentState.Attack;
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
