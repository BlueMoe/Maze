using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    const float MAX_DISTANCE = 10;
    const float MIN_DISTANCE = 1;

    public GameObject target;
    public float cameraRotateSpeed = 0.1f;
    public float cameraMoveSpeed = 3;
    private float _distance = 4;
    private float _theta = -90;
    private float _phi = 10;
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
            _theta -= x * cameraRotateSpeed;
            _phi -= y * cameraRotateSpeed;
            _phi = Mathf.Clamp(_phi, -89, 89);
        }
        if(Input.GetMouseButton(1))
        {
            _theta = -90;
            _phi = 10;
        }
        transform.position = caclCameraPosition();
        transform.LookAt(target.transform);
    }

    Vector3 caclCameraPosition()
    {
        float x = _distance * Mathf.Cos(Mathf.PI / 180 * _phi) * Mathf.Cos(Mathf.PI / 180 * _theta);
        float y = _distance * Mathf.Sin(Mathf.PI / 180 * _phi);
        float z = _distance * Mathf.Cos(Mathf.PI / 180 * _phi) * Mathf.Sin(Mathf.PI / 180 * _theta);
        return target.transform.TransformPoint(new Vector3(x, y, z));
    }
}
