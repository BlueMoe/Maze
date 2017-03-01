using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public Material phantomMaterial;
    public GameObject Ambra;
    private float _teleportationSpeed = 50f;
    private List<GameObject> phantomList;
    private List<float> phantomTime;
    private float _teleportCD = 2;
    private float _teleportCDing;
    private float _teleportTime = 0.2f;
    private float _teleporting;
    private float _t;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(_teleporting > 0)
        {
            Ambra.GetComponent<CharacterController>().Move(Time.deltaTime * Ambra.transform.TransformDirection(new Vector3(0, 0, _teleportationSpeed)));
            _teleporting -= Time.deltaTime;
        }

        if(_teleportCDing > 0)
        {
            _teleportCDing -= Time.deltaTime;
            return;
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
           
            //实例化幻影
            GameObject phantom = Instantiate(Ambra, transform.position, transform.rotation) as GameObject;
            //设置幻影材质
            phantom.GetComponentInChildren<Renderer>().material = phantomMaterial;
            //停止动作
            phantom.GetComponent<Animator>().Stop();
            //设置幻影模式,移除不必要组件
            phantom.GetComponent<ActionModeController>().changeMode(ActionModeController.ActionMode.PHANTOMMODE);
            //瞬移安布拉
            teleportAmbra();
            //3秒后移除幻影
            Destroy(phantom, 0.1f);
        }
    }

    void teleportAmbra()
    {
        var mode = Ambra.GetComponent<ActionModeController>().getActionMode();
        if(mode == ActionModeController.ActionMode.CHARACTERCONTROLLERMODE)
        {
            _teleportCDing = _teleportCD;
            _teleporting = _teleportTime;
        }
        else if(mode == ActionModeController.ActionMode.RIGIDBODYMODE)
        {
            var teleportVec = Ambra.transform.TransformDirection(new Vector3(0, 0, _teleportationSpeed));
            Ambra.GetComponent<MoveController>().setExternalVelocity(teleportVec, 0.2f);
        }
        
    }
}
