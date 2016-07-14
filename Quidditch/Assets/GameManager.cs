using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject[] hoops;
    public int cHoop = 0;
    public static GameManager instance;
    public Text text;

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
        hoops[cHoop].SetActive(false);
        ++cHoop;
        if (cHoop == 11)
        {
            //fireworks
            GetComponent<ParticleSystem>().Play();
            Debug.Log("Done");
            hoops[cHoop].SetActive(false);
        }
        else
        {
            hoops[cHoop].SetActive(true);
        }
        text.text = cHoop + "/12";
    }
}
