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
    [SerializeField] private int maxAmmo = 10;  // Số đạn tối đa
    private int currentAmmo;

    private Animator animator;
    private bool isGrounded;
    private Rigidbody2D rb;
    private HealthManager healthManager;
    //private GameManager gameManager;
    private AudioManager audioManager;

    public int GetCurrentAmmo() { return currentAmmo; }
    public int GetMaxAmmo() { return maxAmmo; }

    private void Awake()
    {
        healthManager = GetComponent<HealthManager>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //gameManager = FindAnyObjectByType<GameManager>();
        audioManager = FindAnyObjectByType<AudioManager>();

        currentAmmo = maxAmmo;
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
        if (collision.CompareTag("Enemy") )
        {

            if (healthManager != null)
            {
                healthManager.TakeDamage(20);

                // Chỉ hủy người chơi nếu máu giảm xuống 0
                if (healthManager.currentHealth <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }

        if (collision.CompareTag("HeadEnemy"))
        {
            // Lấy GameObject cha của HeadEnemy (Enemy)
            GameObject enemyParent = collision.transform.parent.gameObject;

            if (enemyParent != null)
            {
                Destroy(enemyParent); // Hủy cả Enemy
            }

            // Gọi hàm nhảy lại của Player sau khi đạp trúng
            JumpAfterStomp();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (healthManager != null)
            {
                healthManager.TakeDamage(20);

                // Chỉ hủy người chơi nếu máu giảm xuống 0
                if (healthManager.currentHealth <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }


    private void JumpAfterStomp()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(rb.velocity.x, 17f); 
        }
    }

}
