using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour {
    
    SpriteRenderer position;

	// Use this for initialization
	void Start () {
        position = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        position.sortingOrder = -(int)(transform.position.y * 100);
	}
}
