using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveController : MonoBehaviour {

    private Animator _animator;
    private Rigidbody _rigidbody;
    private float _forwardAmount;
    private float _turnAmount;
    private bool _isGrounded;
    private Vector3 _moveNormal;
    private float _OrigGroundCheckDistance;
    private CharacterController _characterContorller;
    private float _JumpAmount;

    public float moveSpeed = 5.0f;
    public float rotateSpeed = 90.0f;
    public float jumpPower = 10.0f;
    public float groundCheckDistance = 0.15f;
    public float gravity = 10.0f;
    // Use this for initialization
    void Start () {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _OrigGroundCheckDistance = groundCheckDistance;
        _characterContorller = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        _forwardAmount = 0;
        _turnAmount = 0;
        _isGrounded = true;
        if (Input.GetKey(KeyCode.W))
        {
            _forwardAmount = moveSpeed;
        }
        if(Input.GetKey(KeyCode.S))
        {
            _forwardAmount = -moveSpeed/2;
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
            _JumpAmount = 0;
            if (Input.GetKey(KeyCode.Space))
            {
                _JumpAmount += jumpPower;
            }
        }
        else
        {
            _JumpAmount -= gravity * Time.deltaTime;
        }
        var move = new Vector3(0, _JumpAmount, _forwardAmount);
        move = transform.TransformDirection(move);
        if(move != Vector3.zero)
        {
            //_characterContorller.Move(move*Time.deltaTime);
            GetComponent<Rigidbody>().AddForce(move*Time.deltaTime,ForceMode.VelocityChange);
        }
        updateAnimator();
        rotateCharacter();
    }
    void updateAnimator()
    {
        if(_forwardAmount >= 0)
        { 
            _animator.SetFloat("Forward", _forwardAmount);
        }
        else
        {
            _animator.SetFloat("Forward", 0.5f);
        }
        _animator.SetBool("OnGround", _isGrounded);
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
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, groundCheckDistance))
        {
            _moveNormal = hitInfo.normal;
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
            _moveNormal = Vector3.up;
        }
    }

    void rotateCharacter()
    {
        transform.Rotate(transform.up, _turnAmount * Time.deltaTime);
    }
}
