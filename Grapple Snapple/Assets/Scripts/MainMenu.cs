using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public Canvas quitMenu;
    public Canvas settingsMenu;
    public Button play;
    public Button settings;
    public Button scores;
    public Button exit;
    public Image vSyncCheck;

	void Start ()
    {
        quitMenu = quitMenu.GetComponent<Canvas>();
        settingsMenu = settingsMenu.GetComponent<Canvas>();
        play = play.GetComponent<Button>();
        settings = settings.GetComponent<Button>();
        scores = scores.GetComponent<Button>();
        exit = exit.GetComponent<Button>();
        vSyncCheck = vSyncCheck.GetComponent<Image>();
        quitMenu.enabled = false;
        settingsMenu.enabled = false;
	}

    public void ExitPress()
    {
        quitMenu.enabled = true;
        settingsMenu.enabled = false;
        play.enabled = false;
        settings.enabled = false;
        scores.enabled = false;
        exit.enabled = false;
    }

    public void NoPress()
    {
        quitMenu.enabled = false;
        settingsMenu.enabled = false;
        play.enabled = true;
        settings.enabled = true;
        scores.enabled = true;
        exit.enabled = true;
    }

    public void SettingsPress()
    {
        quitMenu.enabled = false;
        settingsMenu.enabled = true;
        play.enabled = false;
        settings.enabled = false;
        scores.enabled = false;
        exit.enabled = false;
        
        if (QualitySettings.vSyncCount == 0)
            vSyncCheck.enabled = false;

        if (QualitySettings.vSyncCount == 1)
            vSyncCheck.enabled = true;
        
    }

    public void ApplyPress()
    {
        quitMenu.enabled = false;
        settingsMenu.enabled = false;
        play.enabled = true;
        settings.enabled = true;
        scores.enabled = true;
        exit.enabled = true;
    }

    public void VsyncPress()
    {
        if (QualitySettings.vSyncCount == 0)
        {
            vSyncCheck.enabled = true;
            QualitySettings.vSyncCount = 1;
        }

        if (QualitySettings.vSyncCount == 1)
        {
            vSyncCheck.enabled = false;
            QualitySettings.vSyncCount = 0;
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
