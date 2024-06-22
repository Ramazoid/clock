using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComPosition : MonoBehaviour
{
    public Transform clock;
    public float distance;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = clock.position+Vector3.up * distance;
        transform.LookAt(clock);
    }
}
