using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public int score = 0;
    public GameObject[] apples;


    private void Start()
    {

    }

    private void Update()
    {
       
       

        // Kiểm tra nếu không còn quả táo nào -> Game Win
        if (apples.Length == 0)
        {
            CheckGameWin();
        }

        // Kiểm tra nếu score < 0 -> Game Over
        if (score == -1)
        {
            CheckGameOver();
        }
    }

    // Ăn táo
    public void CollectApple(GameObject coin)
    {
        score++;
        Destroy(coin);
        apples = GameObject.FindGameObjectsWithTag("Coin");
    }

    // Va chạm enemy
    public void Enemy(GameObject enemy)
    {
        score--;
        if (score == -1)
        {
            CheckGameOver();
        }
        // Nếu muốn khi chạm enemy thì hủy enemy luôn, thì thêm Destroy(enemy);
        // Destroy(enemy);
    }

    private void CheckGameWin()
    {
        // Hiển thị "Game Win"
       
        // Tạm dừng game, ngăn người chơi di chuyển tiếp (tuỳ chọn)

    }

    private void CheckGameOver()
    {
        // Hiển thị "Game Over"
      
        // Tạm dừng game, ngăn người chơi di chuyển tiếp (tuỳ chọn)
        Time.timeScale = 0f;
    }

}
