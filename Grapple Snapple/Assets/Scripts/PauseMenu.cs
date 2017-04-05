using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public Canvas pauseMenu;
    public Canvas restartMenu;
    public Canvas quitMenu;
    public MouseLook mouseLookScript;
    public Grapple2 grappleScript;
    public Projectile projectileScript;
    public Line2 lineScript;
    //public Headbob headbobScript;
    public BlurOptimized mainCam;

	void Start () {
        pauseMenu = GetComponent<Canvas>();
        restartMenu.enabled = false;
        quitMenu.enabled = false;
        pauseMenu.enabled = false;
        mainCam = GameObject.Find("Main Camera").GetComponent<BlurOptimized>();
        mouseLookScript = GameObject.Find("Main Camera").GetComponent<MouseLook>();
        grappleScript = GameObject.Find("Player").GetComponent<Grapple2>();
        projectileScript = GameObject.Find("Player").GetComponent<Projectile>();
        lineScript = GameObject.Find("Line").GetComponent<Line2>();
        //headbobScript = GameObject.Find("Main Camera").GetComponent<Headbob>();
        mainCam.enabled = false;
        mouseLookScript.enabled = true;
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.enabled == false)
            {
                Destroy(Line2.line);
                Destroy(Grapple2.hook);
                Destroy(Grapple2.hookAnchor);
                pauseMenu.enabled = true;
                mainCam.enabled = true;
                restartMenu.enabled = false;
                mouseLookScript.enabled = false;
                grappleScript.enabled = false;
                lineScript.enabled = false;
                projectileScript.enabled = false;
                //headbobScript.enabled = false;
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
                Grapple2.hookDestroyed = true;
                //headbobScript.enabled = true;
                Time.timeScale = 1;
            }
        }

        if (FPSInput.canDisable)
        {
            pauseMenu.enabled = false;
            mainCam.enabled = false;
            restartMenu.enabled = false;
            mouseLookScript.enabled = false;
            grappleScript.enabled = false;
            lineScript.enabled = false;
            projectileScript.enabled = false;
            //headbobScript.enabled = false;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
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
        Grapple2.hookDestroyed = true;
        //headbobScript.enabled = true;
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
        //headbobScript.enabled = true;
        Time.timeScale = 1;
    }

    public void RestartPressNo()
    {
        pauseMenu.enabled = true;
        mainCam.enabled = true;
        quitMenu.enabled = false;
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
        pauseMenu.enabled = true;
        mainCam.enabled = true;
        quitMenu.enabled = true;
        restartMenu.enabled = false;
        mouseLookScript.enabled = false;
        grappleScript.enabled = false;
        lineScript.enabled = false;
        projectileScript.enabled = false;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void QuitGamePressYes()
    {
        Application.Quit();
    }

    public void QuitGamePressNo()
    {
        pauseMenu.enabled = true;
        mainCam.enabled = true;
        quitMenu.enabled = false;
        restartMenu.enabled = false;
        mouseLookScript.enabled = false;
        grappleScript.enabled = false;
        lineScript.enabled = false;
        projectileScript.enabled = false;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
