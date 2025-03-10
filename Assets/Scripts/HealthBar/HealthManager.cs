using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth ;

    public HealthBar healthBar ;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetCurrentHealth(currentHealth);
    }
}
