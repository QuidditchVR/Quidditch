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
    private GameObject[] endEffects;

    // Use this for initialization
    void Start()
    {
        instance = this;
        text.text = sSpot + "/" + target;
        endEffects = GameObject.FindGameObjectsWithTag("EndEffect");
        foreach (var effect in endEffects) {
            effect.SetActive(false);
        }
    }

    public void goal()
    {
        ++sSpot;
        if (sSpot >= target)
        {
            foreach (var effect in endEffects) {
                effect.SetActive(true);
            }
        }
        text.text = sSpot + "/" + target;
    }
}
