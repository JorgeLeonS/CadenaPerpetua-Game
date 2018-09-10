using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public int playerHealth = 5;
    public int playerShield = 0;
    public int speed = 10;
    Vector2 forceSpeed = new Vector2(0, 5);
    private Rigidbody2D rigidbodyComponent;
    


    // Use this for initialization
    void Start () {
        if (rigidbodyComponent == null)
            rigidbodyComponent = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
         
        float inputX = Input.GetAxis("Horizontal") * speed;
        float inputY = Input.GetAxis("Vertical") * speed;
        inputX *= Time.deltaTime;
        inputY *= Time.deltaTime;


        transform.Translate(0, inputY, 0);
        transform.Translate(inputX, 0, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbodyComponent.AddForce(forceSpeed);
        }
        

       bool shoot = Input.GetKeyDown(KeyCode.Space);
  
        if (shoot)
        {
            Weapons weapon = GetComponentInChildren<Weapons>();
            if (weapon != null)
            {
                weapon.Attack(false);
            }

        }
    }


    void OnGUI()
    {
        GUILayout.Label("Player Life: " + playerHealth);
        GUILayout.Label("Player Shield: " + playerShield);
    }

    void FixedUpdate()
    {
        
    }

    public void ChangePlayerLife(int damage)
    {
        if (playerShield == 0)
        {
            playerHealth = playerHealth - damage;
        }
        else
        {
            playerShield = playerShield - damage;
        }
        if (playerHealth == 0)
        {
            Destroy(gameObject);
        }
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

    //Items
    public void MedKitFunction()
    {
        playerHealth = playerHealth + 5;
    }

    public void ShieldFunction()
    {
        playerShield = playerShield + 10;
    }

}
