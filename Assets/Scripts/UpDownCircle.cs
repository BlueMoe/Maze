using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownCircle : MonoBehaviour
{
    private float _moveSpeed = 4;
    private float _startTime;
    public float _bottomPosition = -16.25f;
    public float _topPosition = -8.75f;
    private Vector3 _moveVec;
    private bool _ismoving = true;
    private bool _atBottom = false;
    private Transform _targetParent;
    private Vector3 _targetSourceScale;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_atBottom)
        {
            moveToTop();
        }
        else
        {
            moveToBottom();
        }
    }

    void moveToTop()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, _moveSpeed, 0);
        transform.Translate(new Vector3(0, _moveSpeed, 0) * Time.deltaTime);
        if (transform.position.y >= _topPosition)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.position = new Vector3(transform.position.x,_topPosition,transform.position.z);
            _ismoving = false;
            _atBottom = false;
        }
    }

    void moveToBottom()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, -_moveSpeed, 0);
        transform.Translate(new Vector3(0, -_moveSpeed, 0) * Time.deltaTime);
        if (transform.position.y <= _bottomPosition)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.position = new Vector3(transform.position.x, _bottomPosition, transform.position.z);
            _ismoving = false;
            _atBottom = true;
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
