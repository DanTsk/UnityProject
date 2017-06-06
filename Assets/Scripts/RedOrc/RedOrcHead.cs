using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedOrcHead : MonoBehaviour
{

    RedOrc orc;

    void Start()
    {
        orc = transform.parent.GetComponent<RedOrc>();

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Rabbit rabit = collider.GetComponent<Rabbit>();

        if (rabit != null)
        {
            Vector3 r_pos = rabit.transform.localPosition;
            Vector3 this_pos = transform.parent.localPosition;

            if (this_pos.y < r_pos.y && !rabit.isDead())
            {
                orc.die();
            }
        }
    }


}
