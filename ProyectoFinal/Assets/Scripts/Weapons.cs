﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour {

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        
    }


    public void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Player playerPickUp = otherCollider.gameObject.GetComponent<Player>();
        if (playerPickUp != null)
        {
            Destroy(gameObject);
        }
    }
}
