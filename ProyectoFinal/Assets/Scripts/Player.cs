using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //2.197    -3.5
    public int playerHealth = 5;
    public int playerShield = 0;
    public int speedX = 6;
    public int speedY = 4;
    Vector2 limiteY = new Vector2(-3.5f, 2.197f);

    private Rigidbody2D rigidbodyComponent;
    SpriteRenderer spriteComponent;
    SpriteRenderer spriteComponentPoint;

    public bool hasWeapon = false;
    public float fireRate = 0.25f;
    private double shootCooldown;

    public GameObject PlayerBullet;
    public GameObject PlayerCenter;


    // Use this for initialization
    void Start () {
        spriteComponent = GetComponent<SpriteRenderer>();


        if (rigidbodyComponent == null)
            rigidbodyComponent = GetComponent<Rigidbody2D>();

        shootCooldown = 0;
    }
	
	// Update is called once per frame
	void Update () {
         
        float inputX = Input.GetAxis("Horizontal") * speedX;
        float inputY = Input.GetAxis("Vertical") * speedY;
        
        if (inputX != 0)
        {
            spriteComponent.flipX = inputX < 0;
        }

        rigidbodyComponent.velocity = new Vector2(inputX, inputY);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, limiteY.x, limiteY.y), transform.position.z);
        

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

        //Punch animation CREATE ANIMATION VARIABLE, IF(!ANIM... ="PUNCHPROT" THEN...


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
