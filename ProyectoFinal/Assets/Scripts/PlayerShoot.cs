using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {
    public int damage = 10;
    Rigidbody2D bulletRB;

    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        bulletRB.velocity = transform.right * 8f;
        Destroy(gameObject, 0.8f);
    }

    void Update()
    {
        
    }
    
    
}
