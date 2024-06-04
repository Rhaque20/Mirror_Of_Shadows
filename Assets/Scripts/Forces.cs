using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forces : MonoBehaviour
{
    protected Rigidbody _rigid;
    protected Animator _anim;
    protected bool _canJump = true;
    [SerializeField]protected float _jumpPower = 500f;
    [SerializeField]protected bool _onGround = true, _readyToLand = false, _falling = false;
    [SerializeField]protected float _slopeCheckDistance;
    protected float _slopeDownAngle;
    [SerializeField]protected LayerMask _groundLayer;

    public bool _isOnSlope {get; protected set;}

    protected bool _suspendGravity = false;
    protected CapsuleCollider _mainCollider;
    protected Coroutine _gravityTimer = null;
    public Action onLanding;

    protected RaycastHit hit;

    public bool onGround
    {
        get{return _onGround;}
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Debug.Log("Called parent forces");
        _rigid = GetComponent<Rigidbody>();
        _anim = transform.GetChild(0).GetComponent<Animator>();
        _mainCollider = GetComponent<CapsuleCollider>();
        _anim.SetBool("onGround",_onGround);
        onLanding += OnLanding;
    }

    protected virtual IEnumerator ManageTimer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _readyToLand = true;
    }

    protected virtual void OnCollisionEnter(Collision col)
    {
        
    }

    protected void SlopeCheck()
    {
        Vector3 checkPos = transform.position;
        Debug.DrawRay(checkPos,Vector3.down * _slopeCheckDistance,Color.cyan);
        if(Physics.Raycast(checkPos,Vector3.down,out hit,_slopeCheckDistance,_groundLayer))
        {

            _slopeDownAngle = Vector3.Angle(hit.normal,Vector3.up);
            _isOnSlope = _slopeDownAngle < 80f && _slopeDownAngle != 0;
        }
        else
        {
            _isOnSlope = false;
        }

        //_rigid.useGravity = !_isOnSlope;

        if (_isOnSlope)
        {
            Debug.Log("On a slope");
        }
        else
        {
            Debug.Log("Not on a slope");
        }
    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction,hit.normal).normalized;
    }


    protected virtual void OnLanding()
    {
        _readyToLand = false;
        _onGround = true;
        _falling = false;
        _anim.SetBool("onGround",_onGround);
    }

    public void Lunge(float force)
    {
        _rigid.AddForce(transform.forward * force *_rigid.mass);
    }

    public virtual void AerialLunge(float force)
    {
        _rigid.AddForce((transform.forward + Vector3.down * 0.7f).normalized * force *_rigid.mass);
    }

    public virtual void Knockback(float force)
    {
        _rigid.AddForce(-transform.forward * force *_rigid.mass);
    }

    public virtual void Launch(Vector3 force)
    {
        if (force.y > 0f)
        {
            _onGround = false;
            _anim.SetBool("onGround",_onGround);
            
            if (_gravityTimer != null)
                StopCoroutine(_gravityTimer);
            
            _gravityTimer = StartCoroutine(ManageTimer(0.1f));
        }


        _rigid.AddForce(force * _rigid.mass);
            
        //_rigid.AddForce(force);
    }

}
