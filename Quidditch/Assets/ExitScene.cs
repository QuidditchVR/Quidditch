using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExitScene : MonoBehaviour {

    public static string menuSceneName = "Menu"; 

    void OnTriggerEnter(Collider c) {
        if (c.CompareTag("Player")) {
            SceneManager.LoadScene(menuSceneName);
            NextScene.currentLevel = 1;
        }
    }
}
