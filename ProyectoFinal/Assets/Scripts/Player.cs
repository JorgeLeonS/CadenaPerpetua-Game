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
    public int playerScore = 0;
    public int speedX = 6;
    public int speedY = 4;
    public Vector2 limiteX = new Vector2(-184f, 95f); //Limite en Y del jugador 
    Vector2 limiteY = new Vector2(-2.9f, 2.81f); //Limite en Y del jugador estando en la planta baja
    Vector2 limiteEscalera = new Vector2(8.28f, 8.28f); //Limite en Y del jugador estando en la planta alta
    public bool GoThroughFloors; //Booleano para saber si esta adentro del collider de la escalera
    public bool OnUpperLevel; //Booleano para saber si esta en la planta alta
    public bool isDead = false;

    private Rigidbody2D rigidbodyComponent;
    SpriteRenderer spriteComponent;

    //Informacion Basica de sus armas
    protected float fireRate = 0.40f;
    private double shootCooldown;

    //Variables externas - Armas
    public GameObject PlayerBullet;
    public GameObject PlayerCenter;
    public GameObject rightPunch;
    public GameObject leftPunch;
    public string CurrentWeapon;
    public bool GunUnlocked;
    public bool MacanaUnlocked;
    public bool KnifeUnlocked;

    public GameObject PistolIndicator;
    public GameObject KnifeIndicator;
    public GameObject MacanaIndicator;
    public GameObject ShieldIndicator;
    public GameObject StairIndicator;

    public GameObject GameOverCanvas;

    //
    Animator anim;
    AudioSource source;
    public AudioClip PunchSound;
    public AudioClip ShotSound;

    

    // Use this for initialization
    void Start()
    {

        source = GetComponent<AudioSource>();

    GameObject.FindGameObjectWithTag("Music").GetComponent<KeepPlaying>().StopMusic();
        spriteComponent = GetComponent<SpriteRenderer>();


        if (rigidbodyComponent == null)
            rigidbodyComponent = GetComponent<Rigidbody2D>();

        shootCooldown = 0;

        anim = GetComponent<Animator>();

        rightPunch.SetActive(false);
        leftPunch.SetActive(false);
        PistolIndicator.SetActive(false);
        KnifeIndicator.SetActive(false);
        PistolIndicator.SetActive(false);
        ShieldIndicator.SetActive(false);
        StairIndicator.SetActive(false);


        GoThroughFloors = false;
        OnUpperLevel = false;

        CurrentWeapon = "Punch";
        MacanaUnlocked = false;
        KnifeUnlocked = false;
        GunUnlocked = false;

        /*playerHealth = GlobalControl.Instance.playerHealth;
        playerShield = GlobalControl.Instance.playerShield;
        playerScore = GlobalControl.Instance.playerScore;
        limiteX = GlobalControl.Instance.limiteX;
        CurrentWeapon = GlobalControl.Instance.CurrentWeapon;
        GunUnlocked = GlobalControl.Instance.GunUnlocked;
        MacanaUnlocked = GlobalControl.Instance.MacanaUnlocked;
        KnifeUnlocked = GlobalControl.Instance.KnifeUnlocked;*/
    }

    // Update is called once per frame
    void Update()
    {
        if (playerShield > 0)
        {
            spriteComponent.GetComponent<SpriteRenderer>().color = Color.cyan;
            ShieldIndicator.SetActive(true);
        }
        else
        {
            spriteComponent.GetComponent<SpriteRenderer>().color = Color.white;
            ShieldIndicator.SetActive(false);
        }

        //Variables de movimiento
        float inputX = Input.GetAxis("Horizontal") * speedX;
        float inputY = Input.GetAxis("Vertical") * speedY;

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("DamageMain") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("PunchMain") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("KnifeAttackMain") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("MacanaAttackMain") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("ShootMain") && 
            (inputX !=0 | inputY !=0))
        {
            
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
            anim.SetBool("IsWalking", true);

        }
        else
        {
            rigidbodyComponent.velocity = Vector3.zero;
            anim.SetBool("IsWalking", false);

        }

        //Condicional para, dependiendo del piso en el que este, los limites que lo contienen en Y
        if (OnUpperLevel == false)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, limiteX.x, limiteX.y), Mathf.Clamp(transform.position.y, limiteY.x, limiteY.y), transform.position.z);
        }
        else
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, limiteX.x, limiteX.y), Mathf.Clamp(transform.position.y, limiteEscalera.x, limiteEscalera.y), transform.position.z);
        }


        //Validar si el personaje puede atacar
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }

        bool attack = Input.GetKeyDown(KeyCode.Space);

        //Condicional para ver si puede atacar una vez que se pico la barra espaciadora
        if (attack & CanAttack == true)
        {

            //Si su arma actual son los puños, ataca
            if (CurrentWeapon == "Punch")
            {
                
                shootCooldown = fireRate;
                anim.SetTrigger("SendPunch");
                StartCoroutine("DoMelee");
                source.PlayOneShot(PunchSound, 1F);
            }

            //Si su arma actual es la pistola, ataca
            if (CurrentWeapon == "Pistol")
            {
                anim.SetTrigger("ShootMain");
                shootCooldown = fireRate;
                Instantiate(PlayerBullet, PlayerCenter.transform.position, PlayerCenter.transform.rotation);
                source.PlayOneShot(ShotSound);
            }

            //Si su arma actual es la macana, ataca
            if (CurrentWeapon == "Macana")
            {
                shootCooldown = fireRate;
                anim.SetTrigger("MacanaAttack");
                StartCoroutine("DoMelee");
                source.PlayOneShot(PunchSound, 1F);
            }

            //Si su arma actual es el cuchillo, ataca
            if (CurrentWeapon == "Knife")
            {
                shootCooldown = fireRate;
                anim.SetTrigger("KnifeAttack");
                StartCoroutine("DoMelee");
                source.PlayOneShot(PunchSound, 1F);
            }

        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
            CurrentWeapon = "Punch";

        if (Input.GetKeyDown(KeyCode.Alpha2) && MacanaUnlocked == true)
            CurrentWeapon = "Macana";

        if (Input.GetKeyDown(KeyCode.Alpha3) && KnifeUnlocked == true)
            CurrentWeapon = "Knife";

        if (Input.GetKeyDown(KeyCode.Alpha4) && GunUnlocked == true)
            CurrentWeapon = "Pistol";


        if (CurrentWeapon == "Punch")
        {
            fireRate = 0.32f;
            PistolIndicator.SetActive(false);
            KnifeIndicator.SetActive(false);
            MacanaIndicator.SetActive(false);
        }


        if (CurrentWeapon == "Pistol")
        {
            fireRate = 0.50f;
            PistolIndicator.SetActive(true);
            KnifeIndicator.SetActive(false);
            MacanaIndicator.SetActive(false);
        }


        if (CurrentWeapon == "Macana")
        {
            fireRate = 0.40f;
            PistolIndicator.SetActive(false);
            KnifeIndicator.SetActive(false);
            MacanaIndicator.SetActive(true);
        }

        if (CurrentWeapon == "Knife")
        {
            fireRate = 0.25f;
            PistolIndicator.SetActive(false);
            KnifeIndicator.SetActive(true);
            MacanaIndicator.SetActive(false);
        }


        //Protagonista en guardia

        anim.SetBool("ProtGuard", Input.GetKey(KeyCode.C));
        /* CHECK ENEMY DAMAGE  = 0
        if(Input.GetKey(KeyCode.C)&&)
        while(ProtGuard){
            EnemyDamage = 0;
        }*/

        //Si esta adentro de una escalera y presiona Shift Derecho, se movera entre pisos
        if (Input.GetKeyDown(KeyCode.RightShift) && GoThroughFloors == true)
        {
            MoveThroughFloors();
        }
        
    }

    public void SavePlayer()
    {
        GlobalControl.Instance.playerHealth = playerHealth;
        GlobalControl.Instance.playerShield = playerShield;
        GlobalControl.Instance.playerScore = playerScore;
        GlobalControl.Instance.limiteX = limiteX;
        GlobalControl.Instance.CurrentWeapon = CurrentWeapon;
        GlobalControl.Instance.GunUnlocked = GunUnlocked;
        GlobalControl.Instance.MacanaUnlocked = MacanaUnlocked;
        GlobalControl.Instance.KnifeUnlocked = KnifeUnlocked;
    }


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
            GameOverCanvas.SetActive(true);
        }
    }

    //Metodo para actualizar el puntaje del jugador
    public void UpdateScore(int score)
    {
        playerScore = playerScore + score;
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

        EnemyPunch punch = collision.gameObject.GetComponent<EnemyPunch>();
        if (punch != null)
        {
            ChangePlayerLife(punch.damage);
            anim.SetTrigger("ReceivePunch");

        }

        Dog dog = collision.gameObject.GetComponent<Dog>();
        if (dog != null)
        {
            ChangePlayerLife(dog.damage);
            Destroy(dog.gameObject);
            anim.SetTrigger("ReceivePunch");
        }

        //Colision con una pistola
        Pistol pistol = collision.gameObject.GetComponent<Pistol>();
        if (pistol != null)
        {
            CurrentWeapon = "Pistol";
            GunUnlocked = true;
            Destroy(pistol.gameObject);
        }

        //Colision con una macana
        Knife knife = collision.gameObject.GetComponent<Knife>();
        if (knife != null)
        {
            CurrentWeapon = "Knife";
            KnifeUnlocked = true;
            Destroy(knife.gameObject);
        }

        //Colision con un cuchillo
        Macana macana = collision.gameObject.GetComponent<Macana>();
        if (macana != null)
        {
            CurrentWeapon = "Macana";
            MacanaUnlocked = true;
            Destroy(macana.gameObject);
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
            StairIndicator.SetActive(true);
            GoThroughFloors = true;
        }

        EnemyPunch punch = collision.gameObject.GetComponent<EnemyPunch>();
        if (punch != null)
        {
            ChangePlayerLife(punch.damage);
            anim.SetTrigger("ReceivePunch");

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Si el jugador se aleja de una escalera, no podra moverse entre pisos
        Escalera escalera = collision.gameObject.GetComponent<Escalera>();
        if (escalera != null)
        {
            StairIndicator.SetActive(false);
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

    IEnumerator DoMelee()
    {
        //Condicionales para validar la direccion del personaje y dar un puñetazo
        if (spriteComponent.flipX == false)
        {
            yield return new WaitForSeconds(0.09f);
            rightPunch.SetActive(true);
            yield return new WaitForSeconds(0.06f);
            rightPunch.SetActive(false);

        }
        else
        {
            yield return new WaitForSeconds(0.09f);
            leftPunch.SetActive(true);
            yield return new WaitForSeconds(0.06f);
            leftPunch.SetActive(false);

        }
    }
  

    //Metodo para subir las escaleras
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