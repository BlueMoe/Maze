using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureManager : MonoBehaviour {

    private int _totalTreasureCount;
    private int _collectTreasureCount;
	// Use this for initialization
	void Start () {
        _totalTreasureCount = transform.childCount;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void collectTreasure()
    {
        _collectTreasureCount++;
        Debug.Log(_collectTreasureCount.ToString() + "/" + _totalTreasureCount.ToString());
    }
    public int getTotalTreasureCount()
    {
        return _totalTreasureCount;
    }
    public int getCollectTreasureCount()
    {
        return _collectTreasureCount;
    }
}
