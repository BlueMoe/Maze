using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeForce : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().AddForce(transform.forward * 10,ForceMode.VelocityChange);
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
