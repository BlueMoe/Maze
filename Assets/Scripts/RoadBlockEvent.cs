using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBlockEvent : MonoBehaviour {

    public bool destroyButtonWithRoadBlock = false;

    public GameObject controllButton;
    public GameObject chip;

    private bool _actionStart = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void DoActivateTrigger()
    {
        roadBlockBreak();

        if (destroyButtonWithRoadBlock)
        {   
            Destroy(controllButton);
        }
    }

    void roadBlockBreak()
    {
        var audio = GetComponent<AudioSource>();
        if(audio)
        {
            audio.Play();
        }
        StartCoroutine(zoomOutAndBreak());
    }

    IEnumerator zoomOutAndBreak()
    {
        transform.localScale -= Vector3.one * Time.deltaTime * 10;
        yield return 0;
        if (transform.localScale.x > 1)
        {
            StartCoroutine(zoomOutAndBreak());
        }
        else
        {
            breakAsChip();
        }
    }
    void breakAsChip()
    {
        Destroy(gameObject);
        for (int i = 0; i < 100; i++)
        {
            var chips = Instantiate(chip, transform.position, new Quaternion(Random.Range(0,360), Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
        }
    }
}
