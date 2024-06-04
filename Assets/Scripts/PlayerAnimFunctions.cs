using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimFunctions : AnimFunctions
{
    public NormalAttack normalAttack;
    public AttackCollisionManager attackCollision;
    public PlayerForces forces;
    public PlayerCore playerCore;
    // Start is called before the first frame update
    public override void Recovery()
    {
        normalAttack.Recovery();
    }
    
    public override void Thrust()
    {
        normalAttack.Thrust();
    }

    public override void Lunge(float power)
    {
        forces.Lunge(power);
    }

    public override void Launch(float power)
    {
        forces.Launch(new Vector2(0f,power));
    }

    public override void ActivateWeaponCollider(int i)
    {
        attackCollision.ActivateWeaponCollider(i);
    }

    public override void DeactivateWeaponCollider(int i)
    {
        attackCollision.DeactivateWeaponCollider(i);
    }

    public override void AttackScan(int i)
    {
        attackCollision.AttackScan(i);
    }

    public void SpecialCharge()
    {
        playerCore.SpecialCharge();
    }
}
