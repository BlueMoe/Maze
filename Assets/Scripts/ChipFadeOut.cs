using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipFadeOut : MonoBehaviour {

    private Renderer _renderer;
    private Color _color;
    private float _stopTime;
    private Vector3 _pos;
	// Use this for initialization
	void Start () {
        _renderer = GetComponent<Renderer>();
        _color = _renderer.material.color;

        Destroy(gameObject, 120);
	}

    void Update()
    {
        
        if(_pos == transform.position)
        {
            _stopTime += Time.deltaTime;
        }
        _pos = transform.position;
        if(_stopTime > 0.5f)
        {
            Destroy(GetComponent<BoxCollider>());
            Destroy(GetComponent<Rigidbody>());
        }
    }
}
