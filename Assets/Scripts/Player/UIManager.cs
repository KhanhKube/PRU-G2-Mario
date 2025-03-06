using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance; // Singleton để dễ gọi từ các script khác
    [SerializeField] private TextMeshProUGUI ammoText; // Tham chiếu đến UI Text

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if(player != null)
        {
            UpdateAmmoUI(player.GetCurrentAmmo(), player.GetMaxAmmo());
        }
    }

    public void UpdateAmmoUI(int currentAmmo, int maxAmmo)
    {
        ammoText.text = $"{currentAmmo}"; // Hiển thị số đạn còn lại
    }
}
