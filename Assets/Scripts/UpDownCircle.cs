using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownCircle : MonoBehaviour
{

    public float bottomPosition = -16.25f;
    public float topPosition = -8.75f;
    private float cycleTime = 4;
    private bool _ismoving = true;
    private Transform _targetParent;
    private Vector3 _targetSourceScale;
    private bool _isPause = false;
    private float _swing;
    private float _omega;
    private float _phi;
    private float _originPosY;
    private float _zeroY;
    private float _t = 0;

    void Start()
    {
        if (bottomPosition > topPosition)
        {
            var temp = bottomPosition;
            bottomPosition = topPosition;
            topPosition = temp;
            Debug.Log("change");
        }

        _originPosY = transform.position.y;
        _zeroY = (topPosition + bottomPosition) / 2;
        _swing = (topPosition - bottomPosition) / 2;
        _omega = Mathf.PI * 2 / cycleTime;
        var x = _originPosY - _zeroY;
        _phi = Mathf.Asin(Mathf.Clamp(x / _swing,-1,1));
    }

    // Update is called once per frame
    void Update()
    {
        if(_isPause) return;
        var s = _swing * Mathf.Sin(_omega * _t + _phi);
        var pos = transform.position;
        pos.y = _zeroY + s;
        transform.position = pos;
        _t += Time.deltaTime;
    }

   

    void DoActivateTrigger()
    {
        _ismoving = true;
    }
}
