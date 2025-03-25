using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;  // Tốc độ di chuyển
    [SerializeField] private float lifeTime = 0.5f;  // Thời gian tồn tại
    [SerializeField] private int damage = 50;        // Sát thương gây ra

    private Rigidbody2D rb;
    private ObjectPool objectPool;
    public Transform explosionPrefab;
    private bool hasExploded = false;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        objectPool = FindObjectOfType<ObjectPool>(); // Lấy ObjectPool từ scene
    }

    void OnEnable()
    {
        rb.velocity = Vector2.zero;  // Reset vận tốc
        Invoke(nameof(ReturnToPool), lifeTime); // Trả về Pool sau thời gian
    }

    public void Launch(Vector2 direction)
    {
        rb.velocity = direction * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Boss"))
        {
            // Giảm máu Boss nếu có BossHealth script
            BossHealth bossHealth = collision.GetComponent<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(damage);
            }

            // Gọi DamageBoss nếu có BossController script
            BossController bossController = collision.GetComponent<BossController>();
            if (bossController != null)
            {
                bossController.DamageBoss(10);
            }
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
