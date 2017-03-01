﻿using System.Collections;
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
    // Use this for initialization
    void Start () {
        _rigidBody = GetComponent<Rigidbody>();
        _move = targetPosition - originPosition;
        _rotationAxis = Vector3.Cross((targetPosition - originPosition), Vector3.up);
    }
	
	// Update is called once per frame
	void Update () {
        
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
        Vector3 move = targetPosition - originPosition;
        _rigidBody.angularVelocity = Vector3.Cross(move,Vector3.up).normalized * sphereSpeed;
        _rigidBody.velocity = move.normalized * sphereSpeed;  
    }

    void moveBackToSourcePosition()
    {
        unLockPosition();
        _rigidBody.angularVelocity = _rotationAxis * -sphereSpeed;
        _rigidBody.velocity = _move.normalized * -sphereSpeed;
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
    }
}
