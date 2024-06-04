using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCore : EntityCore
{
    public EnemyStats enemyStats;
    protected enum CurrentState{Searching,Idle,Attack};
    [SerializeField]protected CurrentState _activeState = CurrentState.Searching;
    protected EnemySkill _activeSkill;
    [SerializeField]protected Transform _activeTarget;
    protected Coroutine _phaseTimer = null;
    [SerializeField]protected bool _canAttack = true;
    [SerializeField]protected List<EnemySkill> _moveSet = new List<EnemySkill>();
    protected EnemyMove _enemyMove;

    protected const string ATTACKINDEX = "Attack", RECOVERYINDEX = "Recovery";

    public float skillPoiseMod
    {
        get{ 
            if (_activeSkill == null) return 1f;
            else return _activeSkill.motionArmor;
        }
    }

    protected IEnumerator IdleTimer(float idleTime)
    {
        _enemyMove.SetIsStrafe(true);
        yield return new WaitForSeconds(idleTime);
        _canAttack = true;
        _phaseTimer = null;
        _enemyMove.SetIsStrafe(false);
        
    }

    protected override void SetUpAttack()
    {
        animOverride[ATTACKINDEX] = _activeSkill.anim[0];
        animOverride[RECOVERYINDEX] = _activeSkill.anim[1];
        _enemyMove.SetMovement(false);

        _anim.Play(ATTACKINDEX);
    }

    public override void InterruptAttack()
    {
        if (_phaseTimer != null)
        {
            StopCoroutine(_phaseTimer);
            _phaseTimer = null;
        }

        _enemyMove.SetMovement(false);
    }
    
    public override void Recover()
    {
        // Set up animator to include onground being true
        Debug.Log("Recovering!");
        _enemyMove.SetMovement(true);
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
    protected float DistanceDifference()
    {
        
        Vector3 distanceDiff = _activeTarget.position - transform.position;

        distanceDiff.y = 0;

        float finalDistance = distanceDiff.magnitude;

        return finalDistance;
    }

    protected bool AngleDifference(float angle)
    {
        Vector3 _groundAngle = _activeTarget.position - transform.position;
        _groundAngle.y = 0f;
        return Vector3.Angle(_groundAngle,transform.forward) <= angle;
    }

    protected float HeightDifference()
    {
        
        return Mathf.Abs(_activeTarget.position.y - transform.position.y);
    }

    protected void Start()
    {
        Transform child = transform.GetChild(0);
        _enemyMove = GetComponent<EnemyMove>();
        _anim = child.GetComponent<Animator>();
        enemyStats = GetComponent<EnemyStats>();
        _rigid = GetComponent<Rigidbody>();

        child.GetComponent<EnemyAnimFunctions>().enemyCore = GetComponent<EnemyCore>();

        _anim.runtimeAnimatorController = animOverride;
    }

}
