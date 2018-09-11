using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public int playerHealth = 5;
    public int playerShield = 0;
    public int speed = 10;
    //Vector2 forceSpeed = new Vector2(0, 5);
    private Rigidbody2D rigidbodyComponent;

    public bool hasWeapon = false;

    public float fireRate = 0.25f;
    private double shootCooldown;

    public GameObject PlayerBullet;
    public GameObject PlayerCenter;


    // Use this for initialization
    void Start () {
        if (rigidbodyComponent == null)
            rigidbodyComponent = GetComponent<Rigidbody2D>();

        shootCooldown = 0;
    }
	
	// Update is called once per frame
	void Update () {
         
        float inputX = Input.GetAxis("Horizontal") * speed;
        float inputY = Input.GetAxis("Vertical") * speed;
        inputX *= Time.deltaTime;
        inputY *= Time.deltaTime;


        transform.Translate(0, inputY, 0);
        transform.Translate(inputX, 0, 0);

        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbodyComponent.AddForce(forceSpeed);
        }*/


        bool shoot = Input.GetKeyDown(KeyCode.LeftShift);
  
        if (shoot & CanAttack==true)
        {
            if (hasWeapon == true)
            {
                shootCooldown = fireRate;
                Instantiate(PlayerBullet, PlayerCenter.transform.position, Quaternion.identity);
            }
            

           /* Weapons weapon = GetComponentInChildren<Weapons>();
            if (weapon != null)
            {
                weapon.Attack(false);
            }*/

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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyShoot enemyShot = collision.gameObject.GetComponent<EnemyShoot>();
        if (enemyShot != null)
        {
            ChangePlayerLife(enemyShot.damage);
            Destroy(enemyShot.gameObject);
        }

        Weapons weapon = collision.gameObject.GetComponent<Weapons>();
        if (weapon != null)
        {
            hasWeapon = true;
        }
    }

    public bool CanAttack
    {
        get
        {
            return shootCooldown <= 0;
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
