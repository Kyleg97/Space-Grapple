using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScript;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public Canvas pauseMenu;
    public Canvas restartMenu;
    public MouseLook mouseLookScript;
    public Grapple grappleScript;
    public Projectile projectileScript;
    public Line lineScript;
    public BlurOptimized mainCam;

	void Start () {
        pauseMenu = GetComponent<Canvas>();
        restartMenu.enabled = false;
        pauseMenu.enabled = false;
        mainCam = GameObject.Find("Main Camera").GetComponent<BlurOptimized>();
        mouseLookScript = GameObject.Find("Main Camera").GetComponent<MouseLook>();
        grappleScript = GameObject.Find("Player").GetComponent<Grapple>();
        projectileScript = GameObject.Find("Player").GetComponent<Projectile>();
        lineScript = GameObject.Find("Line").GetComponent<Line>();
        mainCam.enabled = false;
        mouseLookScript.enabled = true;
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.enabled == false)
            {
                pauseMenu.enabled = true;
                mainCam.enabled = true;
                restartMenu.enabled = false;
                mouseLookScript.enabled = false;
                grappleScript.enabled = false;
                lineScript.enabled = false;
                projectileScript.enabled = false;
                //idk about this...
                //Destroy(Line.line);
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                pauseMenu.enabled = false;
                mainCam.enabled = false;
                restartMenu.enabled = false;
                mouseLookScript.enabled = true;
                grappleScript.enabled = true;
                lineScript.enabled = true;
                projectileScript.enabled = true;
                Time.timeScale = 1;
            }
        }
	}

    public void RestartPress()
    {
        restartMenu.enabled = true;
    }

    public void ResumePress()
    {
        pauseMenu.enabled = false;
        mainCam.enabled = false;
        mouseLookScript.enabled = true;
        Time.timeScale = 1;
        projectileScript.enabled = true;
        lineScript.enabled = true;
        grappleScript.enabled = true;
        
    }

    public void RestartPressYes()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        pauseMenu.enabled = false;
        mainCam.enabled = false;
        mouseLookScript.enabled = true;
        grappleScript.enabled = true;
        lineScript.enabled = true;
        projectileScript.enabled = true;
        Time.timeScale = 1;
    }

    public void RestartPressNo()
    {
        pauseMenu.enabled = true;
        mainCam.enabled = true;
        restartMenu.enabled = false;
        mouseLookScript.enabled = false;
        grappleScript.enabled = false;
        lineScript.enabled = false;
        projectileScript.enabled = false;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void QuitGamePress()
    {

    }

    public void QuitGamePressYes()
    {

    }

    public void QuitGamePressNo()
    {

    }
}
