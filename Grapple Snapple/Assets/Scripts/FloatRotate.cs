using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatRotate : MonoBehaviour {

    float startY;
    public float floatStrength = 1;

    void Start()
    {
        startY = transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, startY + ((float)Math.Sin(Time.time) * floatStrength), transform.position.z);
        transform.Rotate(Vector3.up * (5 * Time.deltaTime));
    }
}
