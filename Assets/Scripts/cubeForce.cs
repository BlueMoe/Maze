using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeForce : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if(GetComponent<Rigidbody>().velocity.z < transform.forward.z * 5)
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * 5, ForceMode.VelocityChange);
        }  
    }
}
