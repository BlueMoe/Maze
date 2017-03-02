using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAmbraPointLight : MonoBehaviour {

    public GameObject Ambra;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Ambra == null)
            return;
        var pos = Ambra.transform.position;
        pos.y += 5;
        transform.position = pos;
	}
}
