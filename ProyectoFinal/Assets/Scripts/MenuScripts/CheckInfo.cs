using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckInfo : MonoBehaviour {

    public void SeeInfo(string Info)
    {
        SceneManager.LoadScene("Info");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
