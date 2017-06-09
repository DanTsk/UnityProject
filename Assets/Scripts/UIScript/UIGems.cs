using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGems : MonoBehaviour {

    public Sprite first;
    public Sprite second;
    public Sprite third;


    public static UIGems gems;

    UI2DSprite[] gemComponents;


    private void Awake()
    {
        gems = this;
        gemComponents = new UI2DSprite[3];
        loadComponents();
    }

    private void loadComponents()
    {
        for (int i = 0; i < transform.childCount; ++i)
            gemComponents[i] = transform.GetChild(i).GetComponent<UI2DSprite>();
    }

    public void collectGem(string gems) {
        if (gems == "gem-1")
        {
            gemComponents[0].sprite2D = first;
            LevelController.current.gems[0] = 1;
        }
        else if (gems == "gem-2")
        {
            gemComponents[1].sprite2D = second;
            LevelController.current.gems[1] = 1;
        } 
        else if (gems == "gem-3")
        {
            gemComponents[2].sprite2D = third;
            LevelController.current.gems[2] = 1;
        }
    }

}
