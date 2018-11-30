using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckStory : MonoBehaviour {

    public void SeeStory(string Story)
    {
        SceneManager.LoadScene("Story");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
