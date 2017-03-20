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

    //public static bool tooFast = false;
    //public static bool canGo = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }
    void Start()
    {
        groundDist = col.bounds.extents.y;
        prefab = Resources.Load("HookAnchor") as GameObject;
    }


    void Update()
    {
        
        /*
        if (hit.collider != null)
        {
            hitPointDif = hitColPoint1 - hitColPoint;
            hitPoint += hitPointDif;
        }
        */

        if (hit.collider != null && hookAnchor != null)
        {
            //Debug.Log("GameObject: " + hookAnchor.transform.position);
            //Debug.Log("Hit Collider: " + hit.collider.transform.position);
            hitPoint = hookAnchor.transform.position;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 150) && hit.collider.name != "No")
            {

                if (hit.collider && hit.collider.name != "No")
                {
                    hitPoint = hit.point;
                    hookAnchor = Instantiate(prefab) as GameObject;
                    hookAnchor.transform.position = hitPoint;
                    hookAnchor.transform.SetParent(hit.collider.transform);
                    hitPoint = hookAnchor.transform.position;
                    //Debug.Log("GameObject: " + hookAnchor.transform.position);
                    //Debug.Log("Hit Collider: " + hit.collider.transform.position);
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
                distance = vel.magnitude;
                newVel = vel;

                if (distance > ropeLength)
                {
                    newVel.Normalize();
                    vel = Vector3.ClampMagnitude(vel, ropeLength);
                    transform.position = hitPoint + vel;
                    float x = Vector3.Dot(newVel, rb.velocity);
                    newVel *= x;
                    rb.velocity -= newVel;
                }

                if (Input.GetKey(KeyCode.E))
                    ropeLength += .3f;

                if (Input.GetKey(KeyCode.R))
                    ropeLength -= .3f;
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
            }
        }
    }

    void FixedUpdate()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //lol
        if (Input.GetKey(KeyCode.T))
            rb.AddExplosionForce(50, rb.transform.position, 50);

        if (rb.velocity.magnitude < maxVelocity)
        {
            rb.AddForce(transform.forward * z * 10);
            rb.AddForce(transform.right * x * 10);
        }

        if (z > 0 && rb.velocity.y < 0f && grapple)
        {
            rb.AddForce(-transform.up * z * swingForwardSpeed);
            rb.AddForce(transform.right * x * swingStrafeSpeed);
        }

        if (!isGrounded())
        {
            rb.AddForce(transform.forward * z * 5);
            rb.AddForce(transform.right * x * 5);
        }

        if (rb.velocity.y < -10)
            ropeLength -= .2f;

        if (isGrounded() && grapple && rb.velocity.magnitude > 2)
        {
            Debug.Log("Grounded");
            rb.AddForce(transform.forward * 15);
            rb.AddForce(transform.up * 7.5f);
        }
        
    }

    public static bool isGrounded()
    {
        return Physics.Raycast(rb.transform.position, -rb.transform.up, groundDist + 10);

    }
}