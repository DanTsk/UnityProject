using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collider)
    {
     
        Rabbit rabit = collider.GetComponent<Rabbit>();
  
        if (rabit != null)
        {
            rabit.dieSoundPLay();
            LevelController.current.onRabitDeath(rabit);
        }

  
    }
}
