using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManagerL2 : MonoBehaviour
{

    public Vector3[] shotSpots;
    public int sSpot = 0;
    public static GameManagerL2 instance;
    public Text text;
    private GameObject[] endEffects;
    public List<Goal> goals;


    // Use this for initialization
    void Start()
    {
        goals = new List<Goal>();
        var goalObjs = GameObject.FindGameObjectsWithTag("Goal");
        foreach (var obj in goalObjs) {
            var goal = obj.GetComponent<Goal>();
            if (goal) {
                goals.Add(goal);
            }
        }

        instance = this;
        text.text = sSpot + "/" + goals.Count;
        endEffects = GameObject.FindGameObjectsWithTag("EndEffect");
        foreach (var effect in endEffects) {
            effect.SetActive(false);
        }
    }

    public void goal()
    {
        sSpot = 0;
        foreach(var goal in goals) {
            if (goal.hasGoaled) {
                sSpot++;
            }
        }
        if (sSpot >= goals.Count)
        {
            foreach (var effect in endEffects) {
                effect.SetActive(true);
            }
        }
        text.text = sSpot + "/" + goals.Count;
    }
}
