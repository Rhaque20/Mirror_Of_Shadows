using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent((typeof(PlayerForces)))]
public class PlayerDefense : MonoBehaviour
{
    protected int _iFrame = 0;
    [SerializeField]protected AnimationClip[] _defenseAnims = new AnimationClip[2];
    protected AnimatorOverrideController _animOverride;
    protected Animator _anim;
    protected float cooldown = 1f;
    protected PlayerForces _playerForces;
    protected PlayerMovement _playerMovement;
    protected NormalAttack _normalAttack;
    protected PlayerInterrupt _playerInterrupt;

    protected AttackCollisionManager _attackCollisionManager;
    // Start is called before the first frame update
    protected void Start()
    {
        _animOverride = GetComponent<PlayerVariables>().animOverride;
        _anim = transform.GetChild(0).GetComponent<Animator>();
        _playerForces = GetComponent<PlayerForces>();
        _playerMovement = GetComponent<PlayerMovement>();
        _normalAttack = GetComponent<NormalAttack>();
        _playerInterrupt = GetComponent<PlayerInterrupt>();
        _attackCollisionManager = GetComponent<AttackCollisionManager>();
    }

    protected virtual IEnumerator IframeActive(float duration)
    {
        _iFrame = 1;
        yield return new WaitForSeconds(duration);
        _iFrame = 0;
    }

    public bool CanHit(int strength)
    {
        return strength > _iFrame;
    }

}
