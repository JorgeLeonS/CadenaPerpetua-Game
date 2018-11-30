using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour {

    public int damage = 20;
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
}
