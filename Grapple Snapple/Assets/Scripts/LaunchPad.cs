using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPad : MonoBehaviour {

    public FPSInput fpsScript;
    public Grapple2 grappleScript;
    public Line2 lineScript;

    public AudioSource boop;
    public GameObject player;
    public GameObject line;
    public GameObject house;
    public Vector3 dir;

    void Start()
    {
        player = GameObject.Find("Player");
        house = GameObject.Find("House");
        line = GameObject.Find("Line");
        fpsScript = player.GetComponent<FPSInput>();
        grappleScript = player.GetComponent<Grapple2>();
        lineScript = line.GetComponent<Line2>();
        boop = GetComponent<AudioSource>();
    }

    void Update()
    {
        dir = house.transform.position - player.transform.position;
        dir = dir.normalized;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collision with launch");
            player.transform.position = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
            Grapple2.rb.constraints = RigidbodyConstraints.FreezeAll;

            if (Line2.line != null && Grapple2.hook != null && Grapple2.hookAnchor != null)
            {
                Destroy(Line2.line);
                Destroy(Grapple2.hook);
                Destroy(Grapple2.hookAnchor);
            }
            grappleScript.enabled = false;
            lineScript.enabled = false;
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
        Grapple2.rb.AddRelativeForce(-transform.up * 3200);
        boop.Play();
        Debug.Log("Launch!");
        StartCoroutine(launchForward());
    }

    public IEnumerator launchForward()
    {
        yield return new WaitForSeconds(1.5f);
        Grapple2.rb.AddForce(dir * 800);
        grappleScript.enabled = true;
        lineScript.enabled = true;
        fpsScript.enabled = true;
        Debug.Log("Forward!");
    }
}
