using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInput : MonoBehaviour
{

    public float speed = 12.0f;
    public float gravity = 10.0f;
    public float maxVelocity = 80.0f;
    public bool canJump = true;
    public float jumpHeight = 2.0f;
    public bool grounded = false;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = true;
    }

    void FixedUpdate()
    {
        
        if (grounded)
        {
            Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= speed;

            Vector3 velocity = GetComponent<Rigidbody>().velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocity, maxVelocity);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocity, maxVelocity);
            velocityChange.y = 0;
            rb.AddForce(velocityChange, ForceMode.VelocityChange);

            if (canJump && Input.GetButton("Jump"))
            {
                rb.velocity = new Vector3(velocity.x, JumpVerticalSpeed() * 1.5f , velocity.z); //1.5---->1.8
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 20.0f;
            }

            else
                speed = 12.0f;
        }

        rb.AddForce(new Vector3(0, -gravity * GetComponent<Rigidbody>().mass, 0));

        grounded = false;
    }

    float JumpVerticalSpeed()
    {
        return Mathf.Sqrt((2 * jumpHeight * gravity));
    }
    
    void OnCollisionEnter(Collision collision)
    {
        rb.AddForce(rb.velocity / 2, ForceMode.VelocityChange);
    }
    
    void OnCollisionStay()
    {
        grounded = true;
    }
}