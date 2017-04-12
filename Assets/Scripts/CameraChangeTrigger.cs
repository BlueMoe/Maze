using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChangeTrigger : MonoBehaviour {

    public bool changeTo2DMode = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ActionModeController>() == null) return;

        Camera.main.GetComponent<CameraController>().set2DMode(changeTo2DMode);
    }
}
