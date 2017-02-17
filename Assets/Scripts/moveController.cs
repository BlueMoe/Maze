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

    public float moveSpeed = 5.0f;
    public float rotateSpeed = 5.0f;
    public float _GroundCheckDistance = 0.1f;
    // Use this for initialization
    void Start () {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _OrigGroundCheckDistance = _GroundCheckDistance;
    }
	
	// Update is called once per frame
	void Update () {
        _forwardAmount = 0;
        _turnAmount = 0;
        _isGrounded = true;
        //_rigidbody.velocity = Vector3.zero;
		if(Input.GetKey(KeyCode.W))
        {
            _forwardAmount = moveSpeed;
        }
        if(Input.GetKey(KeyCode.S))
        {
            _forwardAmount = -moveSpeed;
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
        updateAnimator();
        
        if(_isGrounded)
        {
            moveAtGround();
        }
        else
        {
            moveAtAir();
        }
        
        


    }
    void updateAnimator()
    {
        _animator.SetFloat("Forward", Mathf.Abs(_forwardAmount), 0.1f, Time.deltaTime);
        _animator.SetBool("OnGround", _isGrounded);
        if (!_isGrounded)
        {
            _animator.SetFloat("Jump", _rigidbody.velocity.y);
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
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, _GroundCheckDistance))
        {
            _moveNormal = hitInfo.normal;
            _isGrounded = true;
            _animator.applyRootMotion = true;
        }
        else
        {
            _isGrounded = false;
            _moveNormal = Vector3.up;
            _animator.applyRootMotion = false;
        }
    }

    void moveAtGround()
    {
        var move = transform.forward + transform.right;
        move = Vector3.ProjectOnPlane(move, _moveNormal);
        move *= _forwardAmount;
        _rigidbody.velocity = move;
    }

    void moveAtAir()
    {
        _rigidbody.AddForce(Physics.gravity);
        //_GroundCheckDistance = _rigidbody.velocity.y < 0 ? _OrigGroundCheckDistance : 0.01f;
    }
}
