using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockRotate : MonoBehaviour {

    public float rotateSpeed;

	void Start () {
        //anchor = GameObject.Find("RotateAnchor");
	}
	
	void Update () {
        transform.RotateAround(transform.parent.position, Vector3.up, rotateSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * (20 * Time.deltaTime));
    }
}
