using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookCollide : MonoBehaviour {

    public static bool canDestroy;
    public AudioSource grappleHit;

    void Start()
    {
        canDestroy = false;
    }

    void Update()
    {
        if (RingCollide.score)
        {
            transform.SetParent(null);
            Grapple2.mouseUp = true;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "Player" && collision.gameObject.name != "No")
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.SetParent(collision.transform);
            grappleHit = GetComponent<AudioSource>();
            grappleHit.Play();
        }
    }
}
