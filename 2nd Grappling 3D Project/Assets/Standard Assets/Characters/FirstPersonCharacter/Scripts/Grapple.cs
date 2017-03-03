using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{

    public Transform cam;
    public static RaycastHit hit;
    private Rigidbody rb;
    private GameObject hookLandObj;
    private GameObject player;
    private LineRenderer line;
    private Vector3 lineHit;
    private Vector3 playerPos;
    public GameObject[] joints;
    private Vector3 jointLocation;
    public static bool attached;
    public bool attached2;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("RigidBodyFPSController");
        rb.isKinematic = false;
        attached = false;
    }

    void Update()
    {
        attached2 = attached;

        if (player.GetComponent<LineRenderer>() != null)
        {
            player.GetComponent<LineRenderer>();
            playerPos = player.transform.position;
            line.SetPosition(0, playerPos);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(cam.position, cam.forward, out hit))
            {
                player.AddComponent<LineRenderer>();
                line = player.GetComponent<LineRenderer>();
                lineHit = hit.point;
                playerPos = player.transform.position;
                line.SetPosition(0, playerPos);
                line.SetPosition(1, lineHit);
                line.useWorldSpace = true;
                line.startWidth = 0.08f;
                line.endWidth = 0.06f;
                line.material = new Material(Shader.Find("Standard"));
                line.material.color = Color.black;
                //For line texture later
                //line.material.mainTexture = "texture thingo";

                if (hit.collider.gameObject)
                {
                    hookLandObj = hit.collider.gameObject;
                    attached = true;
                }

                if (!hookLandObj.GetComponent<Rigidbody>())
                {
                    hookLandObj.AddComponent<Rigidbody>();
                }

                if (!hookLandObj.GetComponent<HingeJoint>())
                {
                    hookLandObj.AddComponent<HingeJoint>();
                }

                for (int i = 0; i < joints.Length; i++)
                {
                    joints[i] = new GameObject("Joint" + i);
                    joints[0].transform.position = player.transform.position;
                    joints[i].AddComponent<Rigidbody>();
                    joints[i].AddComponent<HingeJoint>();
                    HingeJoint connectedJoint = joints[i].GetComponent<HingeJoint>();
                    connectedJoint.useSpring = true;
                    //connectedJoint.spring //left off here!
                    if (i > 0 && i < joints.Length - 1)
                    {
                        connectedJoint.connectedBody = joints[i - 1].GetComponent<Rigidbody>();
                    }

                    if (i == 0)
                    {
                        connectedJoint.connectedBody = player.GetComponent<Rigidbody>();
                    }

                    if (i == joints.Length - 1)
                    {
                        //connectedJoint.connectedBody = hookLandObj.GetComponent<Rigidbody>();
                        hookLandObj.GetComponent<HingeJoint>().connectedBody = joints[i].GetComponent<Rigidbody>();
                    }

                    if (i == 1)
                    {
                        joints[1].transform.position = lineHit;
                        connectedJoint.connectedBody = joints[i - 1].GetComponent<Rigidbody>();
                    }
                    
                }

                hookLandObj.GetComponent<Rigidbody>().isKinematic = true;
            }
        }

        if (Input.GetButtonUp("Fire2"))
        {
                Destroy(hookLandObj.GetComponent<HingeJoint>());
                Destroy(hookLandObj.GetComponent<Rigidbody>());
                Destroy(line);
                attached = false;

                for (int i = 0; i < joints.Length; i++)
                {
                    Destroy(joints[i]);
                }     
        }

        if (attached)
        {

        }
            
    }
}
