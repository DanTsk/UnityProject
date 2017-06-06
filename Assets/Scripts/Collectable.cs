using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    bool hideAnimation = false;

    protected virtual void OnRabitHit(Rabbit rabit){}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!this.hideAnimation)
        {
            Rabbit rabit = collider.GetComponent<Rabbit>();
            if (rabit != null)
            {
                if(!rabit.isDead())
                    this.OnRabitHit(rabit);
            }
        }
    }

    public void CollectedHide()
    {
        Destroy(this.gameObject);
    }
   

}