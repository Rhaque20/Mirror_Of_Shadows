using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCore : EntityCore
{
    public PlayerStats stats;
    [SerializeField]protected List<PlayerSkill> _skills = new List<PlayerSkill>();
    protected NormalAttack _normalAttack;
    [SerializeField]protected PlayerSkill _activeSkill = null;
    protected PlayerMovement _playerMove;

    protected PlayerInput _playerInput;

    protected PlayerForces _playerForces;

    protected bool _selectingSkill = false;
    protected const string ATTACKINDEX = "Attack", RECOVERYINDEX = "Recovery", ATTACKSTATE = "NA", RECOVERYSTATE = "REC";

    public bool selectingSkill
    {
        get {
            return _selectingSkill;
        }
    }
    public float skillPoiseMod
    {
        get{ 
            if (_activeSkill == null) return 1f;
            else return _activeSkill.motionArmor;
        }
    }

    public bool hasActiveSkill
    {
        get{return _activeSkill != null;}
    }

    // void OnEnable()
    // {
    //     _control?.Combat.Enable();
    // }

    // void OnDisable()
    // {
    //     _control?.Combat.Disable();
    // }

    // protected void OnSkill1()
    // {
    //     Debug.Log("Calling skill 1");
    //     SkillSelect(0);
    // }

    // void OnSkill2(InputValue input)
    // {
    //     SkillSelect(1);
    // }

    // void OnSkill3(InputValue input)
    // {
    //     SkillSelect(2);
    // }

    // void OnSkill4(InputValue input)
    // {
    //     SkillSelect(3);
    // }

    protected virtual void SkillSelect(int i)
    {
        if (_activeSkill != null)
            return;

        if (i < _skills.Count)
        {
            SetActiveSkill(_skills[i]);
            Debug.Log("Setted skill Active");
        }
    }

    void Start()
    {
        stats = GetComponent<PlayerStats>();
        _normalAttack = GetComponent<NormalAttack>();
        _playerMove = GetComponent<PlayerMovement>();
        _rigid = GetComponent<Rigidbody>();
        _activeSkill = null;
        _anim = transform.GetChild(0).GetComponent<Animator>();
        _playerForces = GetComponent<PlayerForces>();
        Debug.Log("Initializing controls for "+gameObject.name);
        _playerInput = GetComponent<PlayerInput>();
        PlayerVariables _pv = GetComponent<PlayerVariables>();
        // _pv.controls.Combat.SkillSelection.started += ctx => SkillSelection(true);
        // _pv.controls.Combat.SkillSelection.canceled += ctx => SkillSelection(false);

        _pv.controls.Combat.Skill1.started += ctx => SkillSelect(0);
        _pv.controls.Combat.Skill2.started += ctx => SkillSelect(1);

        // _control.Combat.SkillSelection.canceled += ctx => SkillSelection(false);
        
        // _control.Combat.Move.canceled += ctx => _moveVec = Vector2.zero;
        // _attackCollisionManager = GetComponent<AttackCollisionManager>();
    }

    public override void Recover()
    {
        _playerMove.SetMovement(true);
        _activeSkill = null;
        // _attackCollisionManager.DisableCollision();

    }

    protected override void SetUpAttack()
    {
        animOverride[ATTACKINDEX] = _activeSkill.anim[0];
        animOverride[RECOVERYINDEX] = _activeSkill.anim[1];
        
        // Jump directly to the attack state
        _anim.Play(ATTACKSTATE);
    }

    public void SetActiveSkill(PlayerSkill setSkill)
    {
        _activeSkill = setSkill;

        // if (setSkill == null)
        //     _attackCollisionManager.DisableCollision();
    }
}
