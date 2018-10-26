using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Colision con box collider para terminar nivel 
        Player playerCol = collision.gameObject.GetComponent<Player>();
        SceneManager.LoadScene("Level1Complete");
    }
}
