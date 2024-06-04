using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerForces))]
public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private float turnSmoothTime = 0.1f;
    [SerializeField]private float _baseMoveSpeed = 10f, _speedMod = 10f;
    [SerializeField]private Transform _camera;

    PlayerForces _playerForces;

    public Action<bool> setMove;

    private bool _canMove = true, _canTurn = true;


    private float _horizontal = 0f, _vertical = 0f;
    private float _targetAngle = 0f, _angle = 0f,_turnSmoothVelocity;
    private Vector3 _direction = Vector3.zero, _moveDirection = Vector3.zero;
    Vector2 _moveVec;
    private Rigidbody _rigid;

    private Animator _anim;

    // Called by player variable to receive the animator and rigidbody
    public void Initialize(Animator anim, Rigidbody rigid)
    {
        _anim = anim;
        _rigid = rigid;
    }

    private void OnCollisionStay(Collision col)
    {
        if(col.gameObject.CompareTag("Enemy") && transform.position.y > col.transform.position.y)
        {
            _canMove = false;
            _rigid.AddForce(-transform.forward * 50f);// Adds repulsion force so player doesn't get stuck on top
        }
    }

    private void OnCollisionExit(Collision col)
    {
        if(col.gameObject.CompareTag("Enemy"))
        {
            _canMove = true;
        }
    }

    void Start()
    {
        setMove = SetMovement;
        _camera = Camera.main.transform;
        _playerForces = GetComponent<PlayerForces>();
        // _control = new Controls();

        // if (_control != null)
        // {
        //     Debug.Log("Successfully initialized control");
        // }

        GetComponent<PlayerVariables>().controls.Combat.Move.performed += ctx => SettingMoveVec(ctx);
        GetComponent<PlayerVariables>().controls.Combat.Move.canceled += ctx => _direction = Vector2.zero;
    }

    // Used commonly by the delegate to lock player in place or unlock
    public void SetMovement(bool moveSet)
    {
        _canMove = moveSet;
    }

    private void SettingMoveVec(InputAction.CallbackContext ctx)
    {
        Debug.Log("Setting movement");
        Vector2 input = ctx.ReadValue<Vector2>();
        _direction = new Vector3(input.x,0f,input.y);
        
    }

    // private void OnMove(InputValue input)
    // {
    //     //Debug.Log("Input on joystick is "+input.Get<Vector2>());
    //     _direction = new Vector3(input.Get<Vector2>().x,0f,input.Get<Vector2>().y);
    // }

    void FixedUpdate()
    {
        // _horizontal = Input.GetAxisRaw("Horizontal");
        // _vertical = Input.GetAxisRaw("Vertical");

        //_direction = new Vector3(_horizontal,0f,_vertical).normalized;

        
        // if (_control == null)
        //     _direction = new Vector3(_horizontal,0f,_vertical).normalized;
        
        Move();
    }

    void Move()
    {
        if (_direction.magnitude >= 0.1f && _canMove)
        {
            _anim.SetBool("isMoving",true);// Transition to move state

            // Determine angle to be facing based on camera direction and input direction
            _targetAngle = Mathf.Atan2(_direction.x,_direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            _angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,_targetAngle,ref _turnSmoothVelocity, turnSmoothTime);
            
            // Generate move direction using character's current forward
            _moveDirection = Quaternion.Euler(0f,_targetAngle,0f) * Vector3.forward;
            
            if (_playerForces._isOnSlope)
            {
                _moveDirection = _playerForces.GetSlopeMoveDirection(_moveDirection);
            }

            // Move based on rigid body and movement speed and rotate them accordingly
            //_rigid.AddForce(_moveDirection.normalized * _baseMoveSpeed * _speedMod, ForceMode.Force);
            _rigid.MovePosition(transform.position + (_baseMoveSpeed * Time.deltaTime * _moveDirection.normalized));
            transform.rotation = Quaternion.Euler(0f,_angle,0f);
        }
        else
        {
            // Transition out of movement if movement input is less than 0.1 or if the player can't move
            _anim.SetBool("isMoving",false);
        }
    }
}
