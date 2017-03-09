using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownCircle : MonoBehaviour
{
    
    public float bottomPosition = -16.25f;
    public float topPosition = -8.75f;
    private float cycleTime = 2;
    private bool _ismoving = true;
    private Transform _targetParent;
    private Vector3 _targetSourceScale;
    private bool _isPause = false;
    private float swing;
    private float omega;
    private float phi;
    private float originPosY;
    private float t = 0;
    
    void Start()
    {
        if(bottomPosition > topPosition)
        {
            var temp = bottomPosition;
            bottomPosition = topPosition;
            topPosition = temp;
        }

        originPosY = transform.position.y;
        swing = topPosition - bottomPosition / 2;
        omega = Mathf.PI * 2 / cycleTime;
        var x = originPosY - (topPosition + bottomPosition) / 2;
        phi = Mathf.Asin(x / swing);


    }

    // Update is called once per frame
    void Update()
    {
        if(_isPause) return;

        var s = swing * Mathf.Sin(omega * t + phi);
        var pos = transform.position;
        pos.y = originPosY + s;
        transform.position = pos;
        t += Time.deltaTime;
    }

   

    void DoActivateTrigger()
    {
        _ismoving = true;
    }
}
