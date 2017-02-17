using UnityEngine;
using System.Collections;

public class cameracontroller : MonoBehaviour {

    const float MAX_DISTANCE = 10;
    const float MIN_DISTANCE = 1;

    public GameObject target;
    public float cameraRotateSpeed = 0.1f;
    public float cameraMoveSpeed = 3;
    private float _distance = 2;
    private float _theta = 180;
    private float _phi = -10;
	// Use this for initialization
	void Start () {
        
        transform.position = caclCameraPosition();
        transform.LookAt(target.transform);
    }
	
	// Update is called once per frame
	void Update () {

        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            _distance -= 0.1f * cameraMoveSpeed;
            if (_distance < MIN_DISTANCE) _distance = MIN_DISTANCE;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            _distance += 0.1f * cameraMoveSpeed;
            if (_distance < MIN_DISTANCE) _distance = MIN_DISTANCE;
        }

        if (Input.GetMouseButton(0))
        {
            var x = Input.GetAxis("Mouse X");
            var y = Input.GetAxis("Mouse Y");
            _theta += x * cameraRotateSpeed;
            _phi += y * cameraRotateSpeed;
        }
        if(Input.GetMouseButton(1))
        {
            _theta = 180;
            _phi = -10;
        }
        transform.position = caclCameraPosition();
        transform.LookAt(target.transform);
    }

    Vector3 caclCameraPosition()
    {
        //球坐标系转空间坐标系
        float x = _distance * Mathf.Sin(Mathf.PI / 180 * _theta) * Mathf.Cos(Mathf.PI / 180 * _phi);
        float y = _distance * Mathf.Cos(Mathf.PI / 180 * _theta) * Mathf.Sin(Mathf.PI / 180 * _phi);
        float z = _distance * Mathf.Cos(Mathf.PI / 180 * _theta);
        return target.transform.TransformPoint(new Vector3(x, y, z));
    }
}
