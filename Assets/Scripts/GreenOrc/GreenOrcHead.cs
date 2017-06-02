using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenOrcHead : MonoBehaviour {

    GreenOrc orc;

    void Start()
    {
        orc = transform.parent.GetComponent<GreenOrc>();

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
       Rabbit rabit = collider.GetComponent<Rabbit>();
       Vector3 r_pos = rabit.transform.localPosition;
       Vector3 this_pos = transform.parent.localPosition; 

       if (rabit != null && this_pos.y < r_pos.y && !rabit.isDead())
       {
           orc.die();
       }      
    }

	
}
