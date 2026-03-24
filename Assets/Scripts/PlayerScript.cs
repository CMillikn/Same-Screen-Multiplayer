using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerScript : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public GameObject deathPuff;
    public Slider healthBar;

    public void Start()
    {
        maxHealth = currentHealth;
        healthBar.maxValue = maxHealth;
    }
    
    public void GetHurt(float damage)
    {
        currentHealth = currentHealth - (damage * Time.deltaTime);
    }

    public void Update()
    {
        healthBar.value = currentHealth;
        if (currentHealth <= 0)
        {
            Instantiate(deathPuff, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
