using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : Collectable
{
    protected override void OnRabitHit(Rabbit rabit)
    {
        LevelController.current.collectLife();
        this.CollectedHide();
    }
}