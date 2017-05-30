using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : Collectable
{
    protected override void OnRabitHit(Rabbit rabit)
    {        
        this.CollectedHide();
    }
}