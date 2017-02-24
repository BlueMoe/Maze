using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NailEvent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        var Ambra = other.gameObject.GetComponent<DeadController>();
        if (Ambra)
        {
            Ambra.die();
        }
    }
}
