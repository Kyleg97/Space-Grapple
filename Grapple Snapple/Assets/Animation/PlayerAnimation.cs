using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    public static Animator anim;
    public GameObject arms;
    public bool running;

	void Start () {
        arms = GameObject.Find("Arms");
        anim = arms.GetComponent<Animator>();
        running = false;
	}
	
	void Update () {

        if (Input.GetButtonDown("Fire1"))
        {
            anim.Play("ThrowGrapple", -1, 0f);
        }

        if (Input.GetKey(KeyCode.LeftShift) && !running)
        {
            running = true;
            anim.Play("Running", -1, 0f);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            running = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.Play("Jump", -1, 0f);
        }
	}
}
