using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManagerL2 : MonoBehaviour
{

    public Vector3[] shotSpots;
    public int sSpot = 0;
    public static GameManagerL2 instance;
    public Text text;
    public GameObject player;
    public GameObject ball;
    public GameObject controller;

    // Use this for initialization
    void Start()
    {
        instance = this;

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void nextHoop()
    {
        //shotSpots[sSpot].SetActive(false);
        ++sSpot;
        if (sSpot == 5)
        {
            //fireworks
            GetComponent<ParticleSystem>().Play();
            Debug.Log("Done");
            //shotSpots[sSpot].SetActive(false);
        }
        else
        {
            player.transform.position = shotSpots[sSpot];
            //
            //spawn ball
            //Instantiate(ball, controller.transform.position, Quaternion.identity);
            ball.transform.SetParent(controller.transform);
            ball.GetComponent<Rigidbody>().isKinematic = true;
            ball.transform.position = controller.transform.position;
            //ball.tag = "Ball";
            //controller.GetComponent<ThrowBall>().ball = ball;

        }
        text.text = sSpot + "/5";
    }
}
