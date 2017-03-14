using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BahamutTreasure : MonoBehaviour {

    public GameObject Ambra;
    public float _rotateSpeed = 180;

    private TreasureManager _treasureManager;
    // Use this for initialization
    void Start () {
        _treasureManager = transform.parent.GetComponent<TreasureManager>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up * Time.deltaTime * _rotateSpeed);
	}

    void OnTriggerEnter(Collider other)
    {
        
        if (Ambra == null) return;
        if (other.gameObject != Ambra) return;
        if (_treasureManager != null)
        {
            _treasureManager.collectTreasure();
            Destroy(gameObject);
        }
        
    }
}
