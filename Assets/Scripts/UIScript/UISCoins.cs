using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISCoins : MonoBehaviour {

	
	void Start () {
        int coins = PlayerPrefs.GetInt("coins", 0);

        string placer = "";

        if (coins < 10) placer = "000" + coins;
        else if (coins < 100) placer = "00" + coins;
        else if (coins < 1000) placer = "0" + coins;
        else if (coins < 10000) placer = "" + coins;

        this.GetComponent<UILabel>().text = placer;
    }
	
}
