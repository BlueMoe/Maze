using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{

    private Animator _animator;
    private Rigidbody _rigidBody;
    private float _forwardAmount;
    private float _turnAmount;
    private bool _isGrounded;
    private Vector3 _moveNormal;
    private Vector3 _fallNormal;
    private float _OrigGroundCheckDistance;
    private CharacterController _characterController;
    private ActionModeController _actionModeController;
    private float _JumpAmount;
    private ActionModeController.ActionMode _myMode;
    private Vector3 _relativeVelocity;
    private Vector3 _externalVelocity;
    private float _externalVelocityTime;
    private Transform _parent;
    private float _airTime;

    public float moveSpeed = 5.0f;
    public float rotateSpeed = 90.0f;
    public float jumpPower = 7.0f;
    public float groundCheckDistance = 0.15f;
    public float gravity = 20.0f;
    public float slopeAngelLimit = 80;

    public void setExternalVelocity(Vector3 v, float seconds)
    {
        _externalVelocity = v;
        _externalVelocityTime = seconds;
    }


    // Use this for initialization
    void Start()
    {
        _actionModeController = GetComponent<ActionModeController>();
        _actionModeController.changeMode(ActionModeController.ActionMode.RIGIDBODYMODE);
        _myMode = _actionModeController.getActionMode();
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody>();
        _OrigGroundCheckDistance = groundCheckDistance;
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _forwardAmount = 0;
        _turnAmount = 0;
        _isGrounded = true;

        if (Input.GetKey(KeyCode.W))
        {
            _forwardAmount = moveSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _forwardAmount = -moveSpeed / 2;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _turnAmount = -rotateSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _turnAmount = rotateSpeed;
        }
        checkGrounded();

        if (_isGrounded)
        {
            _JumpAmount = -0.1f;
            if (Input.GetKey(KeyCode.Space))
            {
                _airTime += 0.1f;
                _JumpAmount += jumpPower;
            }
        }
        else
        {
            _JumpAmount -= gravity * Time.deltaTime;
        }

        var moveDirection = new Vector3(0, 0, 1);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = Vector3.ProjectOnPlane(moveDirection, _fallNormal).normalized;
        var move = moveDirection * _forwardAmount;
        var vertical = Vector3.up * _JumpAmount;

        //平面法向量就是竖直方向,正常处理跳跃和自由下落
        if(Vector3.Dot(Vector3.up, _fallNormal) == Mathf.Cos(0))
        {
            move += vertical;
        }
        //竖直方向与平面法向量夹角大于slopeAngelLimit,开始下滑
        else if (Vector3.Dot(Vector3.up,_fallNormal) < Mathf.Cos(slopeAngelLimit / 180 * Mathf.PI))
        {
            move = vertical;
        }
        //竖直方向与平面法向量夹角小于slopeAngelLimit,忽略下落
        else
        {
            if(Vector3.Dot(vertical,Vector3.down) >= 0)
            {
                vertical = Vector3.zero;
            }
            move += vertical;
        }
        moveCharacter(move);
        updateAnimator();
        rotateCharacter();

        transform.parent = _parent;
    }
    void updateAnimator()
    {
        if (_forwardAmount >= 0)
        {
            _animator.SetFloat("Forward", _forwardAmount);
        }
        else
        {
            _animator.SetFloat("Forward", 0.5f);
        }
        if(_airTime > 0.1 && _fallNormal == Vector3.up)
        { 
            _animator.SetBool("OnGround", false);
        }
        else
        {
            _animator.SetBool("OnGround", true);
        }
        if (!_isGrounded)
        {
            _animator.SetFloat("Jump", _JumpAmount);
        }
        float runCycle = Mathf.Repeat(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime + 0.2f, 1);
        float jumpLeg = (runCycle < 0.5 ? 1 : -1) * _forwardAmount;
        if (_isGrounded)
        {
            _animator.SetFloat("JumpLeg", jumpLeg);
        }
    }

    void checkGrounded()
    {
        RaycastHit hitInfo;
        float offset = 0.5f;
        if (Physics.Raycast(transform.position + Vector3.up * offset, Vector3.down, out hitInfo, groundCheckDistance + offset))
        {
            _moveNormal = hitInfo.normal;
            _fallNormal = hitInfo.normal;
            _isGrounded = true;
            _airTime = 0;
        }
        else
        {
            _isGrounded = false;
            _airTime += Time.deltaTime;
            _moveNormal = Vector3.up;
            if (Physics.Raycast(transform.position, Vector3.down, out hitInfo, groundCheckDistance + 0.1f))
            {
                _fallNormal = hitInfo.normal;
            }
            else
            {
                _fallNormal = Vector3.up;
            }
        }
        
    }

    void OnDrawGizmos()
    {
        //Gizmos.DrawLine(transform.position, transform.position);

    }


    void rotateCharacter()
    {
        transform.Rotate(transform.up, _turnAmount * Time.deltaTime);
    }

    void moveCharacter(Vector3 move)
    {

        var curMode = _actionModeController.getActionMode();
        if (_myMode != curMode)
        {
            //在模式切换后,部分组件已经被删除 初始化_characterController或_rigidBody的值
            _myMode = curMode;
            if (curMode == ActionModeController.ActionMode.CHARACTERCONTROLLERMODE)
            {
                _characterController = GetComponent<CharacterController>();
            }
            else if (curMode == ActionModeController.ActionMode.RIGIDBODYMODE)
            {
                _rigidBody = GetComponent<Rigidbody>();
            }
        }

        //角色控制器模式和刚体模式使用不同的移动方式
        if (_myMode == ActionModeController.ActionMode.CHARACTERCONTROLLERMODE)
        {
            _characterController.Move(move * Time.deltaTime);
        }
        else if (_myMode == ActionModeController.ActionMode.RIGIDBODYMODE)
        {
            _rigidBody.velocity = move;
            _rigidBody.velocity += _relativeVelocity;
            if (_externalVelocityTime > 0)
            {
                _rigidBody.velocity += _externalVelocity;
                _rigidBody.velocity = new Vector3(_rigidBody.velocity.x, 0, _rigidBody.velocity.z);
                _externalVelocityTime -= Time.deltaTime;

            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        _rigidBody.velocity = Vector3.zero;
    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.rigidbody != null)
            _relativeVelocity = collision.rigidbody.velocity;
    }
    void OnCollisionExit(Collision collision)
    {
        _relativeVelocity = Vector3.zero;
    }
    void OnControllerColliderHit(ControllerColliderHit col)
    {
    }
}
