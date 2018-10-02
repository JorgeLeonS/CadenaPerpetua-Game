using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour {

    public int enemyHealth = 1;
    public Vector2 speed = new Vector2(-1, 0);
    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;

    public bool canShoot = false;
    public GameObject EnemyBullet;
    public GameObject EnemyCenter;


    // Use this for initialization
    void Start()
    {
        if (canShoot == true)
        {
            InvokeRepeating("EnemyShotAttack", 0, 2f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector2(speed.x, 0);

    }

    void FixedUpdate()
    {
        if (rigidbodyComponent == null)
            rigidbodyComponent = GetComponent<Rigidbody2D>();

        rigidbodyComponent.velocity = movement;
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
            InvokeRepeating("EnemyShotAttack", 0, 2f);
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            canShoot = true;
            CancelInvoke();
        }
    }

    void EnemyShotAttack()
    {
        Instantiate(EnemyBullet, EnemyCenter.transform.position, Quaternion.identity);
    }
}
