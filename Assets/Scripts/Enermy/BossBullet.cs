using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int damage = 20; // Lượng sát thương của đạn Boss

    private void OnTriggerEnter2D(Collider2D collision)
    {
        void Start()
        {
            // Tự hủy viên đạn sau 3 giây
            Destroy(gameObject, 3f);
        }

        if (collision.CompareTag("Player"))
        {
            HealthManager healthManager = collision.GetComponent<HealthManager>();
            if (healthManager != null)
            {
                healthManager.TakeDamage(damage);

                // Kiểm tra nếu máu về 0 thì hủy Player
                if (healthManager.currentHealth <= 0)
                {
                    Destroy(collision.gameObject);
                }
            }

            // Hủy đạn sau khi chạm vào Player
            Destroy(gameObject);
        }
        //else if (collision.CompareTag("Ground"))
        //{
        //    // Hủy đạn nếu chạm vào mặt đất
        //    Destroy(gameObject);
        //}
    }
   
}
