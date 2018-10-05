using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour {

    public int enemyHealth = 1;
    public float speedX = 2.3f;
    public float speedY = 1.7f;
    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;
    private SpriteRenderer enemyRen;

    public bool canShoot = false;
    public GameObject EnemyBullet;
    public GameObject EnemyCenter;
    //public GameObject EnemyShootingPoint;

    Transform player;
    Vector3 target;


    // Use this for initialization
    void Start()
    {
        enemyRen = GetComponent<SpriteRenderer>();

        if (rigidbodyComponent == null)
            rigidbodyComponent = GetComponent<Rigidbody2D>();

        if (canShoot == true)
        {
            InvokeRepeating("EnemyShotAttack", 0, 2f);
        }


    }

    // Update is called once per frame
    void Update()
    {
        //rigidbodyComponent.velocity = new Vector2(speedX, speedY);
        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    

    public void ChangeEnemyLife(int damage)
    {
        enemyHealth = enemyHealth - damage;

        if (enemyHealth == 0)
            Destroy(gameObject);
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
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            target = player.transform.position;
            movement = target - transform.position;
            
            if (movement.x > 0)
            {
                var rotation = transform.rotation;
                rotation.y = 180;
                transform.rotation = rotation;
                //EnemyShootingPoint.transform.rotation = rotation;
                enemyRen.flipX = movement.x < 0;

            }
            else
            {
                var rotation = transform.rotation;
                rotation.y = 0;
                transform.rotation = rotation;
                //EnemyShootingPoint.transform.rotation = rotation;
                enemyRen.flipX = movement.x > 0;
            }
            
            if (movement.magnitude < 0.7f)
                movement = Vector2.zero;
            movement.Normalize();
            rigidbodyComponent.velocity = new Vector2(movement.x * speedX, movement.y * speedY);
            
        }
    }

    //Deja de seguir al enemigo en cuanto sale del FOV
    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            target = transform.position;
            rigidbodyComponent.velocity = new Vector2(0, 0);
            CancelInvoke();
        }
    }

    void EnemyShotAttack()
    {
        Instantiate(EnemyBullet, EnemyCenter.transform.position, Quaternion.identity);
    }
}
