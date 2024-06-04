using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CecileCore : PlayerCore
{
    private int sequence = 0;
    public override void InflictDamage(GameObject target)
    {
        Stats targetStats = target.GetComponent<Stats>();
        EnemyInterrupt targetPoise = target.GetComponent<EnemyInterrupt>();

        if (_activeSkill == null)
        {
            Debug.Log("Can't get activeSkill");
            return;
        }
        targetStats.DamageCalculation(stats,_activeSkill.power);

        if (targetPoise != null && !targetStats.isDead)
        {
            targetPoise.StaggerDamage(_activeSkill,CreateKnockbackVector(target.transform.position));
        }
            

        CameraShake.Instance.ShakeCamera(2f,0.2f);
        TimeManager.hitStop?.Invoke(EnumLib.HitStunDuration(_activeSkill.hitWeight));
    }

    protected override void SkillSelect(int i)
    {
        if (_activeSkill != null)
            return;
        
        if (i < _skills.Count)
        {
            SetActiveSkill(_skills[i]);
            Debug.Log("Setted skill Active");
            SetUpAttack();
            // _selectingSkill = false;
        }
    }

    public override void SpecialCharge()
    {
        if (_activeSkill.skillName == "Lightning Flash")
        {
            Debug.Log("Side swiping");
            _rigid.velocity = Vector3.zero;
            if (sequence % 2 == 0)
            {
                _rigid.AddForce(-transform.right * 400f);
            }
            else
            {
                _rigid.AddForce(transform.right * 400f);
            }

            _rigid.AddForce(transform.forward * 400f);
            sequence = (sequence + 1) % 2;
        }
    }

    void Update()
    {
        // if(Input.GetKey("x") && _activeSkill == null)
        // {
        //     SkillSelect();
        //     if (_activeSkill != null)
        //     {
        //         Debug.Log("Casting skill");
        //         SetUpAttack();
        //     }
        // }
    }
}
