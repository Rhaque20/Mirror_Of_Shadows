using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimFunctions : MonoBehaviour
{
    protected Animator _anim;

    protected void Start()
    {
        _anim = GetComponent<Animator>();
    }
    public virtual void Launch(float power)
    {

    }
    public virtual void Recovery()
    {
    }
    
    public virtual void Thrust()
    {
    }

    public virtual void Lunge(float power)
    {
    }
    public virtual void AerialLunge(float power)
    {
        
    }

    public virtual void ActivateWeaponCollider(int i)
    {
    }

    public virtual void DeactivateWeaponCollider(int i)
    {

    }

    public virtual void AttackScan(int i)
    {

    }
}
