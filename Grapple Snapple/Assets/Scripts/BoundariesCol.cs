using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundariesCol : MonoBehaviour {

    GameObject respawn;
    GameObject player;

    void Start()
    {
        respawn = GameObject.Find("Respawn");
        player = GameObject.Find("Player");
    }

	void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Player")
        {
            Debug.Log("Collide!");
            player.transform.position = respawn.transform.position;
            Grapple2.rb.velocity = new Vector3(0, 0, 0);
        }
    }
}
