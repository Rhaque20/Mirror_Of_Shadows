using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerForces : Forces
{
    PlayerMovement _playerMove;
    protected override void Start()
    {
        base.Start();
        _playerMove = GetComponent<PlayerMovement>();
        _playerMove.setMove += SetJump;
        GetComponent<PlayerVariables>().controls.Combat.Jump.performed += ctx => OnJump();
        GetComponent<PlayerVariables>().controls.Combat.NormalAttack.performed += ctx => ResetAirTimer();
    }
    protected override IEnumerator ManageTimer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _readyToLand = true;
        _gravityTimer = null;
        if (_suspendGravity || !_rigid.useGravity)
        {
            _suspendGravity = false;
            _rigid.useGravity = true;
            Debug.Log("Returning gravity to normal");
        }

    }

    void SetJump(bool canJump)
    {
        _canJump = canJump;
    }

    // void OnCollisionEnter(Collision col)
    // {
    //     if (col.gameObject.CompareTag("Ground") && _readyToLand)
    //     {
    //         Debug.Log("Points colliding: " + col.contacts.Length);
            
    //         foreach (var item in col.contacts)
    //         {
    //             //Debug.DrawRay(item.point, item.normal * 100, UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f), 10f);
    //             Debug.Log("Ray's normal angle is "+Vector3.Angle(item.normal,Vector3.up));
    //             if (Vector3.Angle(item.normal,Vector3.up) <= 75f)
    //             {
    //                 onLanding?.Invoke();
    //                 break;
    //             }
    //         }
    //     }
    //     else
    //     {
    //         Debug.Log("Colliding with "+col.gameObject.name);
    //     }
    // }

    bool CheckOnGround()
    {
        bool pastStatus = _onGround;
        _onGround = Physics.CheckSphere(transform.position,_slopeCheckDistance,_groundLayer);
        if(pastStatus != _onGround)
        {
            if(_onGround)
            {
                PlayerParty.onJump?.Invoke(true);
            }
        }
        return _onGround;
    }

    void Jump()
    {
        _onGround = false;
        _rigid.AddForce(new Vector3(0f,_jumpPower,0f));
        _anim.SetBool("onGround",_onGround);
        _gravityTimer = StartCoroutine(ManageTimer(0.1f));

    }

    void SuspendGravity()
    {
        
    }

    public void ResetAirTimer()
    {
        if (_onGround)
            return;
        
        if(_gravityTimer != null)
        {
            StopCoroutine(_gravityTimer);
        }
        _suspendGravity = true;
        _falling = false;
        _rigid.velocity = Vector2.zero;
        _gravityTimer = StartCoroutine(ManageTimer(2f));
    }

    public override void Launch(Vector3 power)
    {   
        if (power.y > 0)
        {
            _onGround = false;
            _rigid.AddForce(power);
            _anim.SetBool("onGround",_onGround);

             if (_gravityTimer != null)
                StopCoroutine(_gravityTimer);
            
            _gravityTimer = StartCoroutine(ManageTimer(2f));
            _suspendGravity = true;
        }

        _rigid.AddForce(power * _rigid.mass);
        
    }

    private void OnJump()
    {
        if (_onGround && _canJump && _gravityTimer == null)
        {
            PlayerParty.onJump?.Invoke(false);
            Jump();
        }
    }

    void FixedUpdate()
    {
        // if (Input.GetKey("space") && _onGround && _canJump)
        // {
        //     Jump();
        // }



        if (!CheckOnGround())
        {
            if (_rigid.velocity.y <= 0f && !_falling)
            {
                if (!_suspendGravity)
                {
                    _rigid.useGravity = true;
                    _falling = true;
                    _anim.SetBool("falling",_falling);
                }
                else if (_rigid.useGravity && _suspendGravity)
                {
                    _rigid.useGravity = false;
                    _falling = false;
                    _anim.SetBool("falling",_falling);
                }
            }
            else if(_rigid.velocity.y <= 0f)
            {
                Debug.Log("Falling off ledge");
                _onGround = false;
                _falling = false;
                _readyToLand = true;
                _anim.SetBool("onGround",_onGround);
            }
            
        }
        else
        {
            if (_readyToLand)
            {
                onLanding?.Invoke();
            }
            SlopeCheck();
        }
    }
}
