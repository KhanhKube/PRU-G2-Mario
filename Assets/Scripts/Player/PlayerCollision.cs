using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
            float knockbackForce = 30f; // Điều chỉnh lực đẩy

            rb.velocity = Vector2.zero; // Đặt vận tốc về 0 để tránh lỗi
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

            Debug.Log("Player bị đẩy lùi!");
        }
    }
}
