using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisWallTrigger : MonoBehaviour {

    GameObject wallCube;
    GameObject movingWall;
    Vector3 wallStartPos;
    Vector3 wallEndPos;

    void Start()
    {
        /*
        if (GameObject.Find("WallCube") != null)
        {
            wallCube = GameObject.Find("WallCube");
            wallCube.GetComponent<MeshRenderer>().enabled = false;
        }
        */

        movingWall = GameObject.Find("MovingWall");
        wallStartPos = movingWall.transform.position;
        wallEndPos = new Vector3(movingWall.transform.position.x, movingWall.transform.position.y - 50, movingWall.transform.position.z);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.U))
        {
            movingWall.transform.position = Vector3.Lerp(wallStartPos, wallEndPos, 20 * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger!");
        //movingWall.transform.position = Vector3.Lerp(wallStartPos, wallEndPos, 20);


        /*
        if (GameObject.Find("WallCube") != null)
        {
            wallCube.GetComponent<MeshRenderer>().enabled = true;
            wallCube.AddComponent<BoxCollider>();
        }
        */
    }
}
