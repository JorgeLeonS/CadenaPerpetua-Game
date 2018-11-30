using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {
    public int damage = 30;
    Rigidbody2D bulletRB;

    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        bulletRB.velocity = transform.right * 12f;
        Destroy(gameObject, 1f);
    }

    void Update()
    {
        
    }
    
    
}
