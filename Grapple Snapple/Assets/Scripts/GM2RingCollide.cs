using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM2RingCollide : MonoBehaviour {

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Player")
        {
            GM1Score.canIncrement = true;
            Destroy(gameObject);
            //StartCoroutine(destroyWait());
        }
    }

    public IEnumerator destroyWait()
    {
        yield return new WaitForSeconds(.7f);
        Destroy(gameObject);

    }
}
