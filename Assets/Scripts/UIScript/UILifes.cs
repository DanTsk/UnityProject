using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILifes : MonoBehaviour {

    public Sprite used;
    public Sprite unused;

    public static UILifes lifes;

    UI2DSprite [] lifeComponents;

    private void Awake()
    {
        lifes = this;
        lifeComponents = new UI2DSprite[3];
        loadComponents();
    }

    public void renew() {
        foreach (UI2DSprite comp in lifeComponents)
            comp.sprite2D = unused;
    }

    public void die(int time) {
        int place = 3 - time;
        lifeComponents[place].sprite2D = used;
    }

    public void redraw(int lives) {

        for(int i =0; i < lives; i++)
           lifeComponents[i].sprite2D = unused;

        return;
    }
 

    private void loadComponents() {      
        for (int i = 0; i < transform.childCount; ++i)
            lifeComponents[i] =  transform.GetChild(i).GetComponent<UI2DSprite>();
    }


}
