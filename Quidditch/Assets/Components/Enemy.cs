using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public enum EnemyState {
        Idle,
        Escape,
        Death
    }

    public Transform playerHeadPosition;
    public GameObject iceboltPrefab;
    public Transform spawnIceboltPoint;
    private float iceboltSpeed = 30.0f;
    private float iceboltDestroyTime = 8.0f;
    private float iceboltDuration = 2.0f;
    private float attackDistanceThreshold = 50.0f;
    private float escapeDistanceThreshold = 10.0f;
    private float escapeSpeed = 30.0f;
    private float escapeDistance = 50.0f;

    private Vector3 startPos;
    private float rnd;
    private EnemyState state;
    private float nextAttackTime;
    private Vector3 escapePosition;

	// Use this for initialization
	void Start () {
        rnd = Random.value * 360;
        startIdle();
    }
	
	// Update is called once per frame
	void Update () {
        if (state == EnemyState.Idle) {
            nextAttackTime -= Time.deltaTime;
            transform.position = startPos + Vector3.up * Mathf.Sin(Time.time + rnd) * 3;
            transform.LookAt(playerHeadPosition.transform);
            //Vector3 f = transform.forward;
            //f.y = Mathf.Clamp(f.y, -0.3f, 0.3f);
            //transform.forward = f;
            //Debug.Log(transform.forward);

            float distance = getDistanceFromPlayer();
            if (nextAttackTime < 0 && distance < attackDistanceThreshold) {
                shootIcebolt();
            }

            if (distance < escapeDistanceThreshold) {
                startEscape();
            }
        } else if (state == EnemyState.Escape) {
            Vector3 diff = escapePosition - transform.position;
            if (diff.magnitude < 1.0f) {
                startIdle();
            } else {
                GetComponent<Rigidbody>().velocity = diff.normalized * escapeSpeed;
                transform.LookAt(escapePosition);
            }
        }
    }

    float getDistanceFromPlayer() {
        var diff = playerHeadPosition.position - transform.position;
        return diff.magnitude;
    }

    void OnTriggerEnter(Collider col) {
        if (col.CompareTag("FireBolt")) {
            startDeath();   
        }
    }

    void shootIcebolt() {
        Debug.Log("Shoot Icebolt");
        var direction = (playerHeadPosition.transform.position - transform.position).normalized;
        var icebolt = Instantiate(iceboltPrefab, spawnIceboltPoint.position, Quaternion.identity) as GameObject;
        icebolt.GetComponent<Rigidbody>().isKinematic = false;
        icebolt.GetComponent<Rigidbody>().velocity = direction * iceboltSpeed;
        icebolt.GetComponent<Icebolt>().setDestroyTime(iceboltDestroyTime);
        nextAttackTime = iceboltDuration;
    }

    void startIdle() {
        state = EnemyState.Idle;
        startPos = transform.position;
        nextAttackTime = iceboltDuration;
    }

    void startEscape() {
        state = EnemyState.Escape;
        var diff = transform.position - playerHeadPosition.position;
        diff.y = 0;
        escapePosition = transform.position + diff.normalized * escapeDistance;
    }

    void startDeath() {
        state = EnemyState.Death;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        GetComponent<Rigidbody>().AddTorque(new Vector3(Random.value * 1000, Random.value * 1000, Random.value * 1000));
        Destroy(gameObject, 3.0f);
    }
}
