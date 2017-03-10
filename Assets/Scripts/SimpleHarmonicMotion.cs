using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleHarmonicMotion : MonoBehaviour
{
    public float cycleTime = 0;
    public Vector3 Position1 = new Vector3(0, 0, 0);
    public Vector3 Position2 = new Vector3(0, 0, 0);

    private bool _ismoving = true;
    private float _swing;
    private float _omega;
    private float _phi;
    private float _t = 0;
    private Vector3 _originPos;
    private Vector3 _zero;
    private Vector3 _Direction;
    void Start()
    {
        _originPos = transform.position;
        _zero = (Position1 + Position2) / 2;
        _swing = (Position1 - Position2).magnitude / 2;
        _omega = Mathf.PI * 2 / cycleTime;
        var phase = (_originPos - _zero).magnitude;
        _phi = Mathf.Asin(Mathf.Clamp(phase / _swing, -1, 1));
        _Direction = _zero - Position1;
        _Direction.Normalize();

    }

    // Update is called once per frame
    void Update()
    {
        var s = _swing * Mathf.Sin(_omega * _t + _phi);
        transform.position = _zero + _Direction * s;
        _t += Time.deltaTime;
    }

    void DoActivateTrigger()
    {
        _ismoving = true;
    }
}
