using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public int damage = 30;        // Sát thương của đạn

    // Khi đạn va chạm
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra nếu va chạm với Player
        if (collision.CompareTag("Player"))
        {
            // Lấy ShieldController để kiểm tra khiên có hoạt động không
            var shieldController = collision.GetComponent<ShieldController>();

            // Kiểm tra xem khiên có đang hoạt động không
            if (shieldController != null && shieldController.IsShieldActive())
            {
                Debug.Log("Khiên đã chặn đạn từ Boss!");
                Destroy(gameObject); // Hủy đạn sau khi va chạm vào khiên
                return; // Thoát khỏi hàm, không gây sát thương
            }

            // Nếu không có khiên hoặc khiên không hoạt động, gây sát thương bình thường
            var healthManager = collision.GetComponent<HealthManager>();
            if (healthManager != null)
            {
                healthManager.TakeDamage(damage); // Gây sát thương cho player
            }

            Destroy(gameObject); // Hủy đạn sau khi va chạm
        }
    }
}