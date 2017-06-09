using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIEnd : MonoBehaviour
{

    public string sceneName;


    void OnTriggerEnter2D(Collider2D collider)
    {
        Rabbit rabit = collider.GetComponent<Rabbit>();
        if (rabit != null)
        {
            GameObject.Find("FinishPopup").GetComponent<UIGamePopup>().showSettings();     
        }
    }


}
