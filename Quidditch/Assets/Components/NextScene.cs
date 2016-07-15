using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour {

    public string[] sceneNames = {
        "Level 1",
        "Level 2",
        "Level 3",
    };
 
    void OnTriggerEnter(Collider c) {
        if (c.CompareTag("Player")) {
            int currentLevel = 0;
            foreach (var name in sceneNames) {
                if (SceneManager.GetActiveScene().name == name) {
                    break;
                } else {
                    currentLevel++;
                }
            }

            currentLevel++;
            if (currentLevel > sceneNames.Length) {
                currentLevel = 0;
            }
            SceneManager.LoadScene(sceneNames[currentLevel]);
        }
    }
}
