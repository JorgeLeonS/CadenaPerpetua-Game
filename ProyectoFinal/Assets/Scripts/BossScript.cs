using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    public GameObject End;
    public Text text;

    public int enemyHealth = 150;
    public float speedX = 2.3f;
    public float speedY = 1.7f;
    public Vector2 limiteY = new Vector2(-3.5f, 2.197f);
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
    public Spawner Spawn1; 
    public Spawner Spawn2;
    public Spawner Spawn3;
    public GameObject BossDog;

    GameObject PScore;
    PlayerScore Score;
    GameObject MainPlayer;
    Player Gary;
    Transform player;
    Vector3 target;

    Animator anim;
    AudioSource source;
    public AudioClip Dogs;

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
        enemyRen = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (rigidbodyComponent == null)
            rigidbodyComponent = GetComponent<Rigidbody2D>();
        
        enemyPosx = transform.position.x;
        dirRight = false;
        Patrol = true;

        MainPlayer = GameObject.FindGameObjectWithTag("Player");
        Gary = MainPlayer.GetComponent<Player>();

        PScore = GameObject.FindGameObjectWithTag("Score");
        Score = PScore.GetComponent<PlayerScore>();
        
        InvokeRepeating("SpawnDogs", 0, 1.5f);
    }

    Vector2 cntrl;
    // Update is called once per frame
    void Update()
    {
        if (Gary.isDead == false)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("BossAttack") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("BossDamage"))
            {
                anim.SetBool("BossIdle", true);
                
            }
            else
            {
                rigidbodyComponent.velocity = Vector3.zero;
            }

        }
        else
        {
            rigidbodyComponent.velocity = Vector3.zero;
        }

      
            
        

    }

    public void SpawnDogs()
    {
        anim.SetTrigger("BossAttack");
        source.PlayOneShot(Dogs);
        Instantiate(BossDog, Spawn1.transform.position, Spawn1.transform.rotation);
        Instantiate(BossDog, Spawn2.transform.position, Spawn2.transform.rotation);
        Instantiate(BossDog, Spawn3.transform.position, Spawn3.transform.rotation);

    }


    public void ChangeEnemyLife(int damage)
    {
        enemyHealth = enemyHealth - damage;
        anim.SetTrigger("BossDamage");

        if (enemyHealth <= 0)
        {
            Gary.UpdateScore(1000);
            for(int i = 0; i < 100; i++){
                Score.ChangeScore();
            }
            End.SetActive(true);
            text.text = "Puntaje: " + Gary.playerScore;
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



}
