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
    private bool _isRotating;
    private Quaternion _beg;
    private Quaternion _end;
    // Use this for initialization
    void Start () {
        _rotateAngel = endAngel - startAngel;
        _rotateSpeed = _rotateAngel / rotateTime;
	}
	
	// Update is called once per frame
	void Update () {
        if(!_isRotating)
        {
            return;
        }

        transform.Rotate(transform.forward, _rotateSpeed * Time.deltaTime);

        if (!isRotateLoop)
        {
            transform.eulerAngles = new Vector3(0, 0, endAngel);    
        }
        
    }

    public void RotateStart()
    {
        _isRotating = true;
    }
}
