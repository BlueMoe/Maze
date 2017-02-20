using System.Collections.Generic;
using UnityEngine;

public class teleportation : MonoBehaviour
{
    public Material phantomMaterial;
    public GameObject Ambra;
    private float _teleportationSpeed = 10f;
    private List<GameObject> phantomList;
    private List<float> phantomTime;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
            Destroy(phantom, 3);
        }
    }

    void teleportAmbra()
    {
        var mode = Ambra.GetComponent<ActionModeController>().getActionMode();
        if(mode == ActionModeController.ActionMode.CHARACTERCONTROLLERMODE)
        {
            Ambra.GetComponent<CharacterController>().Move(Ambra.transform.TransformDirection(new Vector3(0, 0, _teleportationSpeed)));
        }
        else if(mode == ActionModeController.ActionMode.RIGIDBODYMODE)
        {
            Ambra.GetComponent<Rigidbody>().AddForce(Ambra.transform.TransformDirection(new Vector3(0, 0, _teleportationSpeed)), ForceMode.VelocityChange);
        }
        
    }
}
