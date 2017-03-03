using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGrapple : MonoBehaviour {

    private Rigidbody rb;
    private bool attached = false;
    public GameObject player;
    public GameObject rope;
    public SpringJoint joint;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player" && !attached)
        {
            Debug.Log("collision");
            attached = true;
            player = GameObject.FindGameObjectWithTag("Player");
            player.AddComponent<SpringJoint>();
            rope = collider.gameObject;
            joint = player.GetComponent<SpringJoint>();
            rb = rope.GetComponent<Rigidbody>();
            joint.connectedBody = rb;
            joint.connectedAnchor = new Vector3(0, 0, 0);


        }
    }
}
