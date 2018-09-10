using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {
    
    public int damage = 1;
    public bool enemyShot = true;

    
    public Vector2 speed = new Vector2(-1, 0);
    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;
    /*
    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector2(speed.x, 0);
    }
    

    void FixedUpdate()
    {
        if (rigidbodyComponent == null)
            rigidbodyComponent = GetComponent<Rigidbody2D>();

        rigidbodyComponent.velocity = movement;
    }
    */

    void Update()
    {
        gameObject.transform.Translate(new Vector3(-0.1f, 0, 0));
    }


}
