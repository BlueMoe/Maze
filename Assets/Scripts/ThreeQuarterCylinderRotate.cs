using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeQuarterCylinderRotate : MonoBehaviour
{

    public float rotateSpeed = 45;
    public GameObject Ambra;

    private MoveController _moveController;
    private Vector3 _prevLinearVelocity = Vector3.zero;
    // Use this for initialization
    void Start()
    {
        _moveController = Ambra.GetComponent<MoveController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision col)
    {
        _prevLinearVelocity = Vector3.zero;
    }
    void OnCollisionStay(Collision col)
    {
        _moveController.removeExternalVelocity(_prevLinearVelocity);
        Vector3 coutact = Vector3.zero;
        int coutactCount = 0;
        foreach (ContactPoint cp in col.contacts)
        {
            coutact += cp.point;
            coutactCount++;
        }
        coutact /= coutactCount;
        var direction = coutact - transform.position;
        var distance = Mathf.Sqrt(direction.x * direction.x + direction.y * direction.y + direction.z * direction.z);
        var linearVelocity = Vector3.Cross(transform.up, direction).normalized * Mathf.Deg2Rad * rotateSpeed * distance;
        _moveController.addExternalVelocity(linearVelocity);
        _prevLinearVelocity = linearVelocity;
    }
    void OnCollisionExit(Collision col)
    {
        _prevLinearVelocity = Vector3.zero;
    }

}
