using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCol : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "Player")
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.SetParent(collision.transform);
        }
    }
}