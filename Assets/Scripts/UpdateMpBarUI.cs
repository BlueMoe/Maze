using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMpBarUI : MonoBehaviour {

    public GameObject Ambra;

    private Teleportation _teleportation;
    private Color _mpBarColor;
    private Color _mpBarColorInCD = new Color(1, 0, 0);
    private UISlider _mpBar;
    private UILabel _mpValue;
    // Use this for initialization
    void Start () {
        _teleportation = Ambra.GetComponent<Teleportation>();
        _mpBar = transform.Find("mpBarBoard/mpBar").GetComponent<UISlider>();
        _mpValue = transform.Find("mpBarBoard/mpValue").GetComponent<UILabel>();
        _mpBarColor = _mpBar.foregroundWidget.color;
	}
	
	// Update is called once per frame
	void Update () {
        if(_teleportation == null) return;
        _mpBar.Set(_teleportation.getMp() / _teleportation.getMaxMp());
        if (_teleportation.getCD())
        { 
            _mpBar.foregroundWidget.color = _mpBarColorInCD;
        }
        else
        {
            _mpBar.foregroundWidget.color = _mpBarColor;
        }
        _mpValue.text = Mathf.FloorToInt(_teleportation.getMp()).ToString();
    }
}
