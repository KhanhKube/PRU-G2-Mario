using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int healthAmount = 30;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // CHỈ phản ứng với Player
        if (collision.CompareTag("Player"))
        {
            HealthManager playerHealth = collision.GetComponent<HealthManager>();

            if (playerHealth != null)
            {
                // Tăng máu cho người chơi
                playerHealth.IncreaseDamage(healthAmount);

                // Phát âm thanh nếu có
                if (audioManager != null)
                {
                    // audioManager.PlayHealthPickupSound();
                }

                // HỦY TRÁI TIM 
                Destroy(gameObject);
            }
        }
    }
}