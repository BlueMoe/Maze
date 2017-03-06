using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRotate : MonoBehaviour {

    public float startAngel;
    public float endAngel;
    public float rotateTime;
    public bool isRotateLoop;

    private float _rotateAngel;
    private float _rotateSpeed;
    private Vector3 _originEulerAngles;
    private bool _isRotating;
    private float _rotateTime;
    // Use this for initialization
    void Start () {
        _rotateAngel = endAngel - startAngel;
        _rotateSpeed = _rotateAngel / rotateTime;
        _originEulerAngles = transform.eulerAngles;
        _rotateTime = rotateTime;
    }
	
	// Update is called once per frame
	void Update () {
        if(!_isRotating)
        {
            return;
        }

        transform.Rotate(0, 0, _rotateSpeed * Time.deltaTime);
        _rotateTime -= Time.deltaTime;
        
        if (!isRotateLoop && _rotateTime < 0)
        {
            transform.eulerAngles = new Vector3(_originEulerAngles.x, _originEulerAngles.y , endAngel);    
        }
        
    }
    
    void DoActivateTrigger()
    {
        RotateStart();
    }

    public void RotateStart()
    {
        _isRotating = true;
    }

    
}
