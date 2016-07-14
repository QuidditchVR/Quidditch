using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class Firebolt : MonoBehaviour {

    private ParticleSystem particle;

	// Use this for initialization
	void Start () {
        particle = GetComponent<ParticleSystem>();	
	}
	
	// Update is called once per frame
	void Update () {
        particle.startSize = transform.localScale.x;
	}
}
