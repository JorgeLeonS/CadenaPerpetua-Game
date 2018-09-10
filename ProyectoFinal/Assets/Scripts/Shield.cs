using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

    public Player Player;

	// Use this for initialization
	void Start () {
		
	}
    //Place script on Shield (already prefab), drag player to shield script (inspector)
    public void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Player playerPickUp = otherCollider.gameObject.GetComponent<Player>();
        if (playerPickUp != null)
        {
            Player.ShieldFunction();
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
