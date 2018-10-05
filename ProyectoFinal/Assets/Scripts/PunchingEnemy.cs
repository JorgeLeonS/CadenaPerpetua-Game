using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchingEnemy : MonoBehaviour
{

    public int enemyHealth = 1;
    public float speedX = 2.3f;
    public float speedY = 1.7f;
    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;
    private SpriteRenderer enemyRen;


    public Transform LeftPunch;
    public Transform RightPunch;
    Transform player;
    Vector3 target;


    // Use this for initialization
    void Start()
    {
        enemyRen = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (rigidbodyComponent == null)
            rigidbodyComponent = GetComponent<Rigidbody2D>();





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
    }

    // Update is called once per frame
    void Update()
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
        

       





        // Player player = collision.gameObject.GetComponent<Player>();

    
    /*
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

            if (movement.magnitude < 0.3f)
                movement = Vector2.zero;
            movement.Normalize();
            rigidbodyComponent.velocity = new Vector2(movement.x * speedX, movement.y * speedY);

        }
    }
    */


  


