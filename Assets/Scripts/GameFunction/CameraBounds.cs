using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    public Transform player; // Kéo Player vào đây
    public Vector2 minBounds; // Giới hạn trái & dưới của bản đồ
    public Vector2 maxBounds; // Giới hạn phải & trên của bản đồ
    public float smoothSpeed = 0.1f; // Tốc độ di chuyển camera mượt mà

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (player == null) return;

        // Lấy vị trí của Player
        Vector3 targetPosition = player.position;

        // Giới hạn Camera trong vùng cho phép
        targetPosition.x = Mathf.Clamp(targetPosition.x, minBounds.x, maxBounds.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minBounds.y, maxBounds.y);
        targetPosition.z = transform.position.z; // Giữ nguyên trục Z

        // Áp dụng di chuyển mượt
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed);
    }
}
