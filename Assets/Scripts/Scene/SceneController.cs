using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private void Awake()
    {
        // Kiểm tra nếu game vừa khởi động lần đầu (chưa có dữ liệu)
        if (!PlayerPrefs.HasKey("GameInitialized"))
        {
            ResetGameData(); // Reset về trạng thái ban đầu
            PlayerPrefs.SetInt("GameInitialized", 1); // Đánh dấu game đã được khởi động lần đầu
            PlayerPrefs.Save();
        }

      
    }
    private void ResetGameData()
    {
        PlayerPrefs.DeleteAll(); // Xóa toàn bộ dữ liệu cũ

        // Thiết lập giá trị mặc định
        PlayerPrefs.SetInt("Coin", 0);
        PlayerPrefs.SetString("LastPlayedMap", "Island"); // Mặc định bắt đầu ở Island
        PlayerPrefs.SetInt("MaxAmmo", 10); // Nếu có max đạn, đặt giá trị mặc định
        PlayerPrefs.Save();

        Debug.Log("Game data has been reset to default!");
    }

    public void LoadGameScene()
    {
        string lastMap = PlayerPrefs.GetString("LastPlayedMap", "Island"); // Mặc định là Island nếu chưa có dữ liệu
        SceneManager.LoadScene(lastMap);
    }
    
    public void LoadShopScene()
    {
        SceneManager.LoadScene("UpdatePlayer"); // Chuyển đến scene cửa hàng
    }
    public void Exit()
    {
        SceneManager.LoadScene("MainScene"); // Chuyển đến scene cửa hàng
    }
}
