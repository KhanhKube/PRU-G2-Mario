using System;
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
    public int curHealth = 0;
    public int maxHealth = 100;
    public BossHealthBar healthBar;

    public Transform[] waypoints; // Danh sách điểm đến
    public float speed = 3f; // Tốc độ di chuyển
    private Transform targetPoint; // Điểm đến hiện tại
    private int currentIndex = -1;

    void Start()
    {
        curHealth = maxHealth;
        GetNewTargetPosition();
        animator = GetComponent<Animator>();
        if (waypoints.Length > 0)
            ChooseNextPoint();
    }

    void Update()
    {
        if (IsPlayerInBossArea())
        {
            AttackPlayer();
            ChasePlayer();
        }
        else
        {
            //Patrol();
        }
        
        if (targetPoint != null)
        {
            // Di chuyển quái đến điểm tiếp theo
            transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
            
                Debug.Log(transform.position);
                Debug.Log(targetPoint.position);
            // Nếu đã đến gần điểm, chọn điểm mới
            if (Vector2.Distance(transform.position, targetPoint.position) < 0.2f)
            {
                ChooseNextPoint();
            }
        }
    }

    public void DamageBoss(int damage)
    {
        curHealth -= damage;
        if (healthBar != null)
        {
            healthBar.SetHealth(curHealth);
        }
        if (curHealth <= 0)
        {
            gameObject.SetActive(false); // Ẩn boss khi chết
        }
    }

    void ChooseNextPoint()
    {
        int newIndex;
        do
        {
            newIndex = UnityEngine.Random.Range(0, waypoints.Length);
        } while (newIndex == currentIndex); // Tránh trùng điểm liên tiếp
        currentIndex = newIndex;
        targetPoint = waypoints[currentIndex];
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
                rb.velocity = new Vector2(UnityEngine.Random.Range(-2f, 2f), 5f); // Key rơi xuống một cách ngẫu nhiên
            }
        }
    }

    // lưu kích thước gốc của Boss
    private Vector3 originalScale;
    // Boss Di chuyển theo player 
    float stopDistance = 6f;
    void ChasePlayer()
    {
        if (player != null)
        {
            //transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance > stopDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            }

            // Xoay mặt Boss theo hướng Player (nếu cần)
            // Chỉ lật theo trục X, không làm thay đổi kích thước Y, Z
            if (player.position.x > transform.position.x)
                transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
            else
                transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        
        }
    }

}
