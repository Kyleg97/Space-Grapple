using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockRotate : MonoBehaviour {

    public float rotateSpeed;
    private GameObject anchor;
    //private float startDist;
    private float dist;

	void Start () {
        anchor = transform.parent.gameObject;
        //startDist = Vector3.Distance(transform.position, anchor.transform.position);
    }
	
	void Update () {
        transform.RotateAround(transform.parent.position, Vector3.up, rotateSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * (20 * Time.deltaTime));
        /*
        dist = Vector3.Distance(transform.position, anchor.transform.position);

        if(dist > startDist)
        {
            dist = startDist;
        }
        */
    }
}
