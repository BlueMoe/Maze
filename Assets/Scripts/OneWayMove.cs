using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayMove : MonoBehaviour
{
    public float _moveSpeed = 6;
    public Vector3 _bottomPosition = new Vector3(0, -27.5f, 0);
    public Vector3 _topPosition = new Vector3(0, 0, 0);
    private bool _ismoving = false;
    public bool _atBottom = false;
    private Transform _targetParent;
    private Vector3 _targetSourceScale;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_ismoving == false)
        {
            return;
        }

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
        if(transform.position.y >= _topPosition.y)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.position = _topPosition;
            _ismoving = false;
            _atBottom = false;
        }
    }

    void moveToBottom()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, -_moveSpeed, 0);
        transform.Translate(new Vector3(0, -_moveSpeed, 0) * Time.deltaTime);
        if (transform.position.y <= _bottomPosition.y)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.position = _bottomPosition;
            _ismoving = false;
            _atBottom = true;
        }
    }


    void DoActivateTrigger()
    {
        _ismoving = true;
    }
}
