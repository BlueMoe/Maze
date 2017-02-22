using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BahamutTreasure : MonoBehaviour {

    public GameObject Ambra;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        if (Ambra == null) return;
        if (collision.gameObject != Ambra) return;
        
        Destroy(gameObject);
    }
}
