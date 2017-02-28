using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvents : MonoBehaviour {

    public GameObject Ambra;

    public Material bahamutNormalMaterial;
    public Material bahamutDownMaterial;
    public GameObject bahamut;

    private bool _isDown = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (Ambra == null) return;
        if (_isDown == true) return;
        if(other.gameObject == Ambra)
        {
            setButtonDown();
        }
    }

    public void setButtonDown()
    {
        bahamut.GetComponent<Renderer>().material = bahamutDownMaterial;
        _isDown = true;
    }

    public void setButtonUp()
    {
        bahamut.GetComponent<Renderer>().material = bahamutNormalMaterial;
        _isDown = false;
    }
    public bool isDown()
    {
        return _isDown;
    }
}
