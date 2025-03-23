using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private Vector3 originalPosition; // Lưu vị trí ban đầu
    private bool isFalling = false;   // Kiểm tra platform có đang rơi không
    private Coroutine fallCoroutine;  // Lưu trữ Coroutine để dừng khi cần

    [SerializeField] private float fallDelay = 1f;  // Thời gian trước khi rơi
    [SerializeField] private float resetDelay = 1f; // Thời gian để quay lại vị trí ban đầu
    private Rigidbody2D rb;
    private Collider2D col;

    private void Start()
    {
        originalPosition = transform.position; // Lưu vị trí ban đầu
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();


        // Cấu hình Rigidbody mặc định
        rb.bodyType = RigidbodyType2D.Kinematic;  // Giữ cố định trước khi rơi
        rb.gravityScale = 0;                      // Không bị ảnh hưởng bởi trọng lực khi chưa rơi
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; // Tránh lỗi va chạm khi rơi nhanh
        rb.interpolation = RigidbodyInterpolation2D.None; // Hoặc dùng Interpolate nếu bị giật
        rb.freezeRotation = true;                 // Ngăn xoay khi rơi
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);

            // Nếu chưa có Coroutine, bắt đầu đếm ngược để rơi
            if (!isFalling)
            {
                fallCoroutine = StartCoroutine(FallAfterDelay());
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);

            // Dừng Coroutine nếu Player rời đi trước khi platform rơi
            if (fallCoroutine != null)
            {
                StopCoroutine(fallCoroutine);
                fallCoroutine = null;
            }

            // Trả platform về vị trí cũ sau 3s
            StartCoroutine(ResetPosition());
        }
    }

    private IEnumerator FallAfterDelay()
    {
        yield return new WaitForSeconds(fallDelay);
        isFalling = true;
        rb.bodyType = RigidbodyType2D.Dynamic; // Bật chế độ vật lý
        rb.gravityScale = 10f; // Rơi CỰC NHANH
        col.isTrigger = true; // Tránh va chạm khi rơi xuống
    }

    private IEnumerator ResetPosition()
    {
        yield return new WaitForSeconds(resetDelay);
        isFalling = false;
        rb.bodyType = RigidbodyType2D.Kinematic;
        col.isTrigger = false; // Bật lại va chạm

        transform.position = originalPosition;
    }
}
