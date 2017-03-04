using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBlockEvent : MonoBehaviour {

    public bool destroyButtonWithRoadBlock = false;

    public GameObject controllButton;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void DoActivateTrigger()
    {
        Destroy(gameObject);
        if(destroyButtonWithRoadBlock)
        {
            Destroy(controllButton);
        }
    }
}
