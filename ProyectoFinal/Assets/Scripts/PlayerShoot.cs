using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {
    public int damage = 1;

    void Start()
    {
        Destroy(gameObject, 2);
    }

    void Update()
    {
        gameObject.transform.Translate(new Vector3(0.1f, 0, 0));
    }
}
