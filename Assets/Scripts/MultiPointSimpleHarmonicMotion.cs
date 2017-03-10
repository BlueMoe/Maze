using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPointSimpleHarmonicMotion : MonoBehaviour
{

    public List<Vector3> positions = new List<Vector3>();
    public List<float> cycleTimes = new List<float>();

    private List<Vector3> _directions;
    private List<Vector3> _zeros;
    private List<float> _swings;
    private List<float> _omegas;
    private float _omega;
    private float _t;
    private int _index = 0;
    // Use this for initialization
    void Start()
    {
        _directions = new List<Vector3>();
        initVector3List(ref _directions, (Vector3 pos1, Vector3 pos2) => { return (pos1 - pos2).normalized; });
        initVector3List(ref _zeros, (Vector3 pos1, Vector3 pos2) => { return (pos1 + pos2) / 2; });
        initFloatList(ref _swings, (Vector3 pos1, Vector3 pos2) => { return (pos1 - pos2).magnitude / 2; });
        initOmegaList(ref _omegas, (time) => { return Mathf.PI * 2 / time; });

    }

    // Update is called once per frame
    void Update()
    {

    }

    void initFloatList(ref List<float> list, Func<Vector3, Vector3 , float> op)
    {
        float item;
        for (int i = 0; i < positions.Count - 2; i++)
        {
            item = op(positions[i + 1], positions[i]);
            list.Add(item);
        }
        item = op(positions[positions.Count - 1], positions[0]);
        list.Add(item);
    }

    void initVector3List(ref List<Vector3> list, Func<Vector3, Vector3, Vector3> op)
    {
        Vector3 item;
        for (int i = 0; i < positions.Count - 2; i++)
        {
            item = op(positions[i + 1], positions[i]);
            list.Add(item);
        }
        item = op(positions[positions.Count - 1], positions[0]);
        list.Add(item);
    }

    void initOmegaList(ref List<float> list ,Func<float,float> op)
    {
        float item;
        for (int i = 0; i < cycleTimes.Count - 1; i++)
        {
            item = op(cycleTimes[i]);
            list.Add(item);
        }
    }

    void nextMotion()
    {
        cycleTimes[_index];
        
    }

    IEnumerator increaseIndex(float second)
    {
        yield return new WaitForSeconds();
        _index++;
        if (_index > positions.Count - 1)
        {
            _index = 0;
        }
    }
}
//var s = _swing * Mathf.Sin(_omega * _t + _phi);
//transform.position = _zero + _Direction* s;
//_t += Time.deltaTime;