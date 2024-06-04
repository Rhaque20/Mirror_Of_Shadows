using System;
using System.Collections;
using UnityEngine;

public class EnemyFly : EnemyMove
{
    //[SerializeField] Vector3 _heightOffset = new Vector3(0f,3f,0f);
    [SerializeField]float _heightOffset = 4f, _lastTrackedHeight = 0f;
    bool _adjustHeight = true;

    public void AdjustHeight(bool val)
    {
        _adjustHeight = val;
        if(!_adjustHeight)
        {
            _lastTrackedHeight = _targetPos.position.y;
        }
    }
    protected override float HeightDifference()
    {
        if (!_adjustHeight)
            return transform.position.y - _lastTrackedHeight;
        else
            return transform.position.y - _targetPos.position.y;
    }

    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        _anim = transform.GetChild(0).GetComponent<Animator>();
        setMove = SetMovement;
        PlayerParty.onJump += AdjustHeight;
    }

    protected override void Strafe()
    {
        if(HeightDifference() < _heightOffset)
        {
            Debug.Log("Flying up");
            
            _rigid.MovePosition(transform.position + ((transform.right + transform.up) * _baseSpeed * Time.deltaTime * _speedMod));
        }
        else
            _rigid.MovePosition(transform.position + (transform.right *_baseSpeed * Time.deltaTime * _speedMod));
    }
    
    protected override void Move()
    {
        float angleToTarget = Vector3.Angle(_targetDirGround,transform.forward);
        if (_turningAround != null)
        {
            return;
        }

        if (angleToTarget <= 45f)
        {
            if (DistanceDifference() <= _bubbleDistance)
            {
                if(_isStrafing)
                {
                    //_rigid.MovePosition(transform.position + (transform.right *_baseSpeed * Time.deltaTime * _speedMod));
                    Strafe();
                }
                
            }
            else
            {
                float distance = Vector3.Distance(transform.position,_targetPos.position);
                if (distance >= 5f)
                {
                    _speedMod = 1f;
                }
                else
                {
                    //_speedMod = Mathf.Lerp(0.5f,1f,1f - distance/5f);
                    _speedMod = 0.5f;
                }
                _anim.SetFloat("moveMagnitude",_speedMod);
                //transform.position = Vector3.MoveTowards(transform.position, _targetPos.position, _baseSpeed * Time.deltaTime * _speedMod);
                _rigid.MovePosition(Vector3.MoveTowards(transform.position, _targetPos.position, _baseSpeed * Time.deltaTime * _speedMod));
            }
            _anim.SetBool("isMoving",true);
            _canMove = true;
        }
        // else if (angleToTarget >= 90f)
        // {
        //     _anim.Play("TurnAround");
        //     _turningAround = StartCoroutine(TurnAroundTimer());
        //     _canMove = false;
            

        // }
        else
        {
            Debug.Log("Need to rotate in range");
            _canMove = false;
            _anim.SetBool("isMoving",false);
        }
        
        
    }
}