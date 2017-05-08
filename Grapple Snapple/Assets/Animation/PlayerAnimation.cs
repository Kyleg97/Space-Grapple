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

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        if (Grapple2.canThrow)
        {
            anim.Play("ThrowGrapple", -1, 0f);
        }

        if (Input.GetKey(KeyCode.LeftShift) && Grapple2.isGrounded())
        {
            running = true;
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Running") && running)
            {
                anim.Play("Running", -1, 0f);
            }
            else
                running = false;
        }

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            running = false;
        }

        if (FPSInput.jumpAni)
        {
            anim.Play("Jump", -1, 0f);
        }
	}
}
