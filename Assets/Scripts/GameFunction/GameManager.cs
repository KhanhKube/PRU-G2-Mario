using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int CoinNum = 0;
    public GameObject[] coins;
    
    public Text CoinText;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
    }
    private void Start()
    {
        UpdateUI();
        coins = GameObject.FindGameObjectsWithTag("Coin");
        PlayerPrefs.SetInt("Coin", CoinNum);
        PlayerPrefs.Save();
    }

    private void Update()
    {

     
    }

    // Ăn táo
    public void CollecCoin(GameObject coin)
    {
        CoinNum++;
        audioManager.PlayCoinSound();
        Destroy(coin);
        UpdateUI();
    }

    // Va chạm enemy
    public void Enemy(GameObject enemy)
    {
        CoinNum--;
        if (CoinNum == -1)
        {
            CheckGameOver();
        }
       
    }

    private void CheckGameWin()
    {
       

    }

    private void CheckGameOver()
    {
        // Hiển thị "Game Over"
      
        // Tạm dừng game, ngăn người chơi di chuyển tiếp (tuỳ chọn)
        Time.timeScale = 0f;
    }
    public void UpdateUI()
    {
        CoinText.text = $"{CoinNum}";
    }
}
