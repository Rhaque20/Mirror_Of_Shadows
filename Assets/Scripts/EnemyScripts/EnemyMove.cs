using System;
using System.Collections;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]protected Transform _targetPos;
    [Range(0.0f, 2*Mathf.PI)]
    [SerializeField]protected float _rotationSpeed = 3.14f;
    [SerializeField]protected float _baseSpeed = 2f;
    [SerializeField]protected float _bubbleDistance = 3f;
    [SerializeField]protected AnimationClip _turnAroundAnim;
    protected Rigidbody _rigid;
    protected Animator _anim;
    protected bool _canMove = true, _canRotate = true, _isStrafing = false;

    protected bool _forcedStop = false;
    protected Coroutine _turningAround;

    public Action<bool> setMove;
    
    protected float _speedMod = 0.5f;

    protected Vector3 _targetDir
    {
        get { return _targetPos.position - transform.position;}
    }

    protected Vector3 _targetDirGround
    {
        get{
            Vector3 temp = _targetPos.position - transform.position;
            temp.y = 0f;

            return temp;
        }
    }
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        _anim = transform.GetChild(0).GetComponent<Animator>();
        setMove = SetMovement;
    }

    public void SetMovement(bool moveSet)
    {
        _forcedStop = !moveSet;
        _canRotate = moveSet;
    }

    protected IEnumerator TurnAroundTimer()
    {
        yield return new WaitForSeconds(_turnAroundAnim.length + 1f);
        _turningAround = null;
    }

    public void SetIsStrafe(bool val)
    {
        _isStrafing = val;
    }

    protected float DistanceDifference()
    {
        
        Vector3 distanceDiff = _targetPos.position - transform.position;

        distanceDiff.y = 0;

        float finalDistance = distanceDiff.magnitude;

        return finalDistance;
    }

    protected virtual float HeightDifference()
    {
        
        return transform.position.y - _targetPos.position.y;
    }

    protected virtual void Strafe()
    {
        transform.RotateAround(_targetPos.position,Vector3.up,
        (float)((_baseSpeed * _speedMod)/((Mathf.PI/180)*(_bubbleDistance))) * Time.deltaTime);
        // Length of an Arc is Arc = Theta * (Pi/180) * radius.
        // Arc distance is the same as walking in straight line so use baseSpeed
        // Need theta for third argument of rotate around so rewrite arc formula to be
        // Theta = Arc/((Pi/180) * Radius) with Arc being baseSpeed and radius being bubble distance
    }

    protected virtual void Move()
    {
        float angleToTarget = Vector3.Angle(_targetDirGround,transform.forward);
        if (_turningAround != null)
        {
            return;
        }

        if (angleToTarget <= 45f)
        {
            if (DistanceDifference() <= _bubbleDistance + 0.1f)
            {
                Strafe();
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
                _rigid.MovePosition(transform.position + (transform.right *_baseSpeed * Time.deltaTime * _speedMod));
            }
            
            _canMove = true;
        }
        else if (angleToTarget >= 90f)
        {
            _anim.Play("TurnAround");
            _turningAround = StartCoroutine(TurnAroundTimer());
            _canMove = false;
            

        }
        else
        {
            Debug.Log("Need to rotate in range");
            _canMove = false;
        }
        
        _anim.SetBool("isMoving",(_rigid.velocity.magnitude >= 0.0f && _canMove));
    }

    protected void RotateToTarget()
    {
        Quaternion lookRotation = Quaternion.LookRotation(_targetDirGround);
        lookRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, lookRotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        transform.rotation =  Quaternion.Slerp(transform.rotation, lookRotation, _rotationSpeed * Time.deltaTime);

        Debug.Log("Rotating to target");
    }
    // Update is called once per frame
    protected void FixedUpdate()
    {
        if (_canRotate)
            RotateToTarget();
        if (!_forcedStop)
            Move();
        else
            _anim.SetBool("isMoving",false);
        
    }
}
