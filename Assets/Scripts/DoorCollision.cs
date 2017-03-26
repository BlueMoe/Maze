using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollision : MonoBehaviour {

    public GameObject _target;

    private AudioSource _audioSource;
	// Use this for initialization
	void Start () {
        _audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject == _target)
        {
            _audioSource.Play();
            transform.parent.GetComponent<DoorRotate>().RotateStart();
        }
    }

}
