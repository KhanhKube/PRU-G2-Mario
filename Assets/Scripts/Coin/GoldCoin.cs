using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : MonoBehaviour
{
    [SerializeField] public GameManager gameManager;

    // Khi nhân vật va chạm với quả táo
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.CollectCoin(gameObject); // Gọi GameManager để thu thập táo và tăng điểm
        }
    }

}
