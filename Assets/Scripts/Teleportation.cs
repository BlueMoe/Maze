using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Teleportation : MonoBehaviour
{
    public Material phantomMaterial;
    public GameObject Ambra;
    public UIRoot mpBarRoot;
    public float _mpMax = 100;

    private bool _isTeleportCountDown = false;
    private float _teleportCDTime = 8;
    private float _mp;
    private float _mpCastSpeed = 50;
    private float _mpRegenSpeed = 20;
    private float _normalSpeed;
    private float _fastRatio = 1.5f;
    private bool _isFastMode = false;

    public float getMp()
    {
        return _mp;
    }
    public float getMaxMp()
    {
        return _mpMax;
    }
    public bool getCD()
    {
        return _isTeleportCountDown;
    }

    // Use this for initialization
    void Start()
    {
        //正常状态下的移动速度
        _normalSpeed = Ambra.GetComponent<MoveController>().moveSpeed;
        _mp = _mpMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isTeleportCountDown)
        {
            mpReginInCDTime();
            return;
        }
        
        if (CrossPlatformInputManager.GetButtonUp("FastMode"))
        {
            _isFastMode = !_isFastMode;
        }
        if(_isFastMode)
        {
            mpCast();
            speedUp();
            createPhantom();
            if(_mp <= 0)
            {
                setTeleportCountDown(_teleportCDTime);
            }
        }
        else
        {
            mpRegenInNormalTime();
            speedDown();
        }

    }

    void speedUp()
    {
        Ambra.GetComponent<Animator>().speed = _fastRatio;
        Ambra.GetComponent<MoveController>().moveSpeed = _normalSpeed * _fastRatio;
    }

    void speedDown()
    {
        Ambra.GetComponent<Animator>().speed = 1;
        Ambra.GetComponent<MoveController>().moveSpeed = _normalSpeed * 1;
    }
    void createPhantom()
    {
        //实例化幻影
        GameObject phantom = Instantiate(Ambra, transform.position, transform.rotation) as GameObject;
        //设置幻影材质
        phantom.GetComponentInChildren<Renderer>().material = phantomMaterial;
        //停止幻影的动作
        phantom.GetComponent<Animator>().Stop();
        //设置幻影模式,移除不必要组件
        phantom.GetComponent<ActionModeController>().changeMode(ActionModeController.ActionMode.PHANTOMMODE);
        phantom.transform.parent = Ambra.transform.parent;
        phantom.transform.localScale = Ambra.transform.localScale;
        //0.2秒后移除幻影
        Destroy(phantom, 0.2f);
    }

    void mpRegenInNormalTime()
    {
        _mp += _mpRegenSpeed * Time.deltaTime;
        _mp = Mathf.Clamp(_mp, 0, _mpMax);
    }

    void mpReginInCDTime()
    {
        _mp += Time.deltaTime * _mpMax / _teleportCDTime;
        _mp = Mathf.Clamp(_mp, 0, _mpMax);
    }
    void mpCast()
    {
        _mp -= Time.deltaTime * _mpCastSpeed;
    }

    void setTeleportCountDown(float seconds)
    {
        _isFastMode = false;
        speedDown();
        StartCoroutine(TeleportCountDown(seconds));
    }

    IEnumerator TeleportCountDown(float seconds)
    {
        _isTeleportCountDown = true;
        yield return new WaitForSeconds(seconds);
        _isTeleportCountDown = false;
    }
   
}
