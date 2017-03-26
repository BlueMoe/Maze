using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class OneWayMove : MonoBehaviour
{
    public float _moveSpeed = 6;
    public Vector3 _bottomPosition = new Vector3(0, -27.5f, 0);
    public Vector3 _topPosition = new Vector3(0, 0, 0);
    public GameObject atTopResetButton;
    public GameObject atBottomResetButton;
    public bool _atBottom = false;
    public bool haveSound = false;

    private bool _ismoving = false;
    private Transform _targetParent;
    private Vector3 _targetSourceScale;

    private AudioSource _audio;
    // Use this for initialization
    void Start()
    {
        if(haveSound)
        {
            _audio = GetComponent<AudioSource>();
        }
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
            if (atTopResetButton != null)
            {
                var btnEvent = atTopResetButton.GetComponentInChildren<ButtonEvents>();
                var btnTrigger = atTopResetButton.GetComponentInChildren<ActivateTrigger>();
                if(btnEvent != null)
                {
                    btnEvent.setButtonUp();
                }
                if(btnTrigger != null)
                {
                    btnTrigger.triggerCount = 1;
                }
            }
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
            if (atBottomResetButton != null)
            {
                var btnEvent = atBottomResetButton.GetComponentInChildren<ButtonEvents>();
                var btnTrigger = atBottomResetButton.GetComponentInChildren<ActivateTrigger>();
                if (btnEvent != null)
                {
                    btnEvent.setButtonUp();
                }
                if (btnTrigger != null)
                {
                    btnTrigger.triggerCount = 1;
                }
            }
        }
    }


    void DoActivateTrigger()
    {
        _ismoving = true;
        Debug.Log("11");
        if (haveSound && !_audio.isPlaying)
        {
            Debug.Log("22");
            _audio.Play();
        }
    }
}
