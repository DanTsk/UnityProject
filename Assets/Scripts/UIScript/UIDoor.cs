using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDoor : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collider)
    {
        Rabbit rabit = collider.GetComponent<Rabbit>();
        if (rabit != null)
        {
           
        }
    }

}
