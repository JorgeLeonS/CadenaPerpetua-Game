using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escalera : MonoBehaviour
{
    SpriteRenderer spriteComponent;

    // Use this for initialization
    void Start()
    {
        spriteComponent = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Colision con bala de enemigo
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            spriteComponent.GetComponent<SpriteRenderer>().color = Color.red;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Colision con bala de enemigo
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            spriteComponent.GetComponent<SpriteRenderer>().color = Color.white;

        }
    }
}
