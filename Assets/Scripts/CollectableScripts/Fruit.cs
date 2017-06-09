using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : Collectable
{
    protected override void OnRabitHit(Rabbit rabit)
    {
        if(!LevelController.current.getFruitsCMPX().Contains(int.Parse(this.transform.name)))
            LevelController.current.getFruitsCMPX().Add(int.Parse(this.transform.name));

        LevelController.current.addFruit();
        this.CollectedHide();
    }
}