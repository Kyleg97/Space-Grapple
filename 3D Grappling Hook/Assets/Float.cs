using UnityEngine;
using System;
using System.Collections;

public class Float : MonoBehaviour
{
    float y1;

    public float floatStrength = 1; // You can change this in the Unity Editor to 
                                    // change the range of y positions that are possible.

    void Start()
    {
        this.y1 = this.transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x,
            y1 + ((float)Math.Sin(Time.time) * floatStrength),
            transform.position.z);
    }
}