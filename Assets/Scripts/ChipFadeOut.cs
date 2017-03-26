using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipFadeOut : MonoBehaviour {

    private Renderer _renderer;
    private Color _color;
    private float _stopTime;
    private Vector3 _pos;
    private Rigidbody _rigidbody;
	// Use this for initialization
	void Start () {
        _renderer = GetComponent<Renderer>();
        _color = _renderer.material.color;
        _rigidbody = GetComponent<Rigidbody>();
        GetComponent<AudioSource>().Play();

        Destroy(gameObject, 10);
	}

    void Update()
    {
        if(_rigidbody.velocity.magnitude >1)
        { Destroy(GetComponent<BoxCollider>()); }
        
    }
}
