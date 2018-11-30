using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour {

    public int enemyHealth = 50;
    public float speedX = 2.3f;
    public float speedY = 1.7f;
    public Vector2 limiteY = new Vector2(-2.9f, 2.197f);
    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;
    private SpriteRenderer enemyRen;

    private bool dirRight = true;
    bool Patrol;
    public float patrollingSpeed = 2.0f;
    public float enemyPosx;

    public bool canShoot = false;
    public GameObject EnemyBullet;
    public GameObject EnemyCenter;
    //public GameObject EnemyShootingPoint;

    GameObject PScore;
    PlayerScore Score;
    GameObject MainPlayer;
    Player Gary;
    Transform player;
    Vector3 target;

    Animator anim;


    // Use this for initialization
    void Start()
    {
        enemyRen = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (rigidbodyComponent == null)
            rigidbodyComponent = GetComponent<Rigidbody2D>();

        if (canShoot == true)
        {
            InvokeRepeating("EnemyShotAttack", 0, 2f);
        }

        enemyPosx = transform.position.x;
        dirRight = false;
        Patrol = true;

        MainPlayer = GameObject.FindGameObjectWithTag("Player");
        Gary = MainPlayer.GetComponent<Player>();

        PScore = GameObject.FindGameObjectWithTag("Score");
        Score = PScore.GetComponent<PlayerScore>();
    }

    Vector2 cntrl;
    // Update is called once per frame
    void Update()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("FireCop") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("DamageCop"))
        {
            anim.SetBool("WalkCop", true);

            if (Patrol == true)
            {

                if (dirRight)
                {
                    rigidbodyComponent.velocity = new Vector2(speedX, 0);
                    transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, limiteY.x, limiteY.y), 0);

                }
                else
                {
                    rigidbodyComponent.velocity = new Vector2(-speedX, 0);
                    transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, limiteY.x, limiteY.y), 0);
                }

                if (transform.position.x >= enemyPosx)
                {
                    dirRight = false;
                    var rotation = transform.rotation;
                    rotation.y = 0;
                    transform.rotation = rotation;
                    EnemyCenter.transform.rotation = rotation;

                }

                if (transform.position.x <= (enemyPosx - 4))
                {
                    dirRight = true;
                    var rotation = transform.rotation;
                    rotation.y = 180;
                    transform.rotation = rotation;
                    EnemyCenter.transform.rotation = rotation;

                }
            }
            else
            {
                movement = target - transform.position;

                if (movement.x > 0)
                {
                    var rotation = transform.rotation;
                    rotation.y = 180;
                    transform.rotation = rotation;
                    EnemyCenter.transform.rotation = rotation;
                    enemyRen.flipX = movement.x < 0;

                }
                else
                {
                    var rotation = transform.rotation;
                    rotation.y = 0;
                    transform.rotation = rotation;
                    EnemyCenter.transform.rotation = rotation;
                    enemyRen.flipX = movement.x > 0;
                }

                if (movement.magnitude < 1.3f)
                {
                    movement = Vector2.zero;
                }

                movement.Normalize();
                rigidbodyComponent.velocity = new Vector2(movement.x * speedX, movement.y * speedY);
                transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, limiteY.x, limiteY.y), 0);
                
            }
        }
        else
        {
            rigidbodyComponent.velocity = Vector3.zero;
        }
        
           
    }
    

    public void ChangeEnemyLife(int damage)
    {
        enemyHealth = enemyHealth - damage;
        anim.SetTrigger("ReceiveDamage");

        if (enemyHealth == 0)
        {
            Gary.UpdateScore(10);
            Score.ChangeScore();
            Destroy(gameObject);
            
        }
            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerShoot playerShot = collision.gameObject.GetComponent<PlayerShoot>();
        if (playerShot != null)
        {
            ChangeEnemyLife(playerShot.damage);
            Destroy(playerShot.gameObject);

        }

        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            canShoot = true;
            InvokeRepeating("EnemyShotAttack", 0, 1f);
        }

        PlayerPunch punch = collision.gameObject.GetComponent<PlayerPunch>();
        if (punch != null)
        {
            ChangeEnemyLife(punch.damage);
            
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            Patrol = false;
            target = player.transform.position;
        }
    }

    //Deja de seguir al jugador en cuanto sale del FOV
    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            Patrol = true;
            
            CancelInvoke("EnemyShotAttack");
            enemyPosx = transform.position.x;
            target = transform.position;
            rigidbodyComponent.velocity = new Vector2(0, 0);
        }
    }

    void EnemyShotAttack()
    {
        anim.SetTrigger("CopShot");
        Instantiate(EnemyBullet, EnemyCenter.transform.position, EnemyCenter.transform.rotation);
    }
}
