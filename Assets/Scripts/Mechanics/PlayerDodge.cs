using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodge : PlayerDefense
{
    // Start is called before the first frame update

    // Update is called once per frame
    Vector2 dodgeDir;
    [SerializeField]private AnimationClip _counterEdge;
    [SerializeField]private float _dodgePower = 300f;
    private int _dodgeCount = 1;
    private bool _canDodge = true;

    void Start()
    {
        base.Start();
        GetComponent<PlayerVariables>().controls.Combat.Defensive.performed += ctx => InitiateDodge();
    }

    private IEnumerator DodgeCooldown(float duration)
    {
        yield return new WaitForSeconds(duration);
        _dodgeCount++;
    }

    protected override IEnumerator IframeActive(float duration)
    {
        _iFrame = 1;
        yield return new WaitForSeconds(duration);
        _iFrame = 0;
        _canDodge = true;
        StartCoroutine(DodgeCooldown(1f));

    }

    void InitiateDodge()
    {
        if(_dodgeCount > 0 && _canDodge && !_playerInterrupt.isStaggered)
        {
            _attackCollisionManager.DisableCollision();
            _canDodge = false;
            _dodgeCount--;
            Dodge(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        }
    }

    void Dodge(float x, float y)
    {
        
        dodgeDir = new Vector2(x,y);

        if (dodgeDir == Vector2.zero)
        {
            _animOverride["Dodge"] = _defenseAnims[0];
            _playerForces.Knockback(_dodgePower);
        }
        else
        {
            _animOverride["Dodge"] = _defenseAnims[1];
            _playerForces.Lunge(_dodgePower);
        }
        _normalAttack.StartAttack();
        
        _anim.SetTrigger("dodge");

        StartCoroutine(IframeActive(0.5f));
    }
    void Update()
    {
        // if (Input.GetKeyUp("c") && _dodgeCount > 0 && _canDodge && !_playerInterrupt.isStaggered)
        // {
        //     _attackCollisionManager.DisableCollision();
        //     _canDodge = false;
        //     _dodgeCount--;
        //     Dodge(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        // }
    }
}
