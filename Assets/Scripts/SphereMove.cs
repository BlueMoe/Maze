using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class SphereMove : MonoBehaviour {

    public List<GameObject> ControlButtons = new List<GameObject>();

    private int _DownCount;
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void DoActivateTrigger()
    {
        _DownCount++;
        if(_DownCount == ControlButtons.Count)
        {
            moveToTargetPosition();    
        }
    }

    void moveToTargetPosition()
    {
        resetButtons();
    }

    void moveBackToSourcePosition()
    {

    }
    void resetButtons()
    {
        for(int i = 0;i<ControlButtons.Count;i++)
        {
            var activateTrigger = ControlButtons[i].GetComponentInChildren<ActivateTrigger>();
            activateTrigger.triggerCount = 1;
            ControlButtons[i].GetComponentInChildren<ButtonEvents>().setButtonUp();            
        }
        _DownCount = 0;
    }
    void OnColliderEnter(Collision collision)
    {

    }
}
