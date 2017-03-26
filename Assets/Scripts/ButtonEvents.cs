using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class ButtonEvents : MonoBehaviour {

    public GameObject Ambra;

    public Material bahamutNormalMaterial;
    public Material bahamutDownMaterial;
    public GameObject bahamut;

    private bool _isDown = false;
    private ActivateTrigger _activateTrigger;
    private AudioSource _audioSource;
    // Use this for initialization
    void Start () {
        _audioSource = GetComponent<AudioSource>();
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
            _audioSource.Play();
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
