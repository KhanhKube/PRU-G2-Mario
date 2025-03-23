using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public int damage = 30;        // Sát thương của đạn

    void Update()
    {
    }

    // Khi đạn va chạm
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra nếu va chạm với Player
        if (collision.CompareTag("Player"))
        {
            var healthManager = collision.GetComponent<HealthManager>();
            if (healthManager != null)
            {
                healthManager.TakeDamage(damage); // Gây sát thương cho player
            }
            Destroy(gameObject); // Hủy đạn sau khi va chạm
        }
    }
   
}
