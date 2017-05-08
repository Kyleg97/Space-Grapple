using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class gm1Finish : MonoBehaviour {

    public GameObject player;
    public GameObject cam;
    public GameObject line;

    public MouseLook mouseScript;
    public FPSInput fpsScript;
    public Grapple2 grappleScript;
    public Line2 lineScript;
    public BlurOptimized blurScript;

    public float finalTime;

    public Text scoreText;

    public Text endScoreText;
    public Text timeText;
    public Text finalTimeText;

    public static bool _gameOver;
    public Canvas gameOver;

    void Start()
    {
        player = GameObject.Find("Player");
        cam = GameObject.Find("Main Camera");
        line = GameObject.Find("Line");
        blurScript = cam.GetComponent<BlurOptimized>();
        mouseScript = cam.GetComponent<MouseLook>();
        fpsScript = player.GetComponent<FPSInput>();
        grappleScript = player.GetComponent<Grapple2>();
        lineScript = line.GetComponent<Line2>();
        gameOver.enabled = false;
        _gameOver = false;
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.name == "Player")
        {
            GameOver();
        }
    }

    public void UpdateUI()
    {
        scoreText.text = "Score: " + GM1Score.score;
        timeText.text = "TIME: " + TimerUI.timerText.text;
        endScoreText.text = "SCORE: " + GM1Score.score;
        finalTimeText.text = "FINAL TIME:  " + timeText.text;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        mouseScript.enabled = false;
        fpsScript.enabled = false;
        grappleScript.enabled = false;
        lineScript.enabled = false;
        blurScript.enabled = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _gameOver = true;
        gameOver.enabled = true;
    }
}
