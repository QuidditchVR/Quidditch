using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class Firebolt : MonoBehaviour {

    private ParticleSystem particle;
    public GameObject explosionPrefab;
    public AudioSource audio;
    public AudioClip clip;

    // Use this for initialization
    void Start () {
        particle = GetComponent<ParticleSystem>();	
	}
	
	// Update is called once per frame
	void Update () {
        particle.startSize = transform.localScale.x;
	}

    void OnTriggerEnter(Collider col) {
        if (col.CompareTag("Player")) {
            return;
        }
        Destroy(gameObject);
        var explostion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        //audio
        audio.PlayOneShot(clip);
        Destroy(explostion, 10.0f);
    }

    void OnCollisionEnter(Collision c) {
        if (c.collider.CompareTag("Player")) {
            return;
        }
        Destroy(gameObject);
        var explostion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        //audio
        audio.PlayOneShot(clip);
        Destroy(explostion, 10.0f);
    }

    public void setDestroyTime(float time) {
        Destroy(gameObject, time);
    }
}
