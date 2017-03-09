using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownCircle : MonoBehaviour
{
    public float moveSpeed = 6;
    public float bottomPosition = -16.25f;
    public float topPosition = -8.75f;
    public float pauseAtBotton = 0;
    public float pauseAtTop = 0;
    private Vector3 _moveVec;
    private bool _ismoving = true;
    private bool _atBottom = false;
    private Transform _targetParent;
    private Vector3 _targetSourceScale;
    private bool _isPause = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(_isPause) return;

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
            transform.position = new Vector3(transform.position.x, bottomPosition, transform.position.z);
            _ismoving = false;
            _atBottom = true;
            doPauseAtBottom();
        }
    }


    void doPauseAtBottom()
    {
        StartCoroutine(movePause(pauseAtBotton));
    }

    void doPauseAtTop()
    {
        StartCoroutine(movePause(pauseAtTop));
    }

    IEnumerator movePause(float seconds)
    {
        _isPause = true;
        yield return new WaitForSeconds(seconds);
        _isPause = false;
    }

    void DoActivateTrigger()
    {
        _ismoving = true;
    }
}
