﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM1Score : MonoBehaviour {

    public static int score;
    public static bool canIncrement;

    public Text scoreText;

	void Start () {
        score = 0;
	}

	void Update () {
		if (canIncrement)
        {
            score++;
            canIncrement = false;
            Debug.Log("Score: " + score);
        }
	}
}