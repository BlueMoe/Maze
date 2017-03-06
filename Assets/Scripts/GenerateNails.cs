using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateNails : MonoBehaviour
{

    public GameObject nail;

    private float _rowWidth = 0.5f;
    private float _colWidth = 0.5f;
    // Use this for initialization
    void Start()
    {
        addNails();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void addNails()
    {
        var box = GetComponent<BoxCollider>();
        var x = box.size.x;
        var y = box.size.y;
        var z = box.size.z;

        var row = x / _rowWidth;
        var col = z / _colWidth;

        for (int i = 0; i < row ; i++)
        {
            for (int j = 0; j < col; j++)
            {
                var pos = transform.TransformPoint(new Vector3(i * _rowWidth - x / 2, -y / 2, j * _colWidth - z / 2));
                var nails = Instantiate(nail, pos, transform.rotation);
                nails.transform.parent = transform;

            }
        }

    }
}
