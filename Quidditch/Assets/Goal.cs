using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

    GameObject goalParticle;
    public bool hasGoaled;

	// Use this for initialization
	void Start () {
        goalParticle = transform.FindChild("goalParticle").gameObject;
        goalParticle.SetActive(false);
	}

    void OnTriggerEnter(Collider col) {
        if (col.CompareTag("Ball")) {
            goalParticle.SetActive(true);
            hasGoaled = true;
        }
    }
	
}

