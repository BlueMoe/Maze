using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spotLightFollowCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var eulerAngels = Camera.main.transform.eulerAngles;
        eulerAngels.x = 20;
        transform.eulerAngles = eulerAngels;
	}
}
