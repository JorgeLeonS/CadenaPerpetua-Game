using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour {

    public static GlobalControl Instance;

    public int playerHealth;
    public int playerShield;
    public int playerScore;
    public Vector2 limiteX;

    public string CurrentWeapon;
    public bool GunUnlocked;
    public bool MacanaUnlocked;
    public bool KnifeUnlocked;

    void Start()
    {
        
    }

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    
}
