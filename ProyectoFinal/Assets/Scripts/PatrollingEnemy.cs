using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : MonoBehaviour {

    private bool dirRight = true;
    public float patrollingSpeed = 2.0f;
    float posx;

    // Use this for initialization
    void Start () {
      
        posx = transform.position.x;
	}


	
	// Update is called once per frame
	void Update () {
        if (dirRight)
            transform.Translate(Vector2.right * patrollingSpeed * Time.deltaTime);
        else
            transform.Translate(-Vector2.right * patrollingSpeed * Time.deltaTime);

        if (transform.position.x >= posx)
        {
            dirRight = false;
        }

        if (transform.position.x <= (posx-4))
        {
            dirRight = true;
        }
    }
}
