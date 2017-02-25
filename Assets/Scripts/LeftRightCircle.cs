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
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, _moveSpeed);
        transform.Translate(new Vector3(0, 0, _moveSpeed) * Time.deltaTime);
        if (transform.position.z >= _leftPosition)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.position = new Vector3(transform.position.x, transform.position.y, _leftPosition);
            _ismoving = false;
            _atLeft = false;
        }
    }

    void moveToLeft()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -_moveSpeed);
        transform.Translate(new Vector3(0, 0, -_moveSpeed) * Time.deltaTime);
        if (transform.position.z <= _rightPosition)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
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

    private void OnCollisionEnter(Collision collision)
    {
        var scale = transform.localScale;
        _targetParent = collision.gameObject.transform.parent;
        _targetSourceScale = collision.gameObject.transform.localScale;
        collision.gameObject.transform.parent = transform;
        collision.gameObject.transform.localScale = new Vector3(1.0f / scale.x * _targetSourceScale.x,
                                                                1.0f / scale.y * _targetSourceScale.y,
                                                                1.0f / scale.z * _targetSourceScale.z);
    }
    private void OnCollisionExit(Collision collision)
    {
        collision.gameObject.transform.parent = _targetParent;
        collision.gameObject.transform.localScale = _targetSourceScale;
    }
}
