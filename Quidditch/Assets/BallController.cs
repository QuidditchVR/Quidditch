using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Ball")
        {
            var a = GameManagerL2.instance;
            a.nextHoop();
            Debug.Log("You scored!");
            //Destroy(gameObject);
        }
    }
}
