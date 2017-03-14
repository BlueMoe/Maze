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
    private bool _sliding;
    private bool _needRotate;
    private float _forwardAmount;
    private float _turnAmount;
    private float _JumpAmount;
    private float _radius = 0.3f;
    private float _airTime = 0;
    private CapsuleCollider _collider;
    private CharacterController _characterController;
    private Rigidbody _rigidBody;
    private Vector3 _moveNormal;
    private Vector3 _externalVelocity = Vector3.zero;
    private Vector3 _pos;
    private Vector3 _fallDirection;
    private Transform _camera;


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
        _camera = Camera.main.transform;

    }

    void FixedUpdate()
    {
        _forwardAmount = 0;
        _turnAmount = 0;
        _isGrounded = true;
        _needRotate = true;
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");

        checkGrounded();

        var forward = Vector3.ProjectOnPlane(_camera.forward, _moveNormal).normalized * v;
        var right = Vector3.ProjectOnPlane(_camera.right, _moveNormal).normalized * h;

        var moveDirection = forward + right;

        

        if (_isGrounded)
        {
            _JumpAmount = 0;
            _airTime = 0;
            if (CrossPlatformInputManager.GetButton("Jump"))
            {
                _JumpAmount += jumpPower;
            }
        }
        else
        {
            _airTime += Time.deltaTime;
            _JumpAmount -= gravity * Time.deltaTime;
        }


        moveDirection = Vector3.ProjectOnPlane(moveDirection, _moveNormal).normalized * moveDirection.magnitude;
        var move = moveDirection * moveSpeed;

        if(_sliding)
        {
            move.y = 0;
            _needRotate = false;
        }

        var fall = Vector3.up * _JumpAmount;
        move += fall;

        moveCharacter(move);
        updateAnimator(move);
        fixedPosition();
    }
    void updateAnimator(Vector3 move)
    {
        _animator.SetFloat("Forward", move.magnitude);
        //避免经过某些不平整地面造成的骨骼动画播放错误
        if (_airTime > 0.05f)
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
        _moveNormal = checkMoveNormal();

        _isGrounded = checkSlopeAngle(_moveNormal);  
    }

    //从角色位置周围9个点向下作线段检测与平面交点的法向量,如果没有交点返回Vector3.zero
    Vector3 checkMoveNormal()
    {
        RaycastHit hitInfo;
        Vector3 vec;
        Vector3 normal = Vector3.zero;
        int hitPointCount = 0;
        for (int row = -1; row <= 1; row++)
        {
            for (int col = -1; col <= 1; col++)
            {
                vec = transform.right * row * groundCheckDistance + transform.forward * col * groundCheckDistance;
                if (raycast(vec, out hitInfo))
                {
                    hitPointCount++;
                    normal += hitInfo.normal;
                }
            }
        }
        if (hitPointCount == 0)
        {
            return Vector3.zero;
        }
        else
        {
            normal /= hitPointCount;
        }

        return normal;
    }

    //检测一条由角色位置+vec开始,方向向下的线段与碰撞盒的交点,检测距离为groundCheckDistance
    bool raycast(Vector3 vec, out RaycastHit hitInfo)
    {
        float offset = 0.5f;
        bool ret = Physics.Raycast(transform.position + Vector3.up * offset + vec, Vector3.down, out hitInfo, groundCheckDistance + offset);
        return ret;
    }

    bool checkSlopeAngle(Vector3 slopeNormal)
    {
        //在空中,不属于下滑的范畴
        if (slopeNormal == Vector3.zero)
        {
            _sliding = false;
            return false;
        }

        //竖直方向与平面法向量夹角小于slopeAngelLimit,忽略下落
        if (Vector3.Dot(Vector3.up, slopeNormal) / (Vector3.up.magnitude * slopeNormal.magnitude) >= Mathf.Cos(slopeAngelLimit * Mathf.Deg2Rad))
        {
            _sliding = false;
            return true;
        }
        //竖直方向与平面法向量夹角大于slopeAngelLimit,开始下滑
        else
        {
            _sliding = true;
            return false;
        }
    }

    void OnDrawGizmos()
    {
        Vector3 vec;
        for (int row = -1; row <= 1; row++)
        {
            for (int col = -1; col <= 1; col++)
            {
                vec = transform.right * row * 0.1f + transform.forward * col * 0.1f;
                Gizmos.DrawLine(transform.position + vec, transform.position + vec + Vector3.down * groundCheckDistance);
            }
        }
    }


    void rotateCharacter()
    {
        transform.Rotate(transform.up, _turnAmount * Time.deltaTime);
    }

    void moveCharacter(Vector3 move)
    {

        if (Vector3.ProjectOnPlane(move, Vector3.up).magnitude > 0 && _needRotate)
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
        fixedPositionBySphere(transform.position + transform.up * 0.3f, _radius);
        fixedPositionBySphere(transform.position + transform.up * 0.6f, _radius);
        fixedPositionBySphere(transform.position + transform.up * 0.9f, _radius);
    }

    void fixedPositionBySphere(Vector3 pos, float radius)
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
