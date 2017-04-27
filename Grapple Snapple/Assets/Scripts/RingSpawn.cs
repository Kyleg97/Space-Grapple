using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingSpawn : MonoBehaviour {

    public GameObject ring;
    public GameObject ringInstance;
    public GameObject player;
    public Vector3 spawnSphereCenter;
    public int ringRotation;
    public Quaternion rotation;
    public bool canSpawn;
        
	void Start () {
        player = GameObject.Find("Player");
        spawnSphereCenter = player.transform.position;
        ring = Resources.Load("CIRCLE") as GameObject;
        ringInstance = Instantiate(ring) as GameObject;
        ringInstance.transform.position = spawnSphereCenter + Random.insideUnitSphere * 200;
        canSpawn = false;
    }
	
	void Update () {
		
        if (Input.GetKeyDown(KeyCode.Y))
        {
            player.transform.position = ringInstance.transform.position;
        }

        if (Input.GetKeyDown(KeyCode.M))// && canSpawn)
        {   
            switch (ringRotation)
            {
                case 1: rotation = Quaternion.Euler(30, 90, 0);
                    break;
                case 2: rotation = Quaternion.Euler(0, 180, 0);
                    break;
                case 3: rotation = Quaternion.Euler(0, 0, 0);
                    break;
                case 4: rotation = Quaternion.Euler(90, 0, 90);
                    break;
                case 5: rotation = Quaternion.Euler(180, 45, 30);
                    break;
                case 6: rotation = Quaternion.Euler(45, 90, 180);
                    break;
                case 7: rotation = Quaternion.Euler(30, 180, 90);
                    break;
            }

            ringRotation = Random.Range(1, 7);
            ringInstance = Instantiate(ring) as GameObject;
            ringInstance.transform.position = spawnSphereCenter + Random.insideUnitSphere * 200;
            ringInstance.transform.rotation = rotation;
            canSpawn = false;
        }

        if (RingCollide.score)
        {
            Destroy(ringInstance);
            canSpawn = true;
            //Grapple2.enabled = false;
        }
    }
}
