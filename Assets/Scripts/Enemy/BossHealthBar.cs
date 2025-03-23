using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider healthBar;
    public BossController bossHealth;

    private void Start()
    {
        healthBar = GetComponent<Slider>();
        bossHealth = GetComponentInParent<BossController>(); // Lấy component Health từ boss
        healthBar.maxValue = bossHealth.maxHealth;
        healthBar.value = bossHealth.maxHealth;
    }

    public void SetHealth(int hp)
    {
        healthBar.value = hp;
    }
}