using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public enum EnemyState {
		Seek,
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
    private float attackDistanceThreshold = 40.0f;
    private float escapeDistanceThreshold = 10.0f;
    private float escapeSpeed = 30.0f;
    private float escapeDistance = 50.0f;
	private float seekSpeed = 10.0f;
	private Vector3 fieldMax = new Vector3(50, 75, 80);
	private Vector3 fieldMin = new Vector3(-50, 5, -80);

    private Vector3 startPos;
    private float rnd;
    private EnemyState state;
    private float nextAttackTime;
    private Vector3 escapePosition;

	// Use this for initialization
	void Start () {
        rnd = Random.value * 360;
		if (getDistanceFromPlayer () > 60) {
			startSeek ();
		} else {
			startIdle ();
		}
    }
	
	// Update is called once per frame
	void Update () {
		if (state == EnemyState.Seek) {
			updateSeek ();
		} else if (state == EnemyState.Idle) {
			updateIdle ();
        } else if (state == EnemyState.Escape) {
			updateEscape ();
        }
    }

	void updateSeek() {
		if (getDistanceFromPlayer () < attackDistanceThreshold) {
			startIdle ();
		} else {
			Vector3 diff = escapePosition - transform.position;
			if (diff.magnitude < 1.0f) {
				startSeek ();
			} else {
				GetComponent<Rigidbody> ().velocity = diff.normalized * seekSpeed;
				transform.LookAt (escapePosition);
			}
		}
	}

	void updateIdle() {
		nextAttackTime -= Time.deltaTime;
		transform.position = startPos + Vector3.up * Mathf.Sin(Time.time + rnd) * 3;
		transform.LookAt(playerHeadPosition.transform);
		float distance = getDistanceFromPlayer();

		if (distance > attackDistanceThreshold + 10) {
			startSeek ();
			return;
		}

		if (nextAttackTime < 0) {
			shootIcebolt();
		}

		if (distance < escapeDistanceThreshold) {
			startEscape();
		}
	}

	void updateEscape() {
		Vector3 diff = escapePosition - transform.position;
		if (diff.magnitude < 1.0f) {
			startIdle();
		} else {
			GetComponent<Rigidbody>().velocity = diff.normalized * escapeSpeed;
			transform.LookAt(escapePosition);
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

	void startSeek() {
		state = EnemyState.Seek;
		escapePosition = getRandomPosition ();
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
		if (isOutField(escapePosition)) {
			escapePosition = getRandomPosition ();
		}
    }

    void startDeath() {
        state = EnemyState.Death;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        GetComponent<Rigidbody>().AddTorque(new Vector3(Random.value * 1000, Random.value * 1000, Random.value * 1000));
        Destroy(gameObject, 3.0f);
    }

	Vector3 getRandomPosition() {
		float x = Random.Range (fieldMin.x, fieldMax.x);
		float y = Random.Range (fieldMin.y, fieldMax.y);
		float z = Random.Range (fieldMin.z, fieldMax.z);
		return new Vector3 (x, y, z);
	}

	bool isOutField(Vector3 point) {
		if (point.x < fieldMin.x || point.x > fieldMax.x
		    || point.y < fieldMin.y || point.y > fieldMax.y
		    || point.z < fieldMin.z || point.z > fieldMax.z) {
			return true;
		}
		return false;
	}
}
