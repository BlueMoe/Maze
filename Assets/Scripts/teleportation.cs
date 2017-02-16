using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class teleportation : MonoBehaviour
{
    public Material phantomShader;
    public Material normalShader;
    public GameObject Ambra;
    private float _teleportationSpeed = 5f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            GameObject phantom = Instantiate(Ambra, transform.position, transform.rotation);
            phantom.GetComponentInChildren<Renderer>().material = phantomShader;
            phantom.GetComponent<Animator>().Stop();
            phantom.GetComponent<ThirdPersonUserControl>().enabled = false;
            phantom.GetComponent<teleportation>().enabled = false;
            phantom.GetComponent<Rigidbody>().isKinematic = true;
            phantom.GetComponent<CapsuleCollider>().enabled = false;
            Ambra.transform.Translate(Vector3.forward * _teleportationSpeed);
        }
    }
}
