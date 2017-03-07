using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateNails : MonoBehaviour
{

    public GameObject nail;
    public Shader transparentShader;
    public GameObject nailList;
    private float _rowWidth = 0.5f;
    private float _colWidth = 0.5f;
    // Use this for initialization
    void Start()
    {
        GetComponent<Renderer>().material.shader = transparentShader;
        addNails();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void addNails()
    {
        var x = transform.localScale.x;
        var y = transform.localScale.y;
        var z = transform.localScale.z;

        var row = x / _rowWidth;
        var col = z / _colWidth;

        for (int i = 0; i < row ; i++)
        {
            for (int j = 0; j < col; j++)
            {
                var pos = nailList.transform.TransformPoint(new Vector3(i * _rowWidth, 0, j * _colWidth));
                var nails = Instantiate(nail, pos, transform.rotation);
                nails.transform.parent = nailList.transform;

            }
        }

    }
}
