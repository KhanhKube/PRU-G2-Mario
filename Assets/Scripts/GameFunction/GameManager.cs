﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    

    public int CoinNum = 0;
    public int CurrentCoin;
    public GameObject[] coins;
    
    public Text CoinText;
    private AudioManager audioManager;
    public GameObject popupPanel;
    public GameObject PopupGameOver;
    public GameObject PopupGameWin;
    private PlayerController player; 
    private void Awake()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        player = FindAnyObjectByType<PlayerController>();
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("Coin"))
        {
            CurrentCoin = PlayerPrefs.GetInt("Coin");
            Debug.Log("CurrentCoin: " + CurrentCoin);
        }
        CurrentCoin += CoinNum;
        popupPanel.SetActive(false);
        PopupGameOver.SetActive(false);
        PopupGameWin.SetActive(false );
        UpdateUI();
        coins = GameObject.FindGameObjectsWithTag("Coin");
        PlayerPrefs.SetInt("Coin", CurrentCoin);
        PlayerPrefs.Save();
    }

    private void Update()
    {
        if (player.IsDestroyed()) { CheckGameOver(); }

    }

    // Ăn táo
    public void CollectCoin(GameObject coin)
    {
        CoinNum++;
        audioManager.PlayCoinSound();
        Destroy(coin);
        UpdateUI();
        
        if (CoinNum > 1)
        {
            CheckGameWin();
        }
    }

   

    public void CheckGameWin()
    {
        CurrentCoin += CoinNum;
        PlayerPrefs.SetInt("Coin", CurrentCoin);
        PlayerPrefs.Save();
        PopupGameWin.SetActive(true);
        Time.timeScale = 0f;

    }

    public void CheckGameOver()
    {
        // Hiển thị "Game Over"
      PopupGameOver.SetActive(true);
        // Tạm dừng game, ngăn người chơi di chuyển tiếp (tuỳ chọn)

    }
    public void UpdateUI()
    {
        CoinText.text = $"{CurrentCoin+CoinNum}";
    }

    public void StopGame()
    {
        Debug.Log("Game Stop");
        popupPanel.SetActive(true); // Hiện popup
        Time.timeScale = 0f; 
    }
    public void ContinueGame()
    {
        popupPanel.SetActive(false); // Ẩn popup
        Time.timeScale = 1f; // Tiếp tục game
    }
    public void RestartGame()
    {
        Time.timeScale = 1f; // Đảm bảo game không bị dừng
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Load lại scene hiện tại
    }
    public void GoToMainMenu()
    {

       

        Time.timeScale = 1f; // Đảm bảo game không bị dừng
        SceneManager.LoadScene("MainScene"); // Thay bằng tên scene menu chính
        PlayerPrefs.Save();

    }
    public void NextMap_Island()
    {
        Time.timeScale = 1f; // Đảm bảo game không bị dừng
        SceneManager.LoadScene("Island");
        PlayerPrefs.Save();

    }
    public void NextMap_SkyCity()
    {
        Time.timeScale = 1f; // Đảm bảo game không bị dừng
        SceneManager.LoadScene("Sky_City");
        PlayerPrefs.Save();

    }
    public void NextMap_NuiLua()
    {
        Time.timeScale = 1f; // Đảm bảo game không bị dừng
        SceneManager.LoadScene("Nui_Lua");
        PlayerPrefs.Save();

    }
}
