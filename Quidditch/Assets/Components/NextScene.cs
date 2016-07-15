using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour {

    public static string[] sceneNames = {
        "Level 3",
    };
    public static int currentScene = 0;
 
    void OnTriggerEnter(Collider c) {
        if (c.CompareTag("Player")) {
            currentScene++;
            if (currentScene >= sceneNames.Length) {
                currentScene = 0;
            }
            SceneManager.LoadScene(sceneNames[currentScene]);
        }
    }
}
