using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockRotate : MonoBehaviour {

    GameObject anchor;

	void Start () {
        anchor = GameObject.Find("RotateAnchor");
	}
	
	void Update () {
        transform.RotateAround(anchor.transform.position, Vector3.up, 20 * Time.deltaTime);
        transform.Rotate(Vector3.up * (20 * Time.deltaTime));
    }
}
