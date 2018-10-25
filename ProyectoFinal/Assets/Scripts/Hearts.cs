using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour {

    Animator animH;
    public Player player;

	// Use this for initialization
	void Start () {
        animH = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        //Animación corazones
        if (player.playerHealth <= 5)
        {
            animH.SetTrigger("HeartDecrease");
        }
        else if (player.playerHealth <= 1)
        {
            animH.SetTrigger("DeadHeart");
        }
    }
}
