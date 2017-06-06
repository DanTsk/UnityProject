using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Collectable
{
    string spriteName;

    private void Awake()
    {
        spriteName = GetComponent<SpriteRenderer>().sprite.texture.name;
    }

    protected override void OnRabitHit(Rabbit rabit)
    {
        UIGems.gems.collectGem(spriteName);
        this.CollectedHide();
    }
}