using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityCore : MonoBehaviour
{
    protected Animator _anim;
    [SerializeField]public AnimatorOverrideController animOverride;

    protected Rigidbody _rigid;

    protected AttackCollisionManager _attackCollisionManager;
    public virtual void InflictDamage(GameObject target)
    {

    }

    protected Vector3 CreateKnockbackVector(Vector3 target)
    {
        return new Vector3(target.x - transform.position.x,0f,target.z - transform.position.z);
    }

    protected virtual void SetUpAttack()
    {

    }

    public virtual void InterruptAttack()
    {

    }

    public virtual void Recover()
    {
        
    }

    protected virtual void SkillSelect()
    {

    }

    public virtual void SpecialCharge()
    {

    }
}
