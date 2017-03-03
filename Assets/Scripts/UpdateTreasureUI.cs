using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTreasureUI : MonoBehaviour {

    public GameObject Treasures;

    private TreasureManager _treasureManager;
    private UILabel _treasureProgress;
	// Use this for initialization
	void Start () {
        _treasureManager = Treasures.GetComponent<TreasureManager>();
        _treasureProgress = GetComponent<UILabel>();
    }
	
	// Update is called once per frame
	void Update () {
        if (_treasureManager == null) return;

        int cur = _treasureManager.getCollectTreasureCount();
        int total = _treasureManager.getTotalTreasureCount();
        _treasureProgress.text = cur.ToString() + "/" + total.ToString();
    }
}
