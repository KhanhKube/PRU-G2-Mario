using UnityEngine;

public class BossController : MonoBehaviour
{
    public Transform player;
    public GameObject bossAreaObject; 
    public float moveSpeed = 3f;
    public float attackCooldown = 2f;
    private float attackTimer = 0f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private Vector3 targetPosition;
    public float bulletSpeed = 10f;
    private Animator animator;
    public GameObject keyPrefab; // Gán Key Prefab trong Inspector
    public Transform dropPoint; // Điểm rơi của Key (có thể là Boss)
    void Start()
    {
        GetNewTargetPosition();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (IsPlayerInBossArea())
        {
            AttackPlayer();
        }
        else
        {
            //Patrol();
        }
    }

    bool IsPlayerInBossArea()
    {
        return bossAreaObject.GetComponent<BossArea>().isPlayerInside;
    }

    void AttackPlayer()
    {
        Debug.Log("Boss tấn công!");
        if (attackTimer <= 0)
        {
            ShootAtPlayer();
            animator.SetTrigger("attack");
            attackTimer = attackCooldown;
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }
    }

    void ShootAtPlayer()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector2 direction = (player.position - firePoint.position).normalized;
            rb.velocity = direction * bulletSpeed;

            // Xoay viên đạn theo hướng bay
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle + 180f);
        }
    }


    void Patrol()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.5f)
        {
            GetNewTargetPosition();
        }
    }

    void GetNewTargetPosition()
    {
        
    }
    
    private void OnDestroy()
    {
        if (keyPrefab != null)
        {
            GameObject key = Instantiate(keyPrefab, dropPoint.position, Quaternion.identity);
            Rigidbody2D rb = key.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = new Vector2(Random.Range(-2f, 2f), 5f); // Key rơi xuống một cách ngẫu nhiên
            }
        }
    }
}
