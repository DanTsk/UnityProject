using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICoins : MonoBehaviour {


    public static UICoins coinsCounter;

    UILabel label;


    private void Awake()
    {
        this.label = this.transform.GetComponent<UILabel>();
        coinsCounter = this;
         
    }


    public void setCoins(int coins)
    {
        string placer = "";

        if (coins < 10) placer = "000" + coins;
        else if (coins < 100) placer = "00" + coins;
        else if (coins < 1000) placer = "0" + coins;
        else if (coins < 10000) placer = "" + coins;

        label.text = placer;
    }
}
