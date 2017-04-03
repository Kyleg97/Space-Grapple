using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headbob : MonoBehaviour
{

    private float timer = 0.0f;
    public static float bobbingAmount = 0.1f;
    public static float bobbingSpeed = 0.15f;
    public float midpoint = 1.2f;

    float waveslice;
    float horizontal;
    float vertical;
    Vector3 cSharpConversion;

    float distToGround;
    Collider col;

    void Start()
    {
        distToGround = Grapple.groundDist;
    }

    void Update()
    {
        if (Grapple2.isGrounded())
        {
            waveslice = 0.0f;
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            cSharpConversion = transform.localPosition;

            if (waveslice <= -.95 && !transform.parent.GetComponent<AudioClip>())
            {
                //play audio
            }
            
            if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
            {
                timer = 0.0f;
            }
            else
            {
                waveslice = Mathf.Sin(timer);
                timer = timer + bobbingSpeed;
                if (timer > Mathf.PI * 2)
                {
                    timer = timer - (Mathf.PI * 2);
                }
            }
            if (waveslice != 0)
            {
                float translateChange = waveslice * bobbingAmount;
                float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
                totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
                translateChange = totalAxes * translateChange;
                cSharpConversion.y = midpoint + translateChange;
            }
            else
            {
                cSharpConversion.y = midpoint;
            }
            transform.localPosition = cSharpConversion;
        }
    }

    private bool GetKeyUp(KeyCode leftShift)
    {
        throw new NotImplementedException();
    }

    private bool GetKeyDown(KeyCode leftShift)
    {
        throw new NotImplementedException();
    }

    private bool GetKey(KeyCode leftShift)
    {
        throw new NotImplementedException();
    }
}