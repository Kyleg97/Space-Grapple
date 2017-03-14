using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Line : MonoBehaviour
{

    private GameObject player;
    private GameObject lineObj;
    private GameObject crossX;
    private GameObject crossY;
    private Vector3 playerPos;
    private Vector3 lineHit;
    private LineRenderer line;
    private float distance = 170;

    void Start()
    {
        player = GameObject.Find("Player");
        lineObj = GameObject.Find("Line");
        crossX = GameObject.Find("CrosshairX");
        crossY = GameObject.Find("CrosshairY");
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;
        playerPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);

        if (line != null)
        {
            line.SetPosition(0, playerPos);
        }

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 150) && hit.collider.name != "InvisWall")
        {
            //Debug.Log("In Range!");
            crossX.GetComponent<Image>().color = new Color(0, 255, 0);
            crossY.GetComponent<Image>().color = new Color(0, 255, 0);
        }

        else
        {
            //Debug.Log("Out of Range!");
            crossX.GetComponent<Image>().color = new Color(255, 0, 0);
            crossY.GetComponent<Image>().color = new Color(255, 0, 0);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 150) && hit.collider.name != "InvisWall")
            {
                lineHit = hit.point;
                lineObj.AddComponent<LineRenderer>();
                line = lineObj.GetComponent<LineRenderer>();
                line.SetPosition(0, playerPos);
                line.SetPosition(1, lineHit);
                line.useWorldSpace = true;
                line.startWidth = 0.08f;
                line.endWidth = 0.06f;
                line.material = new Material(Shader.Find("Standard"));
                line.material.color = Color.black;
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            Destroy(line);
        }
    }
}