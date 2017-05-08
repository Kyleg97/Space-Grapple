using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class RingSpawn : MonoBehaviour {

    public GameObject ring;
    public GameObject ringInstance;
    public GameObject player;

    public GameObject cam;

    public Vector3 spawnSphereCenter;
    public int ringRotation;
    public Quaternion rotation;
    public bool canSpawn;

    public GameObject line;

    public int playerScore;
    public float timeLeft;

    public float timeUntilStart;
    public Text _timeUntilStart;

    public Text scoreText;
    public Text timeText;
    public Text finalScore;

    public AudioSource warpSpawn;

    public MouseLook mouseScript;
    public FPSInput fpsScript;
    public Grapple2 grappleScript;
    public Line2 lineScript;
    public RocketBoots rocketScript;
    public BlurOptimized blurScript;

    public static bool _gameOver;
    public Canvas gameOver;


    void Start () {
        player = GameObject.Find("Player");
        cam = GameObject.Find("Main Camera");
        line = GameObject.Find("Line");
        spawnSphereCenter = player.transform.position;
        ring = Resources.Load("CIRCLE") as GameObject;
        ringInstance = Instantiate(ring) as GameObject;
        ringInstance.transform.position = spawnSphereCenter + Random.insideUnitSphere * 180;
        canSpawn = false;
        gameOver.enabled = false;
        playerScore = 0;
        timeLeft = 5.0f;
        timeUntilStart = 4;

        blurScript = cam.GetComponent<BlurOptimized>();
        mouseScript = cam.GetComponent<MouseLook>();
        fpsScript = player.GetComponent<FPSInput>();
        grappleScript = player.GetComponent<Grapple2>();
        lineScript = line.GetComponent<Line2>();
        rocketScript = player.GetComponent<RocketBoots>();
        blurScript.enabled = false;
        _gameOver = false;
    }
	
	void Update () {

        if (timeUntilStart > 1)
        {
            mouseScript.enabled = false;
            fpsScript.enabled = false;
            grappleScript.enabled = false;
            lineScript.enabled = false;
            rocketScript.enabled = false;
            blurScript.enabled = true;
            timeUntilStart -= Time.deltaTime;
        }

        if (timeUntilStart < 1)
        {
            timeUntilStart = 0;
            _timeUntilStart.enabled = false;
            mouseScript.enabled = true;
            fpsScript.enabled = true;
            grappleScript.enabled = true;
            lineScript.enabled = true;
            rocketScript.enabled = true;
            blurScript.enabled = false;
        }

        if (timeUntilStart <= 0)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 1)
            {
                timeLeft = 0;
                timeText.text = "Time: " + 0;
                GameOver();
            }
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            player.transform.position = ringInstance.transform.position;
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
        timeText.text = "Time: " + timeLeft.ToString("F2");
        finalScore.text = "FINAL SCORE:  " + playerScore;
        _timeUntilStart.text = (int)timeUntilStart + "";
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
        warpSpawn = ringInstance.GetComponent<AudioSource>();
        warpSpawn.Play();
        ringInstance.transform.position = spawnSphereCenter + Random.insideUnitSphere * 180;
        ringInstance.transform.rotation = rotation;
        canSpawn = false;
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

    public void GameOver()
    {
        Time.timeScale = 0;
        mouseScript.enabled = false;
        fpsScript.enabled = false;
        grappleScript.enabled = false;
        lineScript.enabled = false;
        rocketScript.enabled = false;
        blurScript.enabled = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _gameOver = true;
        gameOver.enabled = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
