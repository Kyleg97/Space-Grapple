using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public GameObject prefab;
    public GameObject projectile;
    Rigidbody rb;
    bool offCd = false;
    IEnumerator projectileFun;


    void Start()
    {
        prefab = Resources.Load("Projectile") as GameObject;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2") && GameObject.Find("Projectile(Clone)") == null)
        {
            //Debug.Log("shoot!");
            Vector3 playerPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            projectile = Instantiate(prefab) as GameObject;
            projectile.GetComponent<Rigidbody>().isKinematic = false;
            projectile.transform.position = playerPos + Camera.main.transform.forward * 2;
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = Camera.main.transform.forward * 100;
            //small gap to let the player right click again
            StartCoroutine(offCdFun());
            //assigned to later pass as param to StopCoroutine
            projectileFun = projectileTimeOut();
            //starts timer of 4.5s to destroy the projectile after its shot
            StartCoroutine(projectileFun);
        }

        if (GameObject.Find("Projectile(Clone)") != null)
        {
            //Debug.Log("found!");
            //if the player right clicks and the coroutine for offCd has finished
            if (Input.GetButtonDown("Fire2") && offCd == true)
            {
                //Debug.Log("destroy!");
                transform.position = projectile.transform.position;
                Grapple2.rb.velocity = new Vector3(0, 0, 0);
                Destroy(projectile);
                Destroy(ProjectileCol.projectileParent);
                //stops 4.5s timer
                StopCoroutine(projectileFun);
            }
        }
    }

    public IEnumerator offCdFun()
    {
        offCd = false;
        yield return new WaitForSeconds(0.1f);
        offCd = true;
    }

    public IEnumerator projectileTimeOut()
    {
        yield return new WaitForSeconds(4.5f);
        Destroy(projectile);
        Destroy(ProjectileCol.projectileParent);
    }
}