using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Nui_Lua"); // Chuyển đến scene chơi game
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
