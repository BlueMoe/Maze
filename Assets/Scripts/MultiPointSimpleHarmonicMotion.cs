using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPointSimpleHarmonicMotion : MonoBehaviour
{

    public List<Vector3> positions = new List<Vector3>();
    public List<float> cycleTimes = new List<float>();

    private List<Vector3> _directions = new List<Vector3>();
    private List<Vector3> _zeros = new List<Vector3>();
    private List<float> _swings = new List<float>();
    private List<float> _omegas = new List<float>();
    private float _t;
    private int _index = 0;
    // Use this for initialization
    void Start()
    {
        initVector3List(ref _directions, (pos1, pos2) => { return (pos1 - pos2).normalized; });
        initVector3List(ref _zeros,  (pos1, pos2) => { return (pos1 + pos2) / 2; });
        initFloatList(ref _swings,  (pos1, pos2) => { return (pos1 - pos2).magnitude / 2; });
        initOmegaList(ref _omegas, (time) => { return Mathf.PI * 2 / (time * 2); });
    }

    // Update is called once per frame
    void Update()
    {
        var s = _swings[_index] * Mathf.Sin(_omegas[_index] * _t + Mathf.PI * 1.5f);
        transform.position = _zeros[_index] + _directions[_index] * s;
        _t += Time.deltaTime;
        if(_t > cycleTimes[_index])
        {
            _index++;
            if(_index > _directions.Count - 1)
            {
                _index = 0;
            }
            _t = 0;
        }
    }

    void initFloatList(ref List<float> list, Func<Vector3, Vector3 , float> op)
    {
        float item;
        for (int i = 0; i < positions.Count - 1; i++)
        {
            item = op(positions[i + 1], positions[i]);
            list.Add(item);
        }
        item = op(positions[0], positions[positions.Count - 1]);
        list.Add(item);
    }

    void initVector3List(ref List<Vector3> list, Func<Vector3, Vector3, Vector3> op)
    {
        Vector3 item;
        for (int i = 0; i < positions.Count - 1; i++)
        {
            item = op(positions[i + 1], positions[i]);
            list.Add(item);
        }
        item = op(positions[0], positions[positions.Count - 1]);
        list.Add(item);
    }

    void initOmegaList(ref List<float> list ,Func<float,float> op)
    {
        float item;
        for (int i = 0; i < cycleTimes.Count; i++)
        {
            item = op(cycleTimes[i]);
            list.Add(item);
        }
    }
}
