using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class TipsAction : MonoBehaviour {

    public float fillChangeTime = 2;
    public GameObject Ambra;
    public GameObject statusBoard;

    private UISprite _tipsIcon;
    private UISprite _tipsPic1;
    private UISprite _tipsPic2;
    private UILabel _tipsLabel1;
    private UILabel _tipsLabel2;
    private string[] _tipsStrings;
    private string[] _tipsPicturePath;
    private int _tipsIndex = 0;
    private int _sign = -1;
	void Start () {
        _tipsIcon = transform.Find("TipsIcon").GetComponent<UISprite>();
        _tipsPic1 = transform.Find("TipsPic1").GetComponent<UISprite>();
        _tipsPic2 = transform.Find("TipsPic2").GetComponent<UISprite>();
        _tipsLabel1 = transform.Find("TipsLabel1").GetComponent<UILabel>();
        _tipsLabel2 = transform.Find("TipsLabel2").GetComponent<UILabel>();
        _tipsStrings = File.ReadAllLines("Assets/Configs/tipsString.ini");
        _tipsPicturePath = File.ReadAllLines("Assets/Configs/tipsTexture.ini");
        nextTips();


        StartCoroutine(InvertFill());
    }
	
	void Update () {
        //Icon相关
        _tipsIcon.fillAmount += _sign * (1.0f / fillChangeTime) * Time.deltaTime;

        if(CrossPlatformInputManager.GetButtonUp("NextTips"))
        {
            nextTips();
        }
    }

    IEnumerator InvertFill()
    {
        yield return new WaitForSeconds(fillChangeTime);
        _tipsIcon.invert = !_tipsIcon.invert;
        _sign = -_sign;
        StartCoroutine(InvertFill());
    }

    void nextTips()
    {
        if (_tipsIndex >= _tipsPicturePath.Length)
        {
            Ambra.SetActive(true);
            statusBoard.SetActive(true);
            Destroy(gameObject);

        }
        _tipsPic1.spriteName = getStringByIndex(_tipsPicturePath, _tipsIndex);
        _tipsPic2.spriteName = getStringByIndex(_tipsPicturePath, _tipsIndex + 1);
        _tipsLabel1.text = getStringByIndex(_tipsStrings, _tipsIndex);
        _tipsLabel2.text = getStringByIndex(_tipsStrings, _tipsIndex + 1);
        _tipsIndex += 2;
        
    }
    string getStringByIndex(string[]strs,int index)
    {
        if(index >= strs.Length)
        {
            return "";
        }
        return strs[index];
    }
}
