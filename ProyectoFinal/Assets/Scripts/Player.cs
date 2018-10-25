using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{

    //2.197    -3.5
    public int playerHealth = 5;
    public int playerShield = 0;
    public int speedX = 6;
    public int speedY = 4;
    Vector2 limiteY = new Vector2(-3.5f, 2.94f);

    private Rigidbody2D rigidbodyComponent;
    SpriteRenderer spriteComponent;
    SpriteRenderer spriteComponentPoint;

    public bool hasWeapon = false;
    public float fireRate = 0.25f;
    private double shootCooldown;

    public GameObject PlayerBullet;
    public GameObject PlayerCenter;

    Animator anim;
    public GameObject rightPunch;
    public GameObject leftPunch;



    // Use this for initialization
    void Start()
    {
        spriteComponent = GetComponent<SpriteRenderer>();


        if (rigidbodyComponent == null)
            rigidbodyComponent = GetComponent<Rigidbody2D>();

        shootCooldown = 0;

        anim = GetComponent<Animator>();

        rightPunch.SetActive(false);
        leftPunch.SetActive(false);
    }

    // Update is called once per frame
    Vector2 cntrl;
    void Update()
    {

        float inputX = Input.GetAxis("Horizontal") * speedX;
        float inputY = Input.GetAxis("Vertical") * speedY;

        if (inputX != 0)
        {
            if (inputX < 0)
            {
                spriteComponent.flipX = inputX < 0;
                var rotation = PlayerCenter.transform.rotation;
                rotation.y = 180;
                PlayerCenter.transform.rotation = rotation;
                
            }
            else
            {
                spriteComponent.flipX = false;
                var rotation = PlayerCenter.transform.rotation;
                rotation.y = 0;
                PlayerCenter.transform.rotation = rotation;
            }
            
        }

        rigidbodyComponent.velocity = new Vector2(inputX, inputY);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, limiteY.x, limiteY.y), transform.position.z);

        cntrl = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        anim.SetBool("IsWalking", cntrl.magnitude != 0);

        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }

        bool attack = Input.GetKeyDown(KeyCode.Space);

        if (attack & CanAttack == true)
        {
            //Si tiene arma, dispara
            if (hasWeapon == true)
            {
                shootCooldown = fireRate;
                Instantiate(PlayerBullet, PlayerCenter.transform.position, PlayerCenter.transform.rotation);
            }
            else
            {
                //Si no tiene arma dara un puñetazo
                shootCooldown = fireRate;   
                anim.SetTrigger("SendPunch");
                StartCoroutine("DoPunch");
                
            }

        }
        //FALTA CÓDIGO PARA QUE NO EJECUTE OTRA ANIMACIÓN IMENTRAS ESTÉ OTRA 16:00

        
    } //Update Ends

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
            anim.SetTrigger("ReceivePunch");
            
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

    IEnumerator DoPunch()
    {
        if (spriteComponent.flipX == false)
        {
            rightPunch.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            rightPunch.SetActive(false);
        }
        else
        {
            leftPunch.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            leftPunch.SetActive(false);
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