using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NormalAttack : MonoBehaviour
{
    private Rigidbody _rigid;
    private Animator _anim;
    [SerializeField]private float _forwardForce = 100f;
    [SerializeField]private List<PlayerSkill> _normalAttacks = new List<PlayerSkill>();
    [SerializeField]private List<PlayerSkill> _aerialAttacks = new List<PlayerSkill>();
    public AnimatorOverrideController animOverride;
    private int _groundChain = 0,_airChain = 0;

    private const string ATTACKINDEX = "Attack", RECOVERYINDEX = "Recovery", ATTACKSTATE = "NA", RECOVERYSTATE = "REC";

    [SerializeField]bool _canAttack = true, _hasAttackBuffer = false, _endOfChain = false;

    // bool _triggerNormal = true;
    [HideInInspector]public PlayerVariables playerVar;
    [SerializeField]private PlayerCore _playerCore;

    private PlayerForces _playerForces;
    private PlayerMovement _playerMove;

    public bool canAttack
    {
        get { return _canAttack; }
        set{_canAttack = value;}
    }
    
    // Start is called before the first frame update
    public void Initialize(Animator a,Rigidbody r)
    {
        _rigid = r;
        _anim = a;
        PlayerVariables playerVar = GetComponent<PlayerVariables>();
        animOverride = playerVar.animOverride;// Get animation override controller from player variables
        _playerCore = GetComponent<PlayerCore>();
        _playerMove = GetComponent<PlayerMovement>();
        _playerForces = playerVar.forces;
        //playerVar.controls.Combat.SkillSelection.started += ctx => NormalInputStopper(false);
        playerVar.controls.Combat.NormalAttack.performed += ctx => OnNormalAttack();
        _playerForces.onLanding += RecoverOnLanding;
        //playerVar.controls.Combat.SkillSelection.canceled += ctx => NormalInputStopper(true);
    }

    public void Recovery()
    {
        _playerCore.SetActiveSkill(null);
        // _triggerNormal = true;
        if(_playerForces.onGround || !_endOfChain)
        {
            canAttack = true;
            if(_playerForces.onGround)
                (_playerMove.setMove)?.Invoke(true);// Enable movement of player upon recovery animation
        }
        
    }

    public void RecoverOnLanding()
    {
        canAttack = true;
        (_playerMove.setMove)?.Invoke(true);
        _airChain = 0;
    }

    public void StartAttack()
    {
        (_playerMove.setMove)?.Invoke(false);
        _canAttack = false;
    }

    public void Thrust()
    {
        // take current forward directon and propel player that direction
        _rigid.AddForce(this.transform.forward * _forwardForce);
    }

    public void Attack()
    {
        // Check if the current state is "Rec". If not then reset attack chain
        if (!_anim.GetCurrentAnimatorStateInfo(0).IsName(RECOVERYSTATE))
        {
            _groundChain = 0;
        }

        // If this is called while end of chain was active, disable it
        if (_endOfChain)
        {
            _endOfChain = false;
        }

        // Set animation on their respective states
        if (playerVar.forces.onGround)
        {
            animOverride[ATTACKINDEX] = _normalAttacks[_groundChain].anim[0];
            animOverride[RECOVERYINDEX] = _normalAttacks[_groundChain].anim[1];
            _playerCore.SetActiveSkill(_normalAttacks[_groundChain]);
            _groundChain = (_groundChain + 1) % _normalAttacks.Count;
        }
        else
        {
            animOverride[ATTACKINDEX] = _aerialAttacks[_airChain].anim[0];
            animOverride[RECOVERYINDEX] = _aerialAttacks[_airChain].anim[1];
            _playerCore.SetActiveSkill(_aerialAttacks[_airChain]);
            _airChain++;
            if(_airChain == _aerialAttacks.Count)
            {
                _endOfChain = true;
            }
        }
        
        // Jump directly to the attack state
        _anim.Play(ATTACKSTATE);
            
        // If reaching the end of the array, call end of chain
        if (_groundChain == _normalAttacks.Count - 1)
            _endOfChain = true;
        
        
    }

    // private void NormalInputStopper(bool stopNormal)
    // {
    //     Debug.Log("Setting normalattack stopper to "+stopNormal);
    //     if (!_playerCore.hasActiveSkill)
    //         _triggerNormal = stopNormal;
    // }

    private void OnNormalAttack()
    {
        Debug.Log("Triggered normal attack");

        // if(!_triggerNormal)
        //     return;

        if (_canAttack)
        {
            StartAttack();
            Attack();
        }
        else if (!_hasAttackBuffer)// Input buffer, won't activate if end of chain
            _hasAttackBuffer = true;
    }

    // Update is called once per frame
    void Update()
    {

        // Allows for recalling of NA state to transition seamlessly
        if (_canAttack && _hasAttackBuffer)
        {
            Debug.Log("Triggering Buffer");
            _canAttack = false;
            _hasAttackBuffer = false;
            (_playerMove.setMove)?.Invoke(false);
            Attack();
        }

        if(playerVar.forces.onGround)
        {
            _airChain = 0;
        }

        // if (Input.GetKeyUp("z"))
        // {
        //     if (_canAttack)
        //     {
        //         StartAttack();
        //         Attack();
        //     }
        //     else if (!_hasAttackBuffer && !_endOfChain)// Input buffer, won't activate if end of chain
        //         _hasAttackBuffer = true;
        // }
    }
}
