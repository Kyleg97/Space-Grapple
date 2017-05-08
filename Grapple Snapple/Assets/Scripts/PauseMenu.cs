using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public Canvas pauseMenu;
    public Canvas restartMenu;
    public Canvas quitMenu;
    public Canvas controlMenu;
    public Canvas optionsMenu;

    public Slider sensitivitySlider;
    public MouseLook mouseLookScript;
    public Grapple2 grappleScript;
    public Projectile projectileScript;
    public Line2 lineScript;
    public BlurOptimized blurScript;
    public AudioSource music;

	void Start () {
        restartMenu.enabled = false;
        quitMenu.enabled = false;
        pauseMenu.enabled = false;
        controlMenu.enabled = false;
        optionsMenu.enabled = false;
        blurScript = GameObject.Find("Main Camera").GetComponent<BlurOptimized>();
        mouseLookScript = GameObject.Find("Main Camera").GetComponent<MouseLook>();
        grappleScript = GameObject.Find("Player").GetComponent<Grapple2>();
        projectileScript = GameObject.Find("Player").GetComponent<Projectile>();
        lineScript = GameObject.Find("Line").GetComponent<Line2>();
        music = GetComponent<AudioSource>();
        blurScript.enabled = false;
        mouseLookScript.enabled = true;
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.enabled == false)
            {
                Destroy(Line2.line);
                Destroy(Grapple2.hook);
                //Destroy(Grapple2.hookAnchor);
                pauseMenu.enabled = true;
                blurScript.enabled = true;
                optionsMenu.enabled = false;
                controlMenu.enabled = false;
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
                blurScript.enabled = false;
                restartMenu.enabled = false;
                controlMenu.enabled = false;
                optionsMenu.enabled = false;
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
            blurScript.enabled = false;
            optionsMenu.enabled = false;
            restartMenu.enabled = false;
            controlMenu.enabled = false;
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
        blurScript.enabled = false;
        optionsMenu.enabled = false;
        restartMenu.enabled = false;
        controlMenu.enabled = false;
        mouseLookScript.enabled = true;
        grappleScript.enabled = true;
        lineScript.enabled = true;
        projectileScript.enabled = true;
        Grapple2.hookDestroyed = true;
        //headbobScript.enabled = true;
        Time.timeScale = 1;
    }

    public void ControlPress()
    {
        pauseMenu.enabled = true;
        blurScript.enabled = true;
        controlMenu.enabled = true;
        quitMenu.enabled = false;
        restartMenu.enabled = false;
        optionsMenu.enabled = false;
        mouseLookScript.enabled = false;
        grappleScript.enabled = false;
        lineScript.enabled = false;
        projectileScript.enabled = false;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OptionsPress()
    {
        pauseMenu.enabled = true;
        blurScript.enabled = true;
        controlMenu.enabled = false;
        optionsMenu.enabled = true;
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

    public void RestartPressYes()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        pauseMenu.enabled = false;
        blurScript.enabled = false;
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
        blurScript.enabled = true;
        quitMenu.enabled = false;
        controlMenu.enabled = false;
        optionsMenu.enabled = false;
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
        if (!restartMenu.enabled)
        {
            pauseMenu.enabled = true;
            blurScript.enabled = true;
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
    }

    public void QuitGamePressYes()
    {
        Application.Quit();
    }

    public void QuitGamePressNo()
    {
        pauseMenu.enabled = true;
        blurScript.enabled = true;
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

    public void SensitivityChange(float value)
    {
        MouseLook.sensitivity = new Vector2(value, value);
    }

    public void MusicChange(float value)
    {
        music.volume = value / 10;
    }
}
