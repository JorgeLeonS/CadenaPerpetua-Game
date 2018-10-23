using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {
    
    public int damage = 1;
    Rigidbody2D bulletRB;
    
    // Use this for initialization
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        bulletRB.velocity = transform.right * -10f;
        Destroy(gameObject, 2);
    }
    
    void Update()
    {
    }


}
