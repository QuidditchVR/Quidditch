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
            //shotSpots[sSpot].SetActive(true);
        }
        text.text = sSpot + "/12";
    }
}
