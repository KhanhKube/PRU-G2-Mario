using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{

    private bool isGameOver = false;
    public int CoinNum = 0;
    public int CurrentCoin;
    public float fallThreshold = -10f; // Giới hạn khi rơi xuống (tuỳ chỉnh theo map)
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
        Time.timeScale = 1f;
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
        if (isGameOver || player == null) return;

        if (player.IsDestroyed())
        {
            CheckGameOver();
            isGameOver = true;
            return;
        }

        if (player.isWin)
        {
            CheckGameWin();
        }

        // Kiểm tra nếu nhân vật rơi ra khỏi map
        if (player.transform.position.y < fallThreshold)
        {
            Debug.Log("Player fell off the map!");
            CheckGameOver();
            isGameOver = true; // Đánh dấu game đã kết thúc để dừng Update()
        }

    }

    // Ăn táo
    public void CollectCoin(GameObject coin)
    {
        CoinNum++;
        audioManager.PlayCoinSound();
        Destroy(coin);
        UpdateUI();     
    }
    public void CheckGameWin()
    {
        CurrentCoin += CoinNum;
        PlayerPrefs.SetInt("Coin", CurrentCoin);
        PlayerPrefs.Save();
        // Xác định màn chơi tiếp theo
        string currentScene = SceneManager.GetActiveScene().name;
        string nextScene = "";

        if (currentScene == "Island")
            nextScene = "Nui_Lua";
        else if (currentScene == "Nui_Lua")
            nextScene = "Sky_City";
        else
            nextScene = "Island";  // Nếu đã xong Sky_City, quay lại Island (hoặc có thể đổi)

        // Lưu màn chơi tiếp theo
        PlayerPrefs.SetString("LastPlayedMap", nextScene);
        PlayerPrefs.Save();

        PopupGameWin.SetActive(true);
        Time.timeScale = 0f;
        audioManager.gameWinSound();
    }

    public void CheckGameOver()
    {
        audioManager.gameOverSound();
        // Hiển thị "Game Over"
        PopupGameOver.SetActive(true);
        // Tạm dừng game, ngăn người chơi di chuyển tiếp (tuỳ chọn)
        Time.timeScale = 0f;

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
        PlayerPrefs.SetString("LastPlayedMap", "Island"); // Lưu lại màn hiện tại
        PlayerPrefs.Save();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Island");
    }

    public void NextMap_NuiLua()
    {
        PlayerPrefs.SetString("LastPlayedMap", "Nui_Lua"); // Lưu lại màn hiện tại
        PlayerPrefs.Save();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Nui_Lua");
    }

    public void NextMap_SkyCity()
    {
        PlayerPrefs.SetString("LastPlayedMap", "Sky_City"); // Lưu lại màn hiện tại
        PlayerPrefs.Save();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Sky_City");
    }

   
}
