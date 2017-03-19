using UnityEngine;
using System;
using System.Collections;

public class Float : MonoBehaviour
{
    float startY;
    public float floatStrength = 1;

    void Start()
    {
        startY = transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, startY + ((float)Math.Sin(Time.time) * floatStrength), transform.position.z);
    }
}