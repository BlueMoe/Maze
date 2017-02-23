using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    public int Index = 0;

	void OnTriggerEnter(Collider collider)
    {
        var deadController = collider.gameObject.GetComponent<DeadController>();
        if(deadController)
        {
            deadController.setCheckPointIndex(Index);
        }
    }
}
