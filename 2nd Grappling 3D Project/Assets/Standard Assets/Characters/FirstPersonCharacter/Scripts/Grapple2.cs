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
/**
    for (int i = 0; i<joints.Length; i++)
                {
                    joints[i] = new GameObject("Joint" + i);
joints[i].AddComponent<Rigidbody>();
                    joints[i].AddComponent<HingeJoint>();
                    joints[i].GetComponent<HingeJoint>().enablePreprocessing = false;
                    joints[i].GetComponent<HingeJoint>().useSpring = true;

                    if (i == joints.Length - 1)
                    {
                        joints[i].GetComponent<HingeJoint>().connectedBody = hookLandObj.GetComponent<Rigidbody>();
                        joints[i].transform.position = lineHit;
                        //hookLandObj.GetComponent<HingeJoint>().connectedBody = joints[i].GetComponent<Rigidbody>();
                    }
                }
                player.AddComponent<HingeJoint>();
                player.GetComponent<HingeJoint>().connectedBody = joints[0].GetComponent<Rigidbody>();
                player.GetComponent<HingeJoint>().enablePreprocessing = false;
                joints[0].GetComponent<HingeJoint>().connectedBody = joints[1].GetComponent<Rigidbody>();
                joints[joints.Length - 1].GetComponent<HingeJoint>().connectedBody = hookLandObj.GetComponent<Rigidbody>();
                joints[0].transform.position = new Vector3(playerPos.x, playerPos.y, playerPos.z);
//Debug.Log(playerPos);
//Debug.Log(joints[0].transform.position);
joints[1].transform.position = new Vector3(lineHit.x, lineHit.y, lineHit.z);
//Debug.Log(lineHit);
//Debug.Log(joints[1].transform.position);
hookLandObj.GetComponent<Rigidbody>().isKinematic = true;
                //hookLandObj.GetComponent<HingeJoint>().connectedBody = joints[joints.Length - 1].GetComponent<Rigidbody>();
            }
    */