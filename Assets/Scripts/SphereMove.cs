using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class SphereMove : MonoBehaviour {

    public List<GameObject> controlButtons = new List<GameObject>();
    public GameObject block;
    public GameObject originSlot;
    public GameObject targetSlot;
    public Vector3 targetPosition;
    public Vector3 originPosition;
    public float sphereSpeed = 5;


    private int _DownCount;
    private Rigidbody _rigidBody;
    private bool _back = false;
    private Vector3 _rotationAxis;
    private Vector3 _move;
    private Vector3 _v = Vector3.zero;
    private Vector3 _angularv = Vector3.zero;
    private AudioSource _audio;
    // Use this for initialization
    void Start () {
        _rigidBody = GetComponent<Rigidbody>();
        _move = targetPosition - originPosition;
        _rotationAxis = Vector3.Cross((targetPosition - originPosition), Vector3.up);
        _audio = GetComponent<AudioSource>();
        
    }
	
	// Update is called once per frame
	void Update () {
        _rigidBody.angularVelocity = _angularv;
        _rigidBody.velocity = _v;
    }

    void DoActivateTrigger()
    {
        _DownCount++;
        if (_DownCount == controlButtons.Count)
        {
            moveToTargetPosition();    
        }
    }

    void moveToTargetPosition()
    {
        unLockPosition();
        Vector3 move = Vector3.ProjectOnPlane(targetPosition - originPosition,Vector3.up);
        _angularv= Vector3.Cross(move,Vector3.up).normalized * -sphereSpeed;
        _v = move.normalized * sphereSpeed;  
    }

    void moveBackToSourcePosition()
    {
        unLockPosition();
        _angularv = _rotationAxis * -sphereSpeed;
        _v = _move.normalized * -sphereSpeed;
    }
    void resetButtons()
    {
        for(int i = 0;i< controlButtons.Count;i++)
        {
            var activateTrigger = controlButtons[i].GetComponentInChildren<ActivateTrigger>();
            activateTrigger.triggerCount = 1;
            controlButtons[i].GetComponentInChildren<ButtonEvents>().setButtonUp();            
        }
        _DownCount = 0;
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == block)
        {
            moveBackToSourcePosition();
            resetButtons();
        }
        if(collision.gameObject == originSlot)
        {
            transform.position = originPosition;
            lockPosition();
            resetButtons();
        }
        if(collision.gameObject == targetSlot)
        {
            transform.position = targetPosition;
            lockPosition();
        }
    }
    void lockPosition()
    {
        _rigidBody.constraints = RigidbodyConstraints.FreezeAll;
        _angularv = Vector3.zero;
        _v = Vector3.zero;
        _audio.Stop();
    }
    void unLockPosition()
    {
        _rigidBody.constraints = RigidbodyConstraints.None;
        if (_move.x == 0)
        {
            _rigidBody.constraints = _rigidBody.constraints | RigidbodyConstraints.FreezePositionX;
        }
        if (_move.y == 0)
        {
            _rigidBody.constraints = _rigidBody.constraints | RigidbodyConstraints.FreezePositionY;
        }
        if (_move.z == 0)
        {
            _rigidBody.constraints = _rigidBody.constraints | RigidbodyConstraints.FreezePositionZ;
        }
        if (_rotationAxis.x == 0)
        {
            _rigidBody.constraints = _rigidBody.constraints | RigidbodyConstraints.FreezeRotationX;
        }
        if (_rotationAxis.y == 0)
        {
            _rigidBody.constraints = _rigidBody.constraints | RigidbodyConstraints.FreezeRotationY;
        }
        if (_rotationAxis.z == 0)
        {
            _rigidBody.constraints = _rigidBody.constraints | RigidbodyConstraints.FreezeRotationZ;
        }
        _audio.Play();
    }
}
