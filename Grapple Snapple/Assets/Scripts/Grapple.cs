using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{

    public float speed = 10.0f;
    public float maxVelocity = 10.0f;
    public float swingForwardSpeed = 10.0f;
    public float swingStrafeSpeed = 5.0f;
    public float initialForce = 15.0f;

    float distToGround;
    Collider col;

    public float ropeLength = 0f;

    private bool grapple = false;
    public static Rigidbody rb;

    private float dist;
    public static Vector3 hitPoint;

    private Vector3 newVel;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }
    void Start()
    {
        distToGround = col.bounds.extents.y;
    }


    void Update()
    {
        RaycastHit hit;

        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 150))
            {

                if (hit.collider) // && hit.collider.name != "InvisWall")
                {
                    hitPoint = hit.point;
                    grapple = true;
                    dist = Vector3.Distance(transform.position, hitPoint);
                    ropeLength = dist;
                }
            }
        }

        if (Input.GetButton("Fire1"))
        {
            if (grapple)
            {
                Vector3 vel = transform.position - hitPoint;
                //Debug.Log(transform.position);
                //Debug.Log(hitPoint);
                //Debug.Log(vel);
                float distance = vel.magnitude;
                newVel = vel;
                //Vector3 vectorUp = (transform.position - hookAnchor.position).normalized;
                if (distance > ropeLength)
                {
                    newVel.Normalize();
                    vel = Vector3.ClampMagnitude(vel, ropeLength);
                    transform.position = hitPoint + vel;
                    float x = Vector3.Dot(newVel, rb.velocity);
                    newVel *= x;
                    rb.velocity -= newVel;
                    ropeLength -= .1f;
                }

                if (Input.GetKey(KeyCode.E))
                {
                    ropeLength += .5f;
                }

                if (Input.GetKey(KeyCode.R))
                {
                    ropeLength -= .5f;
                }

                if (Physics.Raycast(transform.position, Vector3.down, distToGround + 2))
                {
                    //ropeLength =
                }
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            grapple = false;

            if (rb.velocity.y > 0 && rb.velocity.x > 0)
            {
                rb.AddForce(transform.forward * 50);
                rb.AddForce(transform.right * 50);
            }
        }
    }

    void FixedUpdate()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //lol
        if (Input.GetKey(KeyCode.T))
        {
            rb.AddExplosionForce(50, rb.transform.position, 50);
        }

        if (Input.GetKey(KeyCode.F))
        {
            rb.AddForce(transform.forward * 200);
            rb.AddForce(transform.up * 100);
        }

        if (rb.velocity.magnitude < maxVelocity)
        {
            rb.AddForce(transform.forward * z * speed);
            rb.AddForce(transform.right * x * speed);
        }

        if (x < 5)
        {
            rb.AddForce(transform.right * x * speed);
        }

        if (x > 5)
        {
            rb.AddForce(-transform.right * x * speed);
        }


        if (z > 0 && rb.velocity.y < 0f && grapple)
        {
            //Debug.Log("z > 0");
            rb.AddForce(-transform.up * z * swingForwardSpeed);
            rb.AddForce(transform.right * x * swingStrafeSpeed / 2);
        }

        else if (!IsGrounded())
        {
            rb.AddForce(transform.forward * z * speed / 2);
            rb.AddForce(transform.right * x * speed / 2);
        }

        if (rb.velocity.y < -10)
        {
            ropeLength -= .4f;
        }

    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -transform.up, distToGround + 1);
    }
}