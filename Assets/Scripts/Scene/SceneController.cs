using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private void Awake()
    {
        // Xóa toàn bộ dữ liệu cũ mỗi khi game chạy lại
        PlayerPrefs.DeleteAll();

        // Thiết lập giá trị mặc định
        PlayerPrefs.SetInt("Coin", 0);
        PlayerPrefs.SetString("LastPlayedMap", "Island");
        PlayerPrefs.SetInt("MaxAmmo", 10);
        PlayerPrefs.Save();

        Debug.Log("Game has been reset! LastPlayedMap = " + PlayerPrefs.GetString("LastPlayedMap"));
    }


    private void ResetGameData()
    {
        PlayerPrefs.DeleteAll(); // Xóa toàn bộ dữ liệu cũ

        // Thiết lập giá trị mặc định
        PlayerPrefs.SetInt("Coin", 0);
        PlayerPrefs.SetString("LastPlayedMap", "Island");
        PlayerPrefs.SetInt("MaxAmmo", 10);
        PlayerPrefs.Save();

        Debug.Log("Game data has been reset! LastPlayedMap = " + PlayerPrefs.GetString("LastPlayedMap"));
    }


    public void LoadGameScene()
    {
        string lastMap = PlayerPrefs.GetString("LastPlayedMap", "Island"); // Mặc định là Island nếu chưa có dữ liệu
        Debug.Log("Loading map: " + lastMap);
        SceneManager.LoadScene(lastMap);
    }
    
    public void LoadShopScene()
    {
        SceneManager.LoadScene("UpdatePlayer"); // Chuyển đến scene cửa hàng
    }
    public void LoadGuiScene()
    {
        SceneManager.LoadScene("GuiGame"); // Chuyển đến scene cửa hàng
    }
    public void Exit()
    {
        SceneManager.LoadScene("MainScene"); // Chuyển đến scene cửa hàng
    }
}
