using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

    [SerializeField] private ObjectPool bulletPool;  // Reference to your bullet object pool
    [SerializeField] private Transform firePoint;    // Where bullets spawn
    [SerializeField] private float fireRate = 0.5f; // Time between shots
    private float nextFireTime = 0f;
    private bool facingRight = true;
    private Animator animator;
    private bool isGrounded;
    private Rigidbody2D rb;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && Time.time > nextFireTime)  // Fire with J
        {
            FireBullet();
            nextFireTime = Time.time + fireRate;
        }
        HandleMovement();
        HandleJump();
        UpdateAnimation();
    }

    private void FireBullet()
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
    }

    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        if (moveInput > 0)
        {
            facingRight = true;
            transform.localScale = new Vector3(1, 1, 1);
        } 
        else if (moveInput < 0)
        {
            facingRight = false;
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
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
}
