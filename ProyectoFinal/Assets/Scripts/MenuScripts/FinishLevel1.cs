using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishLevel1 : MonoBehaviour {
    public GameObject End;
    public Text text;

    GameObject MainPlayer;
    Player Gary;

    // Use this for initialization
    void Start () {
        MainPlayer = GameObject.FindGameObjectWithTag("Player");
        Gary = MainPlayer.GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update () {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Colision con box collider para terminar nivel 
        Player playerCol = collision.gameObject.GetComponent<Player>();
        if (playerCol != null)
        {
            End.SetActive(true);
            text.text = "Puntaje: " + Gary.playerScore;
        }
        
    }
}
