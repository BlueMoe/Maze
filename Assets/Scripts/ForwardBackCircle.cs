using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardBackCircle : MonoBehaviour
{
    public float forwardPosition = -16.25f;
    public float backPosition = -8.75f;
    public float cycleTime = 4;
    private bool _ismoving = true;
    private Transform _targetParent;
    private Vector3 _targetSourceScale;
    private bool _isPause = false;
    private float _swing;
    private float _omega;
    private float _phi;
    private float _originPosX;
    private float _zeroX;
    private float _t = 0;

    void Start()
    {
        if (forwardPosition > backPosition)
        {
            var temp = forwardPosition;
            forwardPosition = backPosition;
            backPosition = temp;
        }

        _originPosX = transform.position.x;
        _zeroX = (backPosition + forwardPosition) / 2;
        _swing = (backPosition - forwardPosition) / 2;
        _omega = Mathf.PI * 2 / cycleTime;
        var x = _originPosX - _zeroX;
        _phi = Mathf.Asin(Mathf.Clamp(x / _swing, -1, 1));
    }

    // Update is called once per frame
    void Update()
    {
        var s = _swing * Mathf.Sin(_omega * _t + _phi);
        var pos = transform.position;
        pos.x = _zeroX + s;
        transform.position = pos;
        _t += Time.deltaTime;
    }

    void DoActivateTrigger()
    {
        _ismoving = true;
    }
}
