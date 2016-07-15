using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject[] hoops;
    public int cHoop = 0;
    public static GameManager instance;
    public Text text;
    private GameObject[] endEffects;

    // Use this for initialization
    void Start()
    {
        instance = this;
        endEffects = GameObject.FindGameObjectsWithTag("EndEffect");
        foreach(var effect in endEffects) {
            effect.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void nextHoop()
    {
        hoops[cHoop].SetActive(false);
        ++cHoop;
        if (cHoop == 10)
        {
            //fireworks
            GetComponent<ParticleSystem>().Play();
            Debug.Log("Done");
            hoops[cHoop].SetActive(false);
            foreach (var effect in endEffects) {
                effect.SetActive(true);
            }
        } else
        {
            hoops[cHoop].SetActive(true);
        }
        text.text = cHoop + "/10";
    }
}
