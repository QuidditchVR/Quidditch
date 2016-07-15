using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour {

    public Text scoreText;
    public GameObject sceneRings;
    private List<Enemy> enemies;
    private float checkEnemyInterval = 0.5f;
    private int originalEnemyCount;
    private int enemyCount;

	// Use this for initialization
	void Start () {
        var enemyObjs = GameObject.FindGameObjectsWithTag("Enemy");
        enemies = new List<Enemy>();
        foreach(var obj in enemyObjs) {
            var enemy = obj.GetComponent<Enemy>();
            if (enemy) {
                enemies.Add(enemy);
            }
        }
        enemyCount = getAliveEnemyCount();
        originalEnemyCount = enemyCount;
        StartCoroutine("checkEnemyCount");
	}
	
	// Update is called once per frame
	void Update () {

	}

    IEnumerator checkEnemyCount() {
        while(true) {
            enemyCount = getAliveEnemyCount();
            if (scoreText) {
                scoreText.text = enemyCount + "/" + originalEnemyCount;
            }
            if (enemyCount == 0) {
                sceneRings.SetActive(true);
            }
            yield return new WaitForSeconds(checkEnemyInterval);
        }
    }

    private int getAliveEnemyCount() {
        int count = 0;
        foreach (var enemy in enemies) {
            if (enemy) {
                count++;
            }
        }
        return count;
    }
}
