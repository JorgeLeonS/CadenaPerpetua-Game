using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour {

    public int damage = 10;
    Rigidbody2D dogRB;

    // Use this for initialization
    void Start()
    {
        dogRB = GetComponent<Rigidbody2D>();
        dogRB.velocity = transform.right * -10f;
        Destroy(gameObject, 4);
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerShoot playerShot = collision.gameObject.GetComponent<PlayerShoot>();
        if (playerShot != null)
        {
            Destroy(playerShot.gameObject);
            Destroy(gameObject);
        }
        

        PlayerPunch punch = collision.gameObject.GetComponent<PlayerPunch>();
        if (punch != null)
        {
            Destroy(gameObject);
        }
    }
}
