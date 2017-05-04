using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RingSpawn : MonoBehaviour {

    public GameObject ring;
    public GameObject ringInstance;
    public GameObject player;
    public Vector3 spawnSphereCenter;
    public int ringRotation;
    public Quaternion rotation;
    public bool canSpawn;
    public bool canCount;
    public Grapple2 grappleScript;
    public Line2 lineScript;
    public GameObject line;
    public int playerScore;
    public Text scoreText;

    void Start () {
        player = GameObject.Find("Player");
        line = GameObject.Find("Line");
        grappleScript = player.GetComponent<Grapple2>();
        lineScript = line.GetComponent<Line2>();
        spawnSphereCenter = player.transform.position;
        ring = Resources.Load("CIRCLE") as GameObject;
        ringInstance = Instantiate(ring) as GameObject;
        ringInstance.transform.position = spawnSphereCenter + Random.insideUnitSphere * 180;
        canSpawn = false;
        canCount = true;
        playerScore = 0;
    }
	
	void Update () {
		
        if (Input.GetKeyDown(KeyCode.Y))
        {
            player.transform.position = ringInstance.transform.position;
        }
        
        if (ringInstance != null && canCount)
        {
            if (canCount)
            {
                //StartCoroutine(spawnWait());
                canCount = false;
            }
        }
        
        if (ringInstance == null && canSpawn)
        {
            spawn();
            canSpawn = false;
        }

        if (Grapple2.hook != null  && Grapple2.hook.transform.IsChildOf(ringInstance.transform))
        {
            //Debug.Log("hi mom");
        }

        if(Grapple2.hook == null && RingCollide.score)
        {
            RingCollide.score = false;
            playerScore++;
            Debug.Log("Score: " + playerScore);
            Destroy(ringInstance);
            canSpawn = true;
        }

        if(Grapple2.hook != null)
        {
            if (RingCollide.score && Grapple2.hook.transform.IsChildOf(ringInstance.transform))
            {
                Destroy(Grapple2.hook);
                Destroy(Line2.line);
                Grapple2.hookDestroyed = true;
                canSpawn = true;
                RingCollide.score = false;
                playerScore++;
                Debug.Log("Score: " + playerScore);
                Destroy(ringInstance);
            }
        }
    }

    void FixedUpdate()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        scoreText.text = "Score: " + playerScore;
    }

    public void spawn()
    {
        switch (ringRotation)
        {
            case 1:
                rotation = Quaternion.Euler(30, 90, 0);
                break;
            case 2:
                rotation = Quaternion.Euler(0, 180, 0);
                break;
            case 3:
                rotation = Quaternion.Euler(0, 0, 0);
                break;
            case 4:
                rotation = Quaternion.Euler(90, 0, 90);
                break;
            case 5:
                rotation = Quaternion.Euler(180, 45, 30);
                break;
            case 6:
                rotation = Quaternion.Euler(45, 90, 180);
                break;
            case 7:
                rotation = Quaternion.Euler(30, 180, 90);
                break;
        }
        ringRotation = Random.Range(1, 7);
        ringInstance = Instantiate(ring) as GameObject;
        ringInstance.transform.position = spawnSphereCenter + Random.insideUnitSphere * 180;
        ringInstance.transform.rotation = rotation;
        canSpawn = false;
        //canCount = true;
        //spawnWaitFun();
    }

    public void spawnWaitFun()
    {
        StartCoroutine(spawnWait());
    }

    public IEnumerator spawnWait()
    {
        yield return new WaitForSeconds(10);

        if (ringInstance != null)
        {
            Destroy(ringInstance);
            Grapple2.hookDestroyed = true;
            canSpawn = true;
        }
    }
}
