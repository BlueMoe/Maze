using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowArea : MonoBehaviour {

    private Transform _targetParent;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ActionModeController>() == null) return;
        _targetParent = other.transform.parent;
        other.transform.parent = transform.parent;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<ActionModeController>() == null) return;
        other.transform.parent = transform.parent;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<ActionModeController>() == null) return;
        other.transform.parent = _targetParent;
    }
}
