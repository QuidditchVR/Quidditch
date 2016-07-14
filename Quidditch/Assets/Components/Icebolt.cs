﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class Icebolt : MonoBehaviour
{

    private ParticleSystem particle;
    public GameObject explosionPrefab;

    // Use this for initialization
    void Start() {
        particle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update() {
        particle.startSize = transform.localScale.x;
    }

    void OnTriggerEnter(Collider col) {
        if (col.CompareTag("Enemy")) {
            return;
        }
        Destroy(gameObject);
        var explostion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explostion, 8.0f);
    }

    void OnCollisionEnter(Collision c) {
        if (c.collider.CompareTag("Enemy")) {
            return;
        }
        Destroy(gameObject);
        var explostion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explostion, 10.0f);
    }

    public void setDestroyTime(float time) {
        Destroy(gameObject, time);
    }
}
