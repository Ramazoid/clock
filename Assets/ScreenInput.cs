using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScreenInput : MonoBehaviour
{
    public string value;
    private Text text;
    public ScreenInput nextSI;
    public Action<string> done;
    public bool ready;
    private bool blink;

    internal void Activate()
    {
        ready = true;
        gameObject.SetActive(true);
        value = "";
        text = GetComponentInChildren<Text>();
        
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (ready)
        {
            if (blink)
                text.text = value + "|";
            else text.text = value;
            blink = !blink;

            for (char c = '0'; c <= '9'; c++)
                if (Input.GetKeyDown((KeyCode)(int)c))
                {
                    print($"[{name}] pressed[{c}]");


                    if (value.Length == 0)
                    {
                        value += c;
                    }
                    else if (value.Length == 1)
                        value += c.ToString();

                    if (value.Length == 2)
                    {
                        if (nextSI != null)
                        {
                            nextSI.Activate();
                            nextSI.value = "";
                        }
                        ready = blink = false;
                        Clock.SetFromInputValue(name.Sub(0, 3));
                    }
                }
        }
    }
}