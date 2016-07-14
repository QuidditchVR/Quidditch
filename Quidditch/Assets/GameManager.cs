using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public GameObject[] hoops;
    public int cHoop = 0;
    public static GameManager instance;


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
        
        if (cHoop == 10)
        {
            //GetComponent<ParticleSystem>().Play();
            Debug.Log("Done");
        }

        else
        {

            Debug.Log("NextHoop");
            //hoops[cHoop].GetComponent<Renderer>().enabled = false;
            hoops[cHoop].SetActive(false);

            ++cHoop;
            //hoops[cHoop].GetComponent<Renderer>().material.color = Color.green;
            //hoops[cHoop].GetComponent<Renderer>().enabled = true;
            hoops[cHoop].SetActive(true);

        }

        //hoops[cHoop].mater
    }
}
