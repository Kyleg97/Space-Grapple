using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPad : MonoBehaviour {

    public FPSInput fpsScript;
    public AudioSource boop;

    void Start()
    {
        fpsScript = GameObject.Find("Player").GetComponent<FPSInput>();
        boop = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("Collision with launch");
            boop.Play();
            Grapple2.rb.constraints = RigidbodyConstraints.FreezeAll;
            fpsScript.enabled = false;
            StartCoroutine(launchWait());
        }
    }

    public IEnumerator launchWait()
    {
        Debug.Log("Starting launch...");
        yield return new WaitForSeconds(1.5f);
        Grapple2.rb.constraints = RigidbodyConstraints.None;
        Grapple2.rb.constraints = RigidbodyConstraints.FreezeRotation;
        Grapple2.rb.AddRelativeForce(-transform.up * 3500);
        Debug.Log("Launch!");
        fpsScript.enabled = true;
        StartCoroutine(launchForward());
    }

    public IEnumerator launchForward()
    {
        yield return new WaitForSeconds(1.5f);
        Grapple2.rb.AddRelativeForce(transform.forward * 750);
        Debug.Log("Forward!");
    }
}
