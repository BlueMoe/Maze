﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateNailsCylinder : MonoBehaviour {

    public GameObject nail;
    public Shader transparentShader;
    public GameObject nailList;


    private float _radius;
    private float _height;
    private float _rowWidth = Mathf.PI/20;
    private float _colWidth = 0.2f;

    // Use this for initialization
    void Start () {
        //不是正圆筒属于参数错误
        if(transform.localScale.x != transform.localScale.z)
        {
            Destroy(gameObject);
        }
        _radius = transform.localScale.x;
        _height = transform.localScale.y;
        //transform.localScale = new Vector3(1, 1, 1);
        GetComponent<Renderer>().material.shader = transparentShader;
        addNails();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void addNails()
    {
        //for (int i = 0; i<row ; i++)
        //{
        //    for (int j = 0; j<col; j++)
        //    {
        //        var pos = transform.TransformPoint(new Vector3(i * _rowWidth - x / 2, -y / 2, j * _colWidth - z / 2));
        //        var nails = Instantiate(nail, pos, transform.rotation);
        //        nails.transform.parent = transform;
        //    }
        //}

        var row = _radius * 2 * Mathf.PI / _rowWidth;
        var col = _height / _colWidth;

        for(int i =0;i< row;i++)
        {
            for(int j = 0;j< col;j++)
            {
                var centralAngle = i * _rowWidth / _radius;
                var x = Mathf.Sin(centralAngle) * _radius;
                var z = Mathf.Cos(centralAngle) * _radius;
                var y = j * _colWidth;
                var pos = nailList.transform.TransformPoint(new Vector3(x,y,z));
                var nails = Instantiate(nail, pos, transform.rotation);
                nails.transform.parent = nailList.transform;
                nails.transform.eulerAngles = new Vector3(90, 0,  -centralAngle*180/Mathf.PI);
            }
        }

    }
}
