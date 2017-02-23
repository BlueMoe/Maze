using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadController : MonoBehaviour {

    public List<GameObject> checkPoint = new List<GameObject>();

    private int _checkPointIndex = 0;

    public void die()
    {
        moveToCheckPoint(_checkPointIndex);   
    }

    public void setCheckPointIndex(int index)
    {
        _checkPointIndex = index;
    }

    void moveToCheckPoint(int index)
    {
        Vector3 pos = checkPoint[index].transform.position;
        pos.y += 2;
        transform.position = pos;
    }

    void DoActivateTrigger()
    {
        die();
    }
}
