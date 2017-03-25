using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAmbraPointLight : MonoBehaviour {

    public GameObject target;
    public Vector3 relativePosition;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        var pos = target.transform.position;
        pos += relativePosition;
        transform.position = pos;
	}
}
