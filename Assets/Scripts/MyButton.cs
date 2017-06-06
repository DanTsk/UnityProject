using System.Collections;
using UnityEngine.Events;
using UnityEngine;
public class MyButton : MonoBehaviour
{
    private void Awake()
    {

    }

    public UnityEvent signalOnClick = new UnityEvent();
    public void _onClick()
    {
        this.signalOnClick.Invoke();
    }
}