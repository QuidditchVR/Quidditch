using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayTutorial()
    {
        //play yuyorial
        SceneManager.LoadScene("Level 1");

    }
    public void PlayTutorial3()
    {
        //play yuyorial
        SceneManager.LoadScene("Level 2");

    }
    public void PlayTutorial2()
    {
        //play yuyorial
        SceneManager.LoadScene("Level 3");

    }
}
