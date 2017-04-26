using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoopCollide : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Player")
        {
            Debug.Log("Score!");
        }
    }
}
