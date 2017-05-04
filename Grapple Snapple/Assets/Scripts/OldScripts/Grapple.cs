using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{

    public float maxVelocity = 10.0f;
    public float swingForwardSpeed = 10.0f;
    public float swingStrafeSpeed = 7.0f;

    //public float momentum;

    public static float groundDist;
    Collider col;

    public float ropeLength = 0f;

    public static bool grapple = false;
    public static Rigidbody rb;

    private float dist;
    public static Vector3 hitPoint;
    public static RaycastHit hit;
    private Vector3 newVel;
    public float distance;

    public static GameObject hookAnchor;
    public GameObject prefab;

    public AudioSource grappleShoot;
    public AudioSource grappleHit;
    public AudioClip footStep;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>(); 
    }
    void Start()
    {
        groundDist = col.bounds.extents.y;
        prefab = Resources.Load("HookAnchor") as GameObject;
        grappleShoot = GetComponent<AudioSource>();
        grappleHit = prefab.GetComponent<AudioSource>();
        footStep = Resources.Load("FootStepWalk") as AudioClip;
    }


    void Update()
    {
        if (hit.collider != null && hookAnchor != null)
        {
            hitPoint = hookAnchor.transform.position;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 150) && hit.collider.name != "No")
            {

                if (hit.collider && hit.collider.name != "No")
                {
                    grappleShoot.Play();
                    hitPoint = hit.point;
                    hookAnchor = Instantiate(prefab) as GameObject;
                    hookAnchor.transform.position = hitPoint;
                    hookAnchor.transform.SetParent(hit.collider.transform);
                    //grappleHit.Play();
                    //hitPoint = hookAnchor.transform.position;
                    //Debug.Log("GameObject: " + hookAnchor.transform.position);
                    //Debug.Log("Hit Collider: " + hit.collider.transform.position);
                    grapple = true;
                    dist = Vector3.Distance(transform.position, hookAnchor.transform.position);
                    ropeLength = dist;
                }
            }
        }

        if (Input.GetButton("Fire1"))
        {
            if (grapple)
            {
                Vector3 vel = transform.position - hitPoint;
                distance = vel.magnitude;
                newVel = vel;

                if (distance > ropeLength)
                {
                    newVel.Normalize();
                    vel = Vector3.ClampMagnitude(vel, ropeLength);
                    transform.position = hitPoint + vel;
                    float dot = Vector3.Dot(newVel, rb.velocity);
                    newVel *= dot;
                    rb.velocity -= newVel;
                }

                if (Input.GetKey(KeyCode.E))
                    ropeLength += .5f * (70 * Time.deltaTime);

                if (Input.GetKey(KeyCode.R))
                    ropeLength -= .5f * (100 * Time.deltaTime);
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            grapple = false;
            Destroy(hookAnchor);

            if (rb.velocity.y > 0 && rb.velocity.x > 0)
            {
                rb.AddForce(transform.forward * 50);
                rb.AddForce(transform.right * 50);
                rb.AddForce(transform.up * 50);
            }
        }
    }

    void FixedUpdate()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.T))
            rb.AddExplosionForce(50, rb.transform.position, 50);

        if (rb.velocity.magnitude < maxVelocity)
        {
            rb.AddForce(transform.forward * z * 10);
            rb.AddForce(transform.right * x * 10);
        }

        if (Mathf.Abs(z) > 0 && Mathf.Abs(x) > 0 && rb.velocity.y < 0 && grapple)
        {
            rb.AddForce(-transform.up * z * swingForwardSpeed * 2);
            rb.AddForce(transform.right * x * swingStrafeSpeed * 2);
        }

        if (rb.velocity.y < 0 && grapple)
        {
            ropeLength -= .2f * (5 * Time.deltaTime);
        }

        if (!isGrounded())
        {
            rb.AddForce(transform.forward * z * 5);
            rb.AddForce(transform.right * x * 5);
        }
        
        if (rb.velocity.y < -10)
            ropeLength -= .2f * (40 * Time.deltaTime);

        if (isGrounded() && grapple && rb.velocity.magnitude > 30)
        {
            Debug.Log("Grounded");
            Debug.Log(rb.velocity.magnitude);
            ropeLength -= .3f * (40 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.R) && grapple && Physics.Raycast(rb.transform.position, -rb.transform.up, groundDist + 5))
        {
            Debug.Log("Reeling and Grounded");
            rb.AddForce(transform.up * 5 * Time.deltaTime);
        }
    }

    public static bool isGrounded()
    {
        return Physics.Raycast(rb.transform.position, -rb.transform.up, groundDist + 3);
    }

    public static bool isGrounded2()
    {
        return Physics.Raycast(rb.transform.position, -rb.transform.up, groundDist + 0.1f);
    }
}