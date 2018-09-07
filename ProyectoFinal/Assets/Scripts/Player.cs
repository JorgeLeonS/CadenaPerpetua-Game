using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public int playerHealth = 5;
    public Vector2 speed = new Vector2(5, 0);
    private Rigidbody2D rigidbodyComponent;
    Vector2 movementX = new Vector2(100, 0);
    Vector2 movementXN = new Vector2(-100, 0);
    


    // Use this for initialization
    void Start () {
        if (rigidbodyComponent == null)
            rigidbodyComponent = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rigidbodyComponent.transform.Translate(1, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rigidbodyComponent.transform.Translate(-1, 0, 0);
        }

        /*float inputX = Input.GetAxis("Horizontal");

        movement = new Vector2(speed.x + inputX, 0);*/

        bool shoot = Input.GetKeyDown(KeyCode.Space);
  
        if (shoot)
        {
            Weapons weapon = GetComponentInChildren<Weapons>();
            if (weapon != null)
            {
                // false because the player is not an enemy
                weapon.Attack(false);
            }
        }
    }

    void FixedUpdate()
    {
        
    }

    public void ChangePlayerLife(int damage)
    {
        playerHealth = playerHealth - damage;

        if (playerHealth == 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        EnemyShoot enemyShot = otherCollider.gameObject.GetComponent<EnemyShoot>();
        if (enemyShot != null)
        {
            ChangePlayerLife(enemyShot.damage);
            Destroy(enemyShot.gameObject);
        }
    }
}
