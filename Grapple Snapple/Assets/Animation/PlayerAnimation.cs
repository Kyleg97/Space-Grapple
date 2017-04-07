using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    public static Animator anim;
    public GameObject arms;
    public bool running;

	void Start () {
        arms = GameObject.Find("final_rig");
        anim = arms.GetComponent<Animator>();
        running = false;
	}
	
	void Update () {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.Play("Run", -1, 0f);
            running = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            anim.StopPlayback();
            anim.Play("Idle", -1, 0f);
        }
	}
}
