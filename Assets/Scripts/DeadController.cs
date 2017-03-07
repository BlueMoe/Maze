using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadController : MonoBehaviour {

    public List<GameObject> checkPoint;

<<<<<<< HEAD
    private int _checkPointIndex = 5;
=======
    private int _checkPointIndex = 1;
>>>>>>> ba401d3991d92af7fc6d4e9b1ee56ffa8957af1f

    private void Start()
    {
    }

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
        var pos = checkPoint[index].transform.position;
        var rotate = checkPoint[index].transform.rotation;
        transform.position = pos;
        transform.rotation = rotate;
        transform.Rotate(Vector3.up, 90);
    }
}
