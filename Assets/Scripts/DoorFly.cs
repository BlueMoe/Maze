using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFly : MonoBehaviour {

    private float _force = 80;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void DoActivateTrigger()
    {
        fly();
    }

    void fly()
    {
        transform.Translate(new Vector3(-2, 0, 0));
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        GetComponent<Rigidbody>().AddForce(new Vector3(1, 1, 1) * _force,ForceMode.VelocityChange);
    }
}
