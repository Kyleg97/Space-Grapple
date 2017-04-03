using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour {

    private bool canClimb;
    private Rigidbody rb;
    private RaycastHit hit;
    private GameObject climbTarget;


	void Start () {
        rb = GetComponent<Rigidbody>();
        canClimb = false;
        climbTarget = GameObject.Find("ClimbTarget");
	}
	
	void Update () {
		if (canClimb && Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.Space) && Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 1))
        {
            rb.transform.position = Vector3.MoveTowards(transform.position, climbTarget.transform.position, 30 * Time.deltaTime);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            rb.velocity = Vector3.zero;
        }
	}

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "CityTerrain")
        {
            double height = collision.gameObject.GetComponent<Renderer>().bounds.extents.y * 2.5;
            climbTarget.transform.position = new Vector3(transform.position.x, (float)height + 3, transform.position.z);
            Debug.Log("Player: " + transform.position);
            Debug.Log("Height: " + height);
            Debug.Log("ClimbTarget: " + climbTarget.transform.position);
            canClimb = true;
            Debug.Log(canClimb);
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        Debug.Log("Exit");
        canClimb = false;
    }
}
