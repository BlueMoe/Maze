using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLightController : MonoBehaviour {

    public bool isDestroyed = false;

	// Use this for initialization
	void Start () {
		if(isDestroyed)
        {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
