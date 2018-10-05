using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject NewEnemy;
    public GameObject SpawnerCenter;
    public int SpawnerHealth = 1;

    // Use this for initialization
    void Start () {
        InvokeRepeating("CreateEnemy", 0, 2f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void CreateEnemy()
    {
        Instantiate(NewEnemy, SpawnerCenter.transform.position, Quaternion.identity);
    }

    public void ChangeEnemyLife(int damage)
    {
        SpawnerHealth = SpawnerHealth - damage;

        if (SpawnerHealth == 0)
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

    }
}
