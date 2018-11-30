using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject Creation;
    public GameObject SpawnerCenter;

    // Use this for initialization
    void Start () {
        InvokeRepeating("Create", 0, 2f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Create()
    {
        Instantiate(Creation, SpawnerCenter.transform.position, Quaternion.identity);
    }

    
}
