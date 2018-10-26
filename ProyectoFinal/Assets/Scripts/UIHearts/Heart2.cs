using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart2 : MonoBehaviour {

    Animator animH;
    public Player player;

    // Use this for initialization
    void Start()
    {
        animH = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Animación corazones
        if (player.playerHealth <= 70)
        {
            animH.SetBool("FullHeart", false);
            animH.SetBool("HalfHeart", true);
            animH.SetBool("DeadHeart", false);
        }
        else if (player.playerHealth <= 60)
        {
            animH.SetBool("FullHeart", false);
            animH.SetBool("HalfHeart", false);
            animH.SetBool("DeadHeart", true);
        }
        else if(player.playerHealth > 70)
        {
            animH.SetBool("FullHeart", true);
            animH.SetBool("HalfHeart", false);
            animH.SetBool("DeadHeart", false);
        }
    }
}
