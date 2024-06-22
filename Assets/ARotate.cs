using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ARotate : MonoBehaviour
{
    public bool rot;
    public  bool isMinutes;
    public Action<int> UpdateValue;
    public GameObject marker;

    // Start is called before the first frame update
    void Start()
    {
        isMinutes = name.IndexOf("Minute") != -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (rot && Clock.editMode)
        {
            transform.parent.LookAt(marker.transform.position);

            if (isMinutes)
            {
                int minutes = (int)Math.Floor(transform.parent.eulerAngles.y) / 6;
                print("new minutes=" + minutes);
                UpdateValue(minutes);
            }
            else
            {
                print("hours angle=" + transform.parent.eulerAngles.y);
                
                double hours = Math.Round(transform.parent.eulerAngles.y/30, 0, MidpointRounding.ToEven);
                print("new hours=" + hours);
                if (Input.GetMouseButtonUp(0))
                {
                    UpdateValue((int)hours); rot = false;
                }
            }
          
        }

    }

}
