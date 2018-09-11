using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {
    
    public int damage = 1;
    
    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, 2);
    }
    
    void Update()
    {
        gameObject.transform.Translate(new Vector3(-0.1f, 0, 0));
    }


}
