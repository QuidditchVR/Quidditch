using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Goal") {
            var a = GameManagerL2.instance;
            if (a) {
                a.goal();
            }
        }
    }
}
