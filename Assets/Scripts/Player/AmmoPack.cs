using UnityEngine;

public class AmmoPack : MonoBehaviour
{
    [SerializeField] private int ammoAmount = 5; // Số đạn nhận được khi nhặt
    [SerializeField] private AudioClip pickupSound; // Âm thanh khi nhặt

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ReloadAmmo(ammoAmount); // Nạp đạn
                Destroy(gameObject); // Xóa hộp đạn sau khi nhặt
                Debug.Log("nạp 5 viên");
            }
        }
    }
}
