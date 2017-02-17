using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class teleportation : MonoBehaviour
{
    public Material phantomMaterial;
    public GameObject Ambra;
    private float _teleportationSpeed = 5f;
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
            GameObject phantom = Instantiate(Ambra, transform.position, transform.rotation) as GameObject;
            phantom.GetComponentInChildren<Renderer>().material = phantomMaterial;
            phantom.GetComponent<Animator>().Stop();
            phantom.GetComponent<ThirdPersonUserControl>().enabled = false;
            phantom.GetComponent<teleportation>().enabled = false;
            phantom.GetComponent<Rigidbody>().isKinematic = true;
            phantom.GetComponent<CapsuleCollider>().enabled = false;
            var velocity = Ambra.GetComponent<Rigidbody>().velocity;
            Ambra.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 20, ForceMode.VelocityChange);
            Ambra.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * 2, ForceMode.VelocityChange);
            Destroy(phantom, 3);
        }
    }
    void OnCollisionEnter()
    {
        //Ambra.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
