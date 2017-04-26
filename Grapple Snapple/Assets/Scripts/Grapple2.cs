using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple2 : MonoBehaviour
{

    public float maxVelocity = 10.0f;
    public float maxVelocity2 = 100.0f;
    public float swingForwardSpeed = 10.0f;
    public float swingStrafeSpeed = 10.0f;

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

    //public static GameObject hookAnchor;
    public GameObject prefab;

    public static GameObject hook;
    public GameObject prefab2;

    public AudioSource grappleShoot;
    //public AudioSource grappleHit;
    public AudioClip footStep;

    public Vector3 hookDir;

    public bool kinematicCheck;
    public static bool hookDestroyed;
    public bool mouseUp;
    public float hookDistance;
    public bool hitTarget;

    public Vector3 point1;
    public Vector3 point2;
    public Vector3 point3;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }
    void Start()
    {
        groundDist = col.bounds.extents.y;
        //prefab = Resources.Load("HookAnchor") as GameObject;
        prefab2 = Resources.Load("Grapplehook") as GameObject;
        grappleShoot = GetComponent<AudioSource>();
        footStep = Resources.Load("FootStepWalk") as AudioClip;
        kinematicCheck = false;
        hookDestroyed = true;
        mouseUp = false;
        hitTarget = false;

        if (GameObject.Find("point1") != null)
        {
            point1 = GameObject.Find("point1").transform.position;
            point2 = GameObject.Find("point2").transform.position;
            point3 = GameObject.Find("point3").transform.position;
        }
    }


    void Update()
    {

        if (Input.GetKey(KeyCode.I))
        {
            transform.position = point1;
        }
        if (Input.GetKey(KeyCode.O))
        {
            transform.position = point2;
        }
        if (Input.GetKey(KeyCode.P))
        {
            transform.position = point3;
        }


        if (hook != null)
        {
            hookDistance = Vector3.Distance(transform.position, hook.transform.position);
            hookDir = hook.transform.position - transform.position;
            hookDir = hookDir.normalized;
        }

        if (Input.GetButtonDown("Fire1") && hookDestroyed)
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 150) && hit.collider.name != "No")
            {
                //animate throw
                //PlayerAnimation.anim.Play("Throw", -1, 0f);
                hook = Instantiate(prefab2) as GameObject;
                hook.GetComponent<Rigidbody>().isKinematic = false;
                hook.GetComponent<Rigidbody>().useGravity = false;
                hookDestroyed = false;
                hook.transform.position = transform.position + Camera.main.transform.forward;
                hook.GetComponent<Rigidbody>().velocity = Camera.main.transform.forward * 150;
                grappleShoot.Play();
            }
        }

        if (hook != null)
        {
            if (hook.GetComponent<Rigidbody>().isKinematic && !kinematicCheck)
            {
                kinematicCheck = true;
                hitTarget = true;
                hitPoint = hook.transform.position;
                //hookAnchor = Instantiate(prefab) as GameObject;
                //hookAnchor.transform.position = hitPoint;
                //hookAnchor.transform.SetParent(hit.collider.transform);
                //grappleHit.Play();
                grapple = true;
                dist = Vector3.Distance(transform.position, hook.transform.position);
                ropeLength = dist;
            }
        }

        if (Input.GetButton("Fire1"))
        {
            if (grapple && hook != null)
            {
                Vector3 vel = transform.position - hook.transform.position;
                distance = vel.magnitude;
                newVel = vel;

                if (hook.GetComponent<Rigidbody>().isKinematic)
                {
                    if (distance > ropeLength)
                    {
                        newVel.Normalize();
                        vel = Vector3.ClampMagnitude(vel, ropeLength);
                        transform.position = hook.transform.position + vel;
                        float dot = Vector3.Dot(newVel, rb.velocity);
                        newVel *= dot;
                        rb.velocity -= newVel;
                    }

                    if (Input.GetKey(KeyCode.E))
                        ropeLength += .4f * (70 * Time.deltaTime);
                    
                    if (Input.GetKey(KeyCode.R))
                        ropeLength -= .4f * (70 * Time.deltaTime);
                    
                }
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            mouseUp = true;
            grapple = false;
            kinematicCheck = false;
            //Destroy(hookAnchor);

            if (rb.velocity.y > 0 && rb.velocity.x > 0)
            {
                rb.AddForce(transform.forward * 50);
                rb.AddForce(transform.right * 50);
                rb.AddForce(transform.up * 50);
            }
        }

        if (mouseUp && !hitTarget && hook != null)
        {
            //Debug.Log(hitTarget);
            hook.transform.position = Vector3.MoveTowards(hook.transform.position, transform.position, 300 * Time.deltaTime);
            hitTarget = false;
        }

        if (mouseUp && hook != null)
        {
            hook.transform.position = Vector3.MoveTowards(hook.transform.position, transform.position, 200 * Time.deltaTime);
            hook.GetComponent<SphereCollider>().enabled = false;

            if (hookDistance < 1)
            {
                //Destroy(hookAnchor);
                Destroy(hook);
                hookDestroyed = true;
                mouseUp = false;
                kinematicCheck = false;
                grapple = false;
                hitTarget = false;
            }
        }

        if (hookDestroyed)
            mouseUp = false;

        if (hook != null)
        {
            if (Vector3.Distance(transform.position, hook.transform.position) > 160)
            {
                mouseUp = true;

                if (mouseUp)
                {
                    hook.transform.position = Vector3.MoveTowards(hook.transform.position, transform.position, 1);

                    if (HookCollide.canDestroy)
                    {
                        //Destroy(hookAnchor);
                        Destroy(hook);
                        hookDestroyed = true;
                        mouseUp = false;
                        grapple = false;
                        hitTarget = false;
                    }
                }

            }
        }
    }

    void FixedUpdate()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        /*
        if (Input.GetKey(KeyCode.R) && isGrounded() && rb.velocity.magnitude > 3)
        {
            ropeLength -= .2f * (50 * Time.deltaTime);
            rb.AddForce(hookDir * 50);
            rb.AddForce(-transform.up * 50);
            rb.AddForce(transform.right * x * 10);
        }
        */

        if (Input.GetKey(KeyCode.T))
            rb.AddExplosionForce(50, rb.transform.position, 50);

        if (rb.velocity.magnitude < maxVelocity)
        {
            rb.AddForce(transform.forward * z * 10);
            rb.AddForce(transform.right * x * 10);
        }
        
        if (rb.velocity.magnitude > maxVelocity2)
        {
            rb.velocity -= (rb.velocity / 15);
            Debug.Log(rb.velocity.magnitude);
        }
        
        if (rb.velocity.y < -10)
            ropeLength -= .2f * (40 * Time.deltaTime);

        if (isGrounded() && grapple && rb.velocity.magnitude > 30)
        {
            //Debug.Log("Grounded");
            //Debug.Log(rb.velocity.magnitude); 
            ropeLength -= .3f * (40 * Time.deltaTime);
        }

        if (!isGrounded())
        {
            rb.AddForce(transform.forward * z * 5);
            rb.AddForce(transform.right * x * 5);
        }

    }

    public static bool isGrounded()
    {
        return Physics.Raycast(rb.transform.position, -rb.transform.up, groundDist + 1);
    }
}