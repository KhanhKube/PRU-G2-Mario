using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private int maxHealth = 100; // Máu tối đa của Boss
    private int currentHealth;

    [SerializeField] private Slider healthBar; // Thanh máu của Boss
                                               // Start is called before the first frame update
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>(); // Tìm GameManager trong scene
        currentHealth = maxHealth;
        UpdateHealthBar();
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = (float)currentHealth / maxHealth;
        }
    }
    private void Die()
    {
        Debug.Log("Boss đã bị tiêu diệt!");
        Destroy(gameObject);
        gameManager.CheckGameWin();  // Gọi CheckGameWin khi boss chết
    }
}
