using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFruit : MonoBehaviour {

    public static UIFruit fruitCounter;

    UILabel label;


    private void Awake()
    {
        fruitCounter = this;
        this.label = this.transform.GetComponent<UILabel>();       
    }


    public void setFruits(int fruits,int max)
    {
        this.label.text = fruits + "/" + max;
    }

}
