using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigTurtleEnemy : MonoBehaviour
{
    public float speed = 2f; // Tốc độ di chuyển
    public float moveDistance = 3f; // Khoảng cách di chuyển
    private Vector3 startPos;
    private bool movingRight = true;

    void Start()
    {
        startPos = transform.position; // Lưu vị trí bắt đầu
    }

    void Update()
    {
        // Di chuyển bot theo hướng hiện tại
        transform.position += (movingRight ? Vector3.right : Vector3.left) * speed * Time.deltaTime;

        // Kiểm tra nếu đã di chuyển đủ khoảng cách thì đổi hướng
        if (Vector3.Distance(startPos, transform.position) >= moveDistance)
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        movingRight = !movingRight;

        // Lật Scale theo trục X
        Vector3 newScale = transform.localScale;
        newScale.x *= -1; // Lật ngược X
        transform.localScale = newScale;
    }
}
