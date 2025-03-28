using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerBullet : MonoBehaviour
{
   

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float lifeTime = 0.5f;
    public Boolean isBossDestroy = false;
    public int countBossTakeDame = 0;
    private Rigidbody2D rb;
    private ObjectPool objectPool;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        rb.velocity = Vector2.zero;  // Reset velocity
        Invoke("ReturnToPool", lifeTime);
    }

    public void Launch(Vector2 direction)
    {
        //Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.CompareTag("Ground"))
        //{
        //    ReturnToPool();
        //}
        //else 
        if (collision.CompareTag("Enemy"))
        {

            Destroy(collision.gameObject);
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

    }
    private void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}
