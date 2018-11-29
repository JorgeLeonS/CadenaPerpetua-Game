using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterBossLevel : MonoBehaviour {

    public void LoadBossLevel(string BossLevel)
    {
        SceneManager.LoadScene("BossLevel");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
