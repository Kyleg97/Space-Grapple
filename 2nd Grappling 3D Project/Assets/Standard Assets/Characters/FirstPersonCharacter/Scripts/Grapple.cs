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
    private GameObject joint1;
    private GameObject middleJoint;
    private GameObject joint2;
    public static bool attached;
    public bool attached2;
    Vector3 hookLandObjPos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //player = GameObject.Find("RigidBodyFPSController");
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
                    hookLandObjPos = hookLandObj.transform.position;
                    attached = true;
                }

                if (!hookLandObj.GetComponent<Rigidbody>())
                {
                    hookLandObj.AddComponent<Rigidbody>();
                    hookLandObj.GetComponent<Rigidbody>().isKinematic = true;
                }

                hookLandObj.AddComponent<HingeJoint>();

                joint1 = new GameObject("Joint1");
                middleJoint = new GameObject("MiddleJoint");
                joint2 = new GameObject("Joint2");
                joint1.transform.position = new Vector3(playerPos.x, playerPos.y, playerPos.z);
                joint2.transform.position = new Vector3(hookLandObjPos.x, hookLandObjPos.y, hookLandObjPos.z);
                middleJoint.transform.position = 
                    new Vector3((playerPos.x + hookLandObjPos.x) / 2, (playerPos.y + hookLandObjPos.y) / 2, (playerPos.z + hookLandObjPos.z) / 2);

                joint1.AddComponent<Rigidbody>();
                joint1.AddComponent<HingeJoint>();
                joint1.GetComponent<HingeJoint>().enablePreprocessing = false;

                joint2.AddComponent<Rigidbody>();
                joint2.AddComponent<HingeJoint>();
                joint2.GetComponent<HingeJoint>().enablePreprocessing = false;

                middleJoint.AddComponent<Rigidbody>();
                middleJoint.AddComponent<HingeJoint>();
                middleJoint.GetComponent<HingeJoint>().enablePreprocessing = false;

                hookLandObj.GetComponent<HingeJoint>().connectedBody = joint2.GetComponent<Rigidbody>();
                joint2.GetComponent<HingeJoint>().connectedBody = middleJoint.GetComponent<Rigidbody>();
                joint1.GetComponent<HingeJoint>().connectedBody = player.GetComponent<Rigidbody>();
                middleJoint.GetComponent<HingeJoint>().connectedBody = joint1   .GetComponent<Rigidbody>();
            }
        }

        if (Input.GetButtonUp("Fire2"))
        {
            Destroy(hookLandObj.GetComponent<HingeJoint>());
            Destroy(hookLandObj.GetComponent<Rigidbody>());
            Destroy(line);
            Destroy(joint1);
            Destroy(middleJoint);
            Destroy(joint2);
            attached = false;    
        }            
    }
}
