using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour {
    public Transform bulletPrefab;
    public float fireRate = 0.25f;
    private double shootCooldown;

    // Use this for initialization
    void Start () {
        shootCooldown = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
    }

    public void Attack(bool isEnemy)
    {
        if (CanAttack)
        {
            shootCooldown = fireRate;

            //Crea una nueva bala
            var shotTransform = Instantiate(bulletPrefab) as Transform;

            //Posición de la bala
            shotTransform.position = transform.position;

            // Tiro Enemigo
            EnemyShoot enemyShot = shotTransform.gameObject.GetComponent<EnemyShoot>();
            if (enemyShot != null)
            {
                enemyShot.enemyShot = isEnemy;
            }

            
        }
    }

    public bool CanAttack
    {
        get
        {
            return shootCooldown <= 0;
        }
    }

    public void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Player playerPickUp = otherCollider.gameObject.GetComponent<Player>();
        if (playerPickUp != null)
        {
            Destroy(gameObject);
        }
    }
}
