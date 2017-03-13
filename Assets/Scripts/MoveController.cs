using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MoveController : MonoBehaviour
{

    public float moveSpeed = 5.0f;
    public float rotateSpeed = 90.0f;
    public float jumpPower = 7.0f;
    public float groundCheckDistance = 0.15f;
    public float gravity = 20.0f;
    public float slopeAngelLimit = 80;

    private Animator _animator;
    private ActionModeController _actionModeController;
    private ActionModeController.ActionMode _myMode;
    private bool _hasExternalVelocity;
    private bool _isGrounded;
    private float _forwardAmount;
    private float _turnAmount;
    private float _OrigGroundCheckDistance;
    private float _JumpAmount;
    private float _airTime;
    private float _radius = 0.3f;
    private CapsuleCollider _collider;
    private CharacterController _characterController;
    private Rigidbody _rigidBody;
    private Vector3 _fallNormal;
    private Vector3 _moveNormal;
    private Vector3 _externalVelocity = Vector3.zero;
    private Vector3 _pos;
    private Transform _camera;
    private bool _needRotateAmbra = true;
    private float x = 0;
    public void addExternalVelocity(Vector3 v)
    {
        _externalVelocity += v;
    }
    public void removeExternalVelocity(Vector3 v)
    {
        _externalVelocity -= v;
    }

    void Start()
    {
        _actionModeController = GetComponent<ActionModeController>();
        _actionModeController.changeMode(ActionModeController.ActionMode.RIGIDBODYMODE);
        _myMode = _actionModeController.getActionMode();
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
        _OrigGroundCheckDistance = groundCheckDistance;
        _camera = Camera.main.transform;

    }

    void FixedUpdate()
    {
        _forwardAmount = 0;
        _turnAmount = 0;
        _isGrounded = true;
        _needRotateAmbra = true;
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");

        var forward = Vector3.ProjectOnPlane(_camera.forward, Vector3.up).normalized * v;
        var right = Vector3.ProjectOnPlane(_camera.right, Vector3.up).normalized * h;

        var moveDirection = forward + right;

        checkGrounded();

        if (_isGrounded)
        {
            x = 0;
            _JumpAmount = 0;
            if (CrossPlatformInputManager.GetButton("Jump"))
            {
                _airTime += 0.1f;
                _JumpAmount += jumpPower;
            }
        }
        else
        {
            x += Time.deltaTime;
            _JumpAmount -= gravity * Time.deltaTime;
            if (_JumpAmount < -gravity*2) _JumpAmount = -gravity * 2;
        }

        Debug.Log(x);
        moveDirection = Vector3.ProjectOnPlane(moveDirection, _fallNormal).normalized * moveDirection.magnitude;
        var move = moveDirection * moveSpeed;// * _forwardAmount;
        var vertical = Vector3.up * _JumpAmount;

        //平面法向量就是竖直方向,正常处理跳跃和自由下落
        if (Vector3.Dot(Vector3.up, _fallNormal)/Vector3.up.magnitude*_fallNormal.magnitude == Mathf.Cos(0))
        {
            move += vertical;
        }
        //竖直方向与平面法向量夹角大于slopeAngelLimit,开始下滑
        else if (Vector3.Dot(Vector3.up,_fallNormal) / Vector3.up.magnitude * _fallNormal.magnitude < Mathf.Cos(slopeAngelLimit / 180 * Mathf.PI))
        {
            vertical = Vector3.ProjectOnPlane(Vector3.down, _fallNormal).normalized * gravity / 2;
            move = vertical;
            _needRotateAmbra = false;
        }
        //竖直方向与平面法向量夹角小于slopeAngelLimit,忽略下落
        else
        {
            if (Vector3.Dot(vertical,Vector3.down) >= 0)
            {
                vertical = Vector3.zero;
            }
            move += vertical;
        }
        moveCharacter(move);
        updateAnimator(move);
        fixedPosition();
    }
    void updateAnimator(Vector3 move)
    {
        _animator.SetFloat("Forward", move.magnitude - 1);
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
        float jumpLeg = (runCycle < 0.5 ? 1 : -1) * move.magnitude;
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
            _isGrounded = true;
            _airTime = 0;
        }
        else if(Physics.Raycast(transform.position + Vector3.up * offset + transform.forward *0.1f, Vector3.down, out hitInfo, groundCheckDistance + offset))
        {
            _moveNormal = hitInfo.normal;
            _isGrounded = true;
            _airTime = 0;
        }
        else if(Physics.Raycast(transform.position + Vector3.up * offset + transform.forward * -0.1f, Vector3.down, out hitInfo, groundCheckDistance + offset))
        {
            _moveNormal = hitInfo.normal;
            _isGrounded = true;
            _airTime = 0;
        }
        else if (Physics.Raycast(transform.position + Vector3.up * offset + transform.right * 0.1f, Vector3.down, out hitInfo, groundCheckDistance + offset))
        {
            _moveNormal = hitInfo.normal;
            _isGrounded = true;
            _airTime = 0;
        }
        else if (Physics.Raycast(transform.position + Vector3.up * offset + transform.right * -0.1f, Vector3.down, out hitInfo, groundCheckDistance + offset))
        {
            _moveNormal = hitInfo.normal;
            _isGrounded = true;
            _airTime = 0;
        }
        else
        {
            _isGrounded = false;
            _airTime += Time.deltaTime;
            _moveNormal = Vector3.up;

        }
        if (Physics.Raycast(transform.position, Vector3.down, out hitInfo, groundCheckDistance + 0.1f))
        {
            _fallNormal = hitInfo.normal;
        }
        else
        {
            _fallNormal = Vector3.up;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + _fallNormal);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * (groundCheckDistance + 0.1f));
        Gizmos.DrawLine(transform.position + transform.forward * -0.1f, transform.position + transform.forward * -0.1f + Vector3.down * (groundCheckDistance/ 4));
        Gizmos.DrawLine(transform.position + transform.forward * 0.1f, transform.position + transform.forward * 0.1f + Vector3.down * (groundCheckDistance/4));
        Gizmos.DrawLine(transform.position + transform.right * -0.1f, transform.position + transform.right * -0.1f + Vector3.down * (groundCheckDistance / 4));
        Gizmos.DrawLine(transform.position + transform.right * 0.1f, transform.position + transform.right * 0.1f + Vector3.down * (groundCheckDistance / 4));
        if (_collider != null)
        {
            Gizmos.DrawWireSphere(transform.position + transform.up * 0.5f, _radius);
            Gizmos.DrawWireSphere(transform.position + transform.up * 0.8f, _radius);
        }
        if (_pos != null)
            Gizmos.DrawWireSphere(_pos, _radius);
    }


    void rotateCharacter()
    {
        transform.Rotate(transform.up, _turnAmount * Time.deltaTime);
    }

    void moveCharacter(Vector3 move)
    {

        if (Vector3.ProjectOnPlane(move,Vector3.up).magnitude > 0 && _needRotateAmbra)
        {
            var rotateAngle = Mathf.Atan2(move.z, move.x) * 180 / Mathf.PI;
            transform.eulerAngles = new Vector3(0, 90 - rotateAngle, 0);
        }
        if (_myMode == ActionModeController.ActionMode.RIGIDBODYMODE)
        {
            move += _externalVelocity;
            _rigidBody.velocity = move;
        }
    }

    //对角色的坐标进行补正,避免穿墙
    void fixedPosition()
    {
        fixedPositionBySphere(transform.position + transform.up * 0.5f, _radius);
        fixedPositionBySphere(transform.position + transform.up * 0.8f, _radius);
    }

    void fixedPositionBySphere(Vector3 pos,float radius)
    {
        foreach (Collider col in Physics.OverlapSphere(pos, radius))
        {
            Vector3 contactPoint = Vector3.zero;

            if (col.isTrigger == true) continue;

            if (col is BoxCollider)
            {
                contactPoint = ClosestPointOn((BoxCollider)col, transform.position);
            }
            else if (col is SphereCollider)
            {
                contactPoint = ClosestPointOn((SphereCollider)col, transform.position);
            }
            else if (col is MeshCollider)
            {
                contactPoint = ClosestPointOn((MeshCollider)col, transform.position);
            }

            Vector3 v = transform.position - contactPoint;

            transform.position += Vector3.ClampMagnitude(v, Mathf.Clamp(_radius - v.magnitude, 0, _radius));
        }
    }

    Vector3 ClosestPointOn(BoxCollider collider, Vector3 to)
    {
        if (collider.transform.rotation == Quaternion.identity)
        {
            return collider.ClosestPointOnBounds(to);
        }
        //因为ClosestPointOnBounds是依据AABB来计算最近点的,当局部坐标轴与世界坐标轴不重合的时候只能通过OBB来计算最近的补正点
        return closestPointOnOBB(collider, to);
    }

    Vector3 ClosestPointOn(SphereCollider collider, Vector3 to)
    {
        Vector3 p;
        //球型碰撞盒球心到目标的方向向量
        p = to - collider.transform.position;
        p.Normalize();
        //球型碰撞盒球心到球面上点的坐标
        p *= collider.radius * collider.transform.localScale.x;
        p += collider.transform.position;

        return p;
    }

    Vector3 ClosestPointOn(MeshCollider collider, Vector3 to)
    {
        return collider.ClosestPointOnBounds(to);
    }

    //计算物体的OBB表面最近的点
    Vector3 closestPointOnOBB(BoxCollider collider, Vector3 to)
    {	
        var ct = collider.transform;
	
        //把目标点的坐标转成碰撞盒的局部坐标
        var local = ct.InverseTransformPoint(to);
		
        //碰撞盒中心到目标点的向量
        local -= collider.center;
	
        //把中心到目标的向量限制在包围盒内
        var localNorm = new Vector3(
        Mathf.Clamp(local.x, -collider.size.x * 0.5f, collider.size.x * 0.5f),
        Mathf.Clamp(local.y, -collider.size.y * 0.5f, collider.size.y * 0.5f),
        Mathf.Clamp(local.z, -collider.size.z * 0.5f, collider.size.z * 0.5f));
	    
        localNorm += collider.center;
	
        return ct.TransformPoint(localNorm);
    }
    void OnCollisionEnter(Collision col)
    {
        _externalVelocity = Vector3.zero;
    }
}
