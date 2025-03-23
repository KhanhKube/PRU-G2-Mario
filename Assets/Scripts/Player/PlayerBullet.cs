using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;  // Tốc độ di chuyển
    [SerializeField] private float lifeTime = 0.5f;  // Thời gian tồn tại
    [SerializeField] private int damage = 50;        // Sát thương gây ra

    private Rigidbody2D rb;
    private ObjectPool objectPool;

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
        if (collision.CompareTag("Boss"))
        {
            BossHealth enemyHealth = collision.GetComponent<BossHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            ReturnToPool();
        }
        if (collision.CompareTag("Boss"))
        {
            var bossHealth = collision.GetComponent<BossController>();
            if (bossHealth != null)
            {
                bossHealth.DamageBoss(10);
            }
            //countBossTakeDame += 1;
            //Debug.Log(countBossTakeDame);
            //if(countBossTakeDame == 10)
            //{
            //    isBossDestroy = true;
            //}
        }

    private void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}
