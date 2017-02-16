using UnityEngine;
using System.Collections;

public class cameracontroller : MonoBehaviour {

    public GameObject target;
    public float cameraRotateSpeed = 5;
    private float _distance = 2;
    private float _height = 1;
    const float MAX_DISTANCE = 10;
    const float MIN_DISTANCE = 1;
    private Vector3 _relativePos;
	// Use this for initialization
	void Start () {
        Vector3 pos = target.transform.position;
        pos.y += _height;
        pos.z -= _distance;
        transform.position = pos;
        _relativePos = target.transform.position - transform.position;
        transform.LookAt(target.transform);
    }
	
	// Update is called once per frame
	void Update () {

        transform.position = target.transform.position - _relativePos;
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            var x = Input.GetAxis("Mouse X");
            var y = Input.GetAxis("Mouse Y");
            transform.RotateAround(target.transform.position,Vector3.up,   x * cameraRotateSpeed);
            transform.RotateAround(target.transform.position,Vector3.right,  - y * cameraRotateSpeed);
            _relativePos = target.transform.position - transform.position;
        }
        transform.LookAt(target.transform);
    }
}
