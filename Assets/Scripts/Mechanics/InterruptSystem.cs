using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InterruptSystem : MonoBehaviour
{
    protected Animator _anim;
    public enum ArmorState{Neutral, SuperArmor, HyperArmor};
    public enum PoiseState{Active,Broken};

    protected PoiseState _poiseState = PoiseState.Active;
    public float maxPoise = 100f, endurance = 1f, poiseDrain = 5f, poiseResetTime = 2f;
    [SerializeField]protected float _curPoise = 0f;
    
    protected AttackCollisionManager _attackCollisionManager;

    protected float _poiseMod = 1f, _motionArmor = 1f;

    public bool isStaggered = false;

    protected bool _recoverOnGround = false;

    protected int airRecovery = 0;

    protected Coroutine _poiseResetTimer,_staggerReset;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        _anim = transform.GetChild(0).GetComponent<Animator>();
        _attackCollisionManager = GetComponent<AttackCollisionManager>();
    }

    protected virtual IEnumerator PoiseReset(float timer)
    {
        yield return new WaitForSeconds(timer);

        _poiseState = PoiseState.Active;
        _curPoise = 0f;
    }

    protected virtual IEnumerator StaggerReset(float timer)
    {
        isStaggered = true;
        yield return new WaitForSeconds(timer);
        _anim.SetTrigger("recover");
        isStaggered = false;
    }

    public virtual void StaggerDamage(Skill receivingSkill,Vector3 knockBackDir)
    {
        if (_poiseState == PoiseState.Active)
        {
            _curPoise += (receivingSkill.poiseDamage *(1+_poiseMod + _motionArmor));

            if (_curPoise >= maxPoise)
            {
                _poiseState = PoiseState.Broken;
                _poiseResetTimer = StartCoroutine(PoiseReset(poiseResetTime));
            }
            else
                return;
        }

        _anim.Play("Stagger");



    }

    void Update()
    {
        if (_curPoise > 0f && _poiseState == PoiseState.Active)
        {
            _curPoise -= poiseDrain;
            
            if (_curPoise < 0f)
                _curPoise = 0f;
        }
    }
}
