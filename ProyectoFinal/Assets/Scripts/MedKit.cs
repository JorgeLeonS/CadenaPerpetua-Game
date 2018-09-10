using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour {


    public Player Player;
    // Use this for initialization
    void Start() {

    }

    //Place script on MedKit (already prefab), drag player to medkit script (inspector)
    public void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Player playerPickUp = otherCollider.gameObject.GetComponent<Player>();
        if (playerPickUp != null)
        {
            Player.MedKitFunction();
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update () {
		
	}
}
