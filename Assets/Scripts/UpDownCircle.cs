using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownCircle : MonoBehaviour
{
<<<<<<< HEAD
    public float moveSpeed = 6;
    public float bottomPosition = -16.25f;
    public float topPosition = -8.75f;
    public float pauseAtBotton = 0;
    public float pauseAtTop = 0;
=======
    private float _moveSpeed = 6;
    public float _bottomPosition = -16.25f;
    public float _topPosition = -8.75f;
>>>>>>> ba401d3991d92af7fc6d4e9b1ee56ffa8957af1f
    private Vector3 _moveVec;
    private bool _ismoving = true;
    private bool _atBottom = false;
    private Transform _targetParent;
    private Vector3 _targetSourceScale;
    private float _pause = 0;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(_pause > 0)
        {
            _pause -= Time.deltaTime;
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
        GetComponent<Rigidbody>().velocity = new Vector3(0, moveSpeed, 0);
        transform.Translate(new Vector3(0, moveSpeed, 0) * Time.deltaTime);
        if (transform.position.y >= topPosition)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.position = new Vector3(transform.position.x,topPosition,transform.position.z);
            _ismoving = false;
            _atBottom = false;
            doPauseAtTop();
        }
    }

    void moveToBottom()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, -moveSpeed, 0);
        transform.Translate(new Vector3(0, -moveSpeed, 0) * Time.deltaTime);
        if (transform.position.y <= bottomPosition)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.position = new Vector3(transform.position.x, bottomPosition, transform.position.z);
            _ismoving = false;
            _atBottom = true;
            doPauseAtBottom();
        }
    }


    void doPauseAtBottom()
    {
        _pause = pauseAtBotton;
    }

    void doPauseAtTop()
    {
        _pause = pauseAtTop;
    }

    void DoActivateTrigger()
    {
        _ismoving = true;
    }
}
