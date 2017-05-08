using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInput : MonoBehaviour
{

    public float speed;
    public static float gravity = 11.0f;
    public float maxVelocity = 80.0f;
    public float jumpHeight = 3.5f;
    //public static bool grounded = false;
    Rigidbody rb;
    public static bool running;
    public static bool canDisable;
    public static bool jumpAni;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = true;
        canDisable = false;
        jumpAni = false;
    }

    void FixedUpdate()
    {
        jumpAni = false;

        if (Grapple2.isGrounded()) // || Grapple.isGrounded())
        {
            Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= speed;

            Vector3 velocity = GetComponent<Rigidbody>().velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocity, maxVelocity);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocity, maxVelocity);
            velocityChange.y = 0;
            if (velocityChange != new Vector3 (0,0,0))
            rb.AddForce(velocityChange, ForceMode.VelocityChange);

            if (Input.GetButton("Jump"))
            {
                jumpAni = true;
                rb.velocity = new Vector3(velocity.x, JumpVerticalSpeed() * 1.5f , velocity.z); //1.5---->1.8
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                running = true;
                speed = 20.0f;
                //Headbob.bobbingAmount = .1f;
                //Headbob.bobbingSpeed = .2f;
            }

            else
            {
                running = false;
                speed = 12.0f;
                //Headbob.bobbingAmount = .1f;
                //Headbob.bobbingSpeed = .15f;
            }
        }

        rb.AddForce(new Vector3(0, -gravity * GetComponent<Rigidbody>().mass, 0));

        //grounded = false;
    }

    float JumpVerticalSpeed()
    {
        return Mathf.Sqrt((2 * jumpHeight * gravity));
    }
    
    void OnCollisionEnter(Collision collision)
    {
        rb.AddForce(rb.velocity / 2, ForceMode.VelocityChange);

        if (collision.gameObject.name == "Ramp")
        {
            Debug.Log("Ramp");
            gravity = 80.0f;
            Debug.Log(gravity);
            StartCoroutine(gravityChange());
            Debug.Log(gravity);
        }
    }

    public IEnumerator gravityChange()
    {
        yield return new WaitForSeconds(0.5f);
        gravity = 10.0f;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Lava")
        {
            Debug.Log("Die");
            canDisable = true;
            Time.timeScale = 0;
        }
    }
}