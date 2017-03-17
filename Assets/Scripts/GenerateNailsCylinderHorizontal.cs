using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateNailsCylinderHorizontal : MonoBehaviour
{

    public GameObject nail;
    public Shader transparentShader;
    public GameObject nailList;


    private float _radius;
    private float _height;
    private float _rowWidth = Mathf.PI / 10;
    private float _colWidth = 0.5f;
    private float _scale = 1;

    // Use this for initialization
    void Start()
    {
        //不是正圆筒属于参数错误
        if (transform.localScale.x != transform.localScale.z)
        {
            Destroy(gameObject);
        }
        _radius = transform.localScale.x;
        _height = transform.localScale.y;
        GetComponent<Renderer>().material.shader = transparentShader;
        addNails();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void addNails()
    {
        int row = (int)(_radius * 2 * Mathf.PI / _rowWidth);
        int col = (int)(_height / _colWidth);

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                var centralAngle = i * _rowWidth / _radius;
                var x = Mathf.Sin(centralAngle) * _radius;
                var z = Mathf.Cos(centralAngle) * _radius;
                var y = (j + 1) * _colWidth;
                var pos = nailList.transform.TransformPoint(new Vector3(x, y, z));
                var nails = Instantiate(nail, pos, transform.rotation);
                nails.transform.parent = nailList.transform;
                nails.transform.eulerAngles = new Vector3(0, 180-centralAngle * 180 / Mathf.PI, 0);
                nails.transform.localScale = new Vector3(_scale, _scale, _scale);
            }
        }

    }
}
