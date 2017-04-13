using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChangeTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ActionModeController>() == null) return;

        Camera.main.GetComponent<CameraController>().set2DMode(true);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<ActionModeController>() == null) return;

        Camera.main.GetComponent<CameraController>().set2DMode(false);
    }
}
