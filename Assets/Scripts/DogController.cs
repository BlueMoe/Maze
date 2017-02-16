using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour {

    public GameObject dog;
    public float runSpeed = 1.0f;
    private Animator _animator;
    private float _rotateSpeed = 90f;
    // Use this for initialization
    void Start () {
        _animator = dog.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
        }
        DogMove();
       
    }

    void DogMove()
    {
        bool _isRun = false;
        Vector3 _moveDirection = new Vector3(0, 0, 0);
        float _rotate = 0;
        if (Input.GetKey(KeyCode.W))
        {
            _moveDirection.z += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _moveDirection.z -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _rotate -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _rotate += 1;
        }
        if(_moveDirection != Vector3.zero)
        {
            _isRun = true;
        }
        _animator.SetBool("isRun", _isRun);
        dog.transform.Translate(_moveDirection * runSpeed * Time.deltaTime);
        dog.transform.Rotate(Vector3.up, _rotate * _rotateSpeed * Time.deltaTime);
    }
}
