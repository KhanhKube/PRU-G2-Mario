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
        if (PlayerPrefs.HasKey("MaxHealth"))
        {
            maxHealth = PlayerPrefs.GetInt("MaxHealth");
        }
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        PlayerPrefs.SetInt("MaxHealth", maxHealth);
        PlayerPrefs.Save();
       
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
        if (currentHealth <= 0) {
            currentHealth = 0;        
        }
    }

    public void IncreaseDamage(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        healthBar.SetCurrentHealth(currentHealth);
    }
}
