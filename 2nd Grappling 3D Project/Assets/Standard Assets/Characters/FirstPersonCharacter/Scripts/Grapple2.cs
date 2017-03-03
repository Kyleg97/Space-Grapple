using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
public class Grapple2 : MonoBehaviour
{

    public Transform cam;
    public static RaycastHit hit;
    private Rigidbody rb;
    private bool attached = false;
    public static float momentum;
    public float speed;
    private float step;
    private GameObject player;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(cam.position, cam.forward, out hit))
            {
               public GameObject hookLandObj = hit.collider.gameObject;
}
        }

        if (Input.GetButtonUp("Fire1"))
        {
            attached = false;
            rb.isKinematic = false;
            rb.velocity = cam.transform.forward * momentum;
        }

        if (attached)
        {
            momentum += Time.deltaTime * speed;
            step = momentum * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, hit.point, step);
        }

        if (!attached && momentum >= 0)
        {
            momentum -= Time.deltaTime * 10;
            step = 0;
        }

        if (attached && momentum > 30)
        {
            momentum += 0.2f;
        }

        if (!attached && momentum > 30)
        {
            momentum -= 2;
        }

        if (momentum < 0)
        {
            momentum = 0;
        }
    }
}
//public GameObject hookLandObj = hit.collider.gameObject;
    */

/**
for (int i = 0; i < jointsArray.Count; i++)
{
Debug.Log(jointsArray.Count);
Debug.Log(i);
jointsArray[i] = new GameObject("Joint" + i);
jointsArray[i].AddComponent<Rigidbody>();
jointsArray[i].AddComponent<HingeJoint>();
jointsArray[i + 1].GetComponent<HingeJoint>().connectedBody = jointsArray[i].GetComponent<Rigidbody>();
jointsArray[jointsArray.Count - 1].GetComponent<HingeJoint>().connectedBody = hookLandObj.GetComponent<Rigidbody>();
}
*/

//jointsArray[0].GetComponent<HingeJoint>().connectedBody = player.GetComponent<Rigidbody>();