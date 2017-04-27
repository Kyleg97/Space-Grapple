using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingCollide : MonoBehaviour {

    public static bool score;

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Player")
        {
            Debug.Log("Score!");
            score = true;
        }
    }
}
