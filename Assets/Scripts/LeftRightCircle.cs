using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightCircle : MonoBehaviour
{
    public float _moveSpeed = 8;
    public float _leftPosition = -16.25f;
    public float _rightPosition = -8.75f;
    private float _startTime;
    private Vector3 _moveVec;
    private bool _ismoving = true;
    private bool _atLeft = false;
    private Transform _targetParent;
    private Vector3 _targetSourceScale;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_atLeft)
        {
            moveToRight();
        }
        else
        {
            moveToLeft();
        }
    }

    void moveToRight()
    {
        transform.Translate(new Vector3(0, 0, _moveSpeed) * Time.deltaTime);
        if (transform.position.z >= _leftPosition)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, _leftPosition);
            _ismoving = false;
            _atLeft = false;
        }
    }

    void moveToLeft()
    {
        transform.Translate(new Vector3(0, 0, -_moveSpeed) * Time.deltaTime);
        if (transform.position.z <= _rightPosition)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, _rightPosition);
            _ismoving = false;
            _atLeft = true;
        }
    }


    void DoActivateTrigger()
    {
        _ismoving = true;
        _startTime = Time.time;
    }
}
