using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisWallTrigger : MonoBehaviour {

    GameObject wallCube;

    void Start()
    {
        wallCube = GameObject.Find("WallCube");
        wallCube.GetComponent<MeshRenderer>().enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger!");
        wallCube.GetComponent<MeshRenderer>().enabled = true;
        wallCube.AddComponent<BoxCollider>();
    }
}
