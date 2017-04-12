using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spotLightFollow : MonoBehaviour {

    public GameObject Ambra;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var eulerAngels = Ambra.transform.eulerAngles;
        eulerAngels.x = 20;
        transform.eulerAngles = eulerAngels;
	}
}
