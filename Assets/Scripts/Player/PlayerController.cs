using System;
using System.Collections;

using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ObjectPool bulletPool;  // Reference to your bullet object pool
    [SerializeField] private Transform firePoint;    // Where bullets spawn
    [SerializeField] private float fireRate = 0.5f;  // Time between shots
    private float nextFireTime = 0f;
    private bool facingRight = true;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    public int maxAmmo = 10;  // Số đạn tối đa
    private int currentAmmo;
    public Boolean isWin = false;
    private Animator animator;
    private bool isGrounded;
    private Rigidbody2D rb;
    private HealthManager healthManager;
    private GameManager gameManager;
    private AudioManager audioManager;
    //shield
    private ShieldController shieldController;

    public int GetCurrentAmmo() { return currentAmmo; }
    public int GetMaxAmmo() { return maxAmmo; }

    public void Awake()
    {
        shieldController = GetComponent<ShieldController>();

        if (PlayerPrefs.HasKey("MaxAmmo"))
        {
            maxAmmo = PlayerPrefs.GetInt("MaxAmmo");
        }
       
        healthManager = GetComponent<HealthManager>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindAnyObjectByType<GameManager>();
        audioManager = FindAnyObjectByType<AudioManager>();
        currentAmmo = maxAmmo;
      
    }

    void Start()
    {
        
        PlayerPrefs.SetInt("MaxAmmo", maxAmmo);
        PlayerPrefs.Save();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && Time.time > nextFireTime && currentAmmo > 0)  // Fire with J
        {
            FireBullet();
            nextFireTime = Time.time + fireRate;
        }
        //if (gameManager.IsGameOver() || gameManager.IsGameWin()) return;
        HandleMovement();
        HandleJump();
        UpdateAnimation();
    }
    private void FireBullet()
    {
        if (firePoint != null)
        {
            GameObject bullet = bulletPool.GetObject();  // Get a bullet from the pool
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = Quaternion.identity;
            bullet.SetActive(true);

            PlayerBullet bulletComponent = bullet.GetComponent<PlayerBullet>();
            if (bulletComponent != null)
            {
                Vector2 direction = facingRight ? Vector2.right : Vector2.left;  // Fire in the correct direction
                bulletComponent.Launch(direction);
            }

            currentAmmo--; // Giảm số lượng đạn khi bắn
            Debug.Log("Số đạn còn lại: " + currentAmmo);
            UIManager.Instance.UpdateAmmoUI(currentAmmo, maxAmmo);
        }

    }

    public void ReloadAmmo(int amount)
    {
        currentAmmo = Mathf.Min(currentAmmo + amount, maxAmmo);
        UIManager.Instance.UpdateAmmoUI(currentAmmo, maxAmmo);
        Debug.Log("Đã nạp đạn. Số đạn hiện tại: " + currentAmmo);
    }

    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        if (moveInput > 0) {
            facingRight = true;
            transform.localScale = new Vector3(1, 1, 1); }
        else if (moveInput < 0) {
            facingRight = false;
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            audioManager.PlayJumpSound();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

    }
    private void UpdateAnimation()
    {
        bool isRunning = Mathf.Abs(rb.velocity.x) > 0.1f;
        bool isJumping = !isGrounded;
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
// <<<<<<< hai-Boss
//         // Xử lý các va chạm không liên quan đến sát thương
// =======
//         if (collision.CompareTag("Enemy") )
//         {

//             if (healthManager != null)
//             {
//                 healthManager.TakeDamage(20);

//                 // Chỉ hủy người chơi nếu máu giảm xuống 0
//                 if (healthManager.currentHealth <= 0)
//                 {
//                     Destroy(gameObject);
//                     gameManager.CheckGameOver();
//                 }
//             }
//             else
//             {
//                 Debug.Log("Player va chạm với Enemy!");
//                 // Nếu Player không có healthManager, chỉ cần Destroy
//                 Destroy(gameObject);
//                 gameManager.CheckGameOver();
//             }

            
       

// >>>>>>> develop
        if (collision.CompareTag("HeadEnemy"))
        {
            // Lấy GameObject cha của HeadEnemy (Enemy)
            GameObject enemyParent = collision.transform.parent.gameObject;
            if (enemyParent != null)
            {
                Destroy(enemyParent); // Hủy cả Enemy
            }
            JumpAfterStomp();
            return;
        }

        if (collision.CompareTag("Key"))
        {
            isWin = true;
            return;
        }

        // Kiểm tra khiên cho các va chạm gây sát thương
        if (collision.CompareTag("Enemy") || collision.CompareTag("Boss") || collision.CompareTag("Trap"))
        {
            // Kiểm tra xem khiên có đang hoạt động không
            if (shieldController != null && shieldController.IsShieldActive())
            {
                Debug.Log("Khiên đã chặn sát thương từ: " + collision.tag);

                // bossđẩy người chơi ra
                if (collision.CompareTag("Boss"))
                {
                    Vector2 knockback = new Vector2(-transform.localScale.x * 5f, 5f);
                    rb.velocity = knockback;
                }

                return; // Không gây sát thương nếu khiên đang hoạt động
            }

            // Xử lý sát thương khi không có khiên
            if (healthManager != null)
            {
                int damageAmount = 20; // Mặc định cho Enemy và Trap

                if (collision.CompareTag("Boss"))
                {
                    damageAmount = 30; 

                    // Đẩy Player ra khỏi Boss khi chạm
                    Vector2 knockback = new Vector2(-transform.localScale.x * 5f, 5f);
                    rb.velocity = knockback;
                }

                healthManager.TakeDamage(damageAmount);

                // Kiểm tra nếu máu giảm xuống 0
                if (healthManager.currentHealth <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Enemy"))
    //    {
    //        if (healthManager != null)
    //        {
    //            healthManager.TakeDamage(20);

    //            // Chỉ hủy người chơi nếu máu giảm xuống 0
    //            if (healthManager.currentHealth <= 0)
    //            {
    //                Destroy(gameObject);
    //            }
    //        }
    //    }

    //}


    private void JumpAfterStomp()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(rb.velocity.x, 17f); 
        }
    }

}
