using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCol : MonoBehaviour
{
    public GameObject prefab;
    public static GameObject projectileParent;

    void Start()
    {
        prefab = Resources.Load("ParentOfProjectile") as GameObject;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "Player")
        {
            GetComponent<Rigidbody>().isKinematic = true;
            projectileParent = Instantiate(prefab) as GameObject;
            projectileParent.transform.SetParent(collision.transform);
            transform.SetParent(projectileParent.transform);
        }
    }
}