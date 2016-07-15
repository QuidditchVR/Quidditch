using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour {

    public static string[] sceneNames = {
        "Level 1",
        "Level 2",
        "Level 3",
    };
    public static int currentLevel = 1;
 
    void OnTriggerEnter(Collider c) {
        if (c.CompareTag("Player")) {
            currentLevel++;
            if (currentLevel > sceneNames.Length) {
                currentLevel = 1;
            }
            SceneManager.LoadScene(sceneNames[currentLevel - 1]);
        }
    }
}
