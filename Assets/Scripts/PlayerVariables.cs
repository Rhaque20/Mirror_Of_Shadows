using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerVariables : MonoBehaviour
{
    [Header("Manual")]
    public AnimatorOverrideController animOverride;
    public NormalAttack normalAttack{get;private set;}
    public PlayerMovement playerMovement{get;private set;}
    public Animator anim{get;private set;}
    public Rigidbody rigid{get;private set;}
    public AttackCollisionManager attackCollision{get;private set;}
    public PlayerForces forces{get;private set;}
    public PlayerAnimFunctions animFunc{get;private set;}
    public PlayerCore playerCore{get;private set;}
    public Controls controls{get;private set;}

    Transform child;

    void Awake()
    {
        controls = new Controls();
        controls.Enable();
        child = transform.GetChild(0);
        normalAttack = GetComponent<NormalAttack>();
        playerMovement = GetComponent<PlayerMovement>();
        rigid = GetComponent<Rigidbody>();
        attackCollision = GetComponent<AttackCollisionManager>();
        forces = GetComponent<PlayerForces>();
        playerCore = GetComponent<PlayerCore>();

        anim = child.GetComponent<Animator>();
        anim.runtimeAnimatorController = animOverride;
        playerMovement.Initialize(anim,rigid);
        normalAttack.playerVar = GetComponent<PlayerVariables>();
        normalAttack.Initialize(anim,rigid);

        animFunc = child.GetComponent<PlayerAnimFunctions>();

        animFunc.normalAttack = normalAttack;
        animFunc.attackCollision = attackCollision;
        animFunc.forces = forces;
        animFunc.playerCore = playerCore;

        playerCore.animOverride = animOverride;
    }
}
