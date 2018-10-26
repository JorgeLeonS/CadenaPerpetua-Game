using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {
    
    public int damage = 10;
    Rigidbody2D bulletRB;
    
    // Use this for initialization
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        bulletRB.velocity = transform.right * -8f;
        Destroy(gameObject, 1);
    }
    
    void Update()
    {
    }


}
