using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchingEnemy : MonoBehaviour
{
    public int enemyHealth = 10;
    public float speedX = 2.3f;
    public float speedY = 1.7f;
    public Vector2 limiteY = new Vector2(-3.5f, 2.197f);
    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;
    private SpriteRenderer enemyRen;
    public bool canAttack;
    public GameObject rightPunch;
    public GameObject leftPunch;
    private float fireRate = 1f;
    private double shootCooldown;

    private bool dirRight = true;
    bool Patrol;
    public float patrollingSpeed = 2.0f;
    public float enemyPosx;
    

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

        

        enemyPosx = transform.position.x;
        dirRight = false;
        Patrol = true;
        canAttack = false;

        MainPlayer = GameObject.FindGameObjectWithTag("Player");
        Gary = MainPlayer.GetComponent<Player>();

        rightPunch.SetActive(false);
        leftPunch.SetActive(false);

        PScore = GameObject.FindGameObjectWithTag("Score");
        Score = PScore.GetComponent<PlayerScore>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Gary.isDead == false)
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

                    }

                    if (transform.position.x <= (enemyPosx - 4))
                    {
                        dirRight = true;
                        var rotation = transform.rotation;
                        rotation.y = 180;
                        transform.rotation = rotation;

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
                        enemyRen.flipX = movement.x < 0;

                    }
                    else
                    {
                        var rotation = transform.rotation;
                        rotation.y = 0;
                        transform.rotation = rotation;
                        enemyRen.flipX = movement.x > 0;
                    }

                    if (movement.magnitude < 1.5f)
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
                rigidbodyComponent.velocity = Vector2.zero;
            }

            if (shootCooldown > 0)
            {
                shootCooldown -= Time.deltaTime;
            }


            if (Vector3.Distance(target, transform.position) < 2f)
            {
                if (CanAttack == true)
                {
                    shootCooldown = fireRate;
                    EnemyMeleeAttack();
                }

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
            enemyPosx = transform.position.x;
            target = transform.position;
            rigidbodyComponent.velocity = new Vector2(0, 0);
        }
    }
    

    public void EnemyMeleeAttack()
    {
        anim.SetTrigger("CopShot");
        rigidbodyComponent.velocity = Vector3.zero;
        StartCoroutine("DoMelee");
    }

    protected IEnumerator DoMelee()
    {
        
        //Condicionales para validar la direccion del personaje y dar un puñetazo
        if (enemyRen.flipX == true)
        {
            rightPunch.SetActive(true);
            yield return new WaitForSeconds(0.05f);
            rightPunch.SetActive(false);
            
        }
        else
        {
            leftPunch.SetActive(true);
            yield return new WaitForSeconds(0.05f);
            leftPunch.SetActive(false);

        }
    }

    public bool CanAttack
    {
        get
        {
            return shootCooldown <= 0;
        }
    }
}














