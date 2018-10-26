using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    //-184 86
    //Informacion Basica del Jugador
    public int playerHealth = 100;
    public int playerShield = 0;
    public int speedX = 6;
    public int speedY = 4;
    Vector2 limiteX = new Vector2(-184f, 86f); //Limite en Y del jugador 
    Vector2 limiteY = new Vector2(-3.5f, 2.81f); //Limite en Y del jugador estando en la planta baja
    Vector2 limiteEscalera = new Vector2(7.83f, 7.83f); //Limite en Y del jugador estando en la planta alta
    public bool GoThroughFloors; //Booleano para saber si esta adentro del collider de la escalera
    public bool OnUpperLevel; //Booleano para saber si esta en la planta alta
    public bool isDead = false;

    private Rigidbody2D rigidbodyComponent;
    SpriteRenderer spriteComponent;

    //Informacion Basica de sus armas
    public bool hasWeapon = false;
    public float fireRate = 0.15f;
    private double shootCooldown;

    //Variables externas - Armas
    public GameObject PlayerBullet;
    public GameObject PlayerCenter;
    public GameObject rightPunch;
    public GameObject leftPunch;

    //
    Animator anim;
    
    
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

        GoThroughFloors = false;
        OnUpperLevel = false;
    }

    // Update is called once per frame
    Vector2 cntrl;
    void Update()
    {
        if (playerShield > 0)
        {
            spriteComponent.GetComponent<SpriteRenderer>().color = Color.cyan;
        }
        else
        {
            spriteComponent.GetComponent<SpriteRenderer>().color = Color.white;
        }

        //Variables de movimiento
        float inputX = Input.GetAxis("Horizontal") * speedX;
        float inputY = Input.GetAxis("Vertical") * speedY;

        //Condicional para ver la direccion de la imagen del jugador
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

        //Condicional para, dependiendo del piso en el que este, los limites que lo contienen en Y
        if (OnUpperLevel == false)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, limiteX.x, limiteX.y), Mathf.Clamp(transform.position.y, limiteY.x, limiteY.y), transform.position.z);
        }
        else
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, limiteX.x, limiteX.y), Mathf.Clamp(transform.position.y, limiteEscalera.x, limiteEscalera.y), transform.position.z);
        }
        
        //Si se mueve el personaje, la animacion de movimiento se ejecutara
        cntrl = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        anim.SetBool("IsWalking", cntrl.magnitude != 0);

        //Validar si el personaje puede atacar
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }

        bool attack = Input.GetKeyDown(KeyCode.Space);

        //COndicional para ver si puede atacar una vez que se pico la barra espaciadora
        if (attack & CanAttack == true)
        {
            //Si tiene arma, dispara
            if (hasWeapon == true)
            {
                //anim.SetTrigger("ShootMain"); AUN NO ESTÁ LA ANIMACIÓN
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
        
        //Si esta adentro de una escalera y presiona Shift Derecho, se movera entre pisos
        if (Input.GetKeyDown(KeyCode.RightShift) && GoThroughFloors == true)
        {
            MoveThroughFloors();
        }

        //FALTA CÓDIGO PARA QUE NO EJECUTE OTRA ANIMACIÓN IMENTRAS ESTÉ OTRA 16:00


    } //Update Ends

    /*void OnGUI()
    {
        GUILayout.Label("Player Life: " + playerHealth);
        GUILayout.Label("Player Shield: " + playerShield);
    }*/
    

    //Metodo de perder vida y/o escudo
    public void ChangePlayerLife(int damage)
    {
        if (playerShield <= 0)
        {
            playerHealth = playerHealth - damage;
        }
        else if (damage > playerShield)
        {
            playerShield = 0;
        }
        else
        {
            playerShield = playerShield - damage;
        }
            
        
        if (damage > playerHealth && playerShield == 0)
        {
            playerHealth = 0;
        }
        
        if (playerHealth <= 0 && playerShield == 0)
        {
            isDead = true;
            SceneManager.LoadScene("Level1");
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Colision con bala de enemigo
        EnemyShoot enemyShot = collision.gameObject.GetComponent<EnemyShoot>();
        if (enemyShot != null)
        {
            ChangePlayerLife(enemyShot.damage);
            Destroy(enemyShot.gameObject);
            anim.SetTrigger("ReceivePunch");
            
        }

        //Colision con un arma
        Weapons weapon = collision.gameObject.GetComponent<Weapons>();
        if (weapon != null)
        {
            hasWeapon = true;
        }

        //Colision con medkit
        MedKit medkit = collision.gameObject.GetComponent<MedKit>();
        if (medkit != null)
        {
            MedKitFunction();
            Destroy(medkit.gameObject);
        }

        //Colision con escudo
        Shield shield = collision.gameObject.GetComponent<Shield>();
        if (shield != null)
        {
            ShieldFunction();
            Destroy(shield.gameObject);
        }





    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Colision con una escalera que permite cambiar de pisos
        Escalera escalera = collision.gameObject.GetComponent<Escalera>();
        if (escalera != null)
        {
            GoThroughFloors = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Si el jugador se aleja de una escalera, no podra moverse entre pisos
        Escalera escalera = collision.gameObject.GetComponent<Escalera>();
        if (escalera != null)
        {
            GoThroughFloors = false;

        }
    }

    public bool CanAttack
    {
        //Se checa si el personaje puede atacar con respecto a su fire rate
        get
        {
            return shootCooldown <= 0;
        }
    }

    IEnumerator DoPunch()
    {
        //Condicionales para validar la direccion del personaje y dar un puñetazo
        if (spriteComponent.flipX == false)
        {
            yield return new WaitForSeconds(0.2f);
            rightPunch.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            rightPunch.SetActive(false);
        }
        else
        {
            yield return new WaitForSeconds(0.2f);
            leftPunch.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            leftPunch.SetActive(false);
        }
    }

    public void MoveThroughFloors()
    {
        //Condicional para moverse de la planta baja a la alta y viceversa
        if (OnUpperLevel == false)
        {
            OnUpperLevel = true;
            transform.position = new Vector3(transform.position.x, 7.83f, transform.position.z);
        }
        else
        {
            OnUpperLevel = false;
            transform.position = new Vector3(transform.position.x, 2.94f, transform.position.z);
        }
    }

    //Items
    public void MedKitFunction()
    {
        //Metodo para tomar vida
        playerHealth = playerHealth + 25;
    }

    public void ShieldFunction()
    {
        //Metodo para tomar escudo
        playerShield = playerShield + 25;
    }


}