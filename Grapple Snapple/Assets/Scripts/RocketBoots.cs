using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketBoots : MonoBehaviour {

    float currentFuel = 5;
    float maxFuel = 5;
    bool flying;

    public Image fuelImage;
    public Text fuelText;

    public GameObject ringSpawn;

    public Rigidbody rb;

    public float maxVelocity = 45f;

    public AudioSource startRocket;
    public AudioSource loopRocket;

	void Start () {
        rb = GetComponent<Rigidbody>();
        ringSpawn = GameObject.Find("RingSpawn");

        startRocket = GameObject.Find("RocketBootsSound").GetComponent<AudioSource>();
        loopRocket = GameObject.Find("RocketBootsSound2").GetComponent<AudioSource>();

        flying = false;
	}
	
	void FixedUpdate() {

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        UpdateUI();

        if (ringSpawn != null && Input.GetKey(KeyCode.F) && currentFuel != 0)
        {
            flying = true;
        }

        if (flying)
        {
            if (Input.GetKeyDown(KeyCode.F) && !startRocket.isPlaying)
            {
                startRocket.Play();
            }

            if (!startRocket.isPlaying && !loopRocket.isPlaying)
            {
                loopRocket.Play();
            }

            currentFuel -= Time.deltaTime * 1.2f;
            rb.AddExplosionForce(currentFuel * 15, rb.transform.position, currentFuel * 15);

            if (currentFuel <= 0)
            {
                currentFuel = 0;
                flying = false;
                loopRocket.Stop();
                startRocket.Stop();
            }

            if (!Input.GetKey(KeyCode.F))
            {
                flying = false;
                loopRocket.Stop();
                startRocket.Stop();
            }

            if (RingSpawn._gameOver)
            {
                loopRocket.Stop();
                startRocket.Stop();
            }
        }

        else if (currentFuel < maxFuel || !flying && currentFuel < maxFuel)
        {
            currentFuel += Time.deltaTime;
        }

        if (rb.velocity.y > maxVelocity)
        {
            Debug.Log(rb.velocity);
            rb.velocity -= rb.velocity / 2f;
        }

        if(!Grapple2.isGrounded())
        {
            rb.AddForce(transform.forward * moveZ * 12);
            rb.AddForce(transform.right * moveX * 12);
        }
	}

    public void UpdateUI()
    {
        fuelImage.fillAmount = currentFuel / maxFuel;
        fuelText.text = "Fuel: " + ((int)(currentFuel / maxFuel * 100)).ToString() + "%";
    }
}
