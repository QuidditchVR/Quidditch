using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManagerL2 : MonoBehaviour
{

    public Vector3[] shotSpots;
    public int sSpot = 0;
    public int target = 5;
    public static GameManagerL2 instance;
    public Text text;
    public GameObject SceneRings;

    // Use this for initialization
    void Start()
    {
        instance = this;
        text.text = sSpot + "/" + target;
    }

    public void goal()
    {
        ++sSpot;
        if (sSpot >= target)
        {
            SceneRings.SetActive(true);
        }
        text.text = sSpot + "/" + target;
    }
}
