using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour {

    public static Text timerText;

    public float seconds;
    public float minutes;

	void Start () {
        timerText = GetComponent<Text>() as Text;
	}
	
	void Update () {
        minutes = (int) (Time.timeSinceLevelLoad / 60f);
        seconds = (int) (Time.timeSinceLevelLoad % 60f);
        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");

        if (GM1Score.score > 0)
        {
            seconds -= GM1Score.score * 3;
        }
	}
}
