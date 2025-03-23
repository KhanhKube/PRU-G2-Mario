using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using UnityEngine.UI;

public class UpgradeStats : MonoBehaviour
{
    public int maxHealth;
    public int maxAmmo;
    private int gold; // Số vàng ban đầu

    public int healthUpgradeCost = 20; // Giá nâng cấp máu
    public int ammoUpgradeCost = 20; // Giá nâng cấp mana
    public int healthIncreaseAmount = 10; // Mỗi lần nâng cấp, tăng bao nhiêu HP
    public int ammoIncreaseAmount = 1; // Mỗi lần nâng cấp, tăng bao nhiêu MP

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI ammoText;
    public Text healthCostText;
    public Text ammoCostText;
  


    public Text goldText;

    void Start()
    {
        if (PlayerPrefs.HasKey("MaxHealth"))
        {
            maxHealth = PlayerPrefs.GetInt("MaxHealth");
        }
        if (PlayerPrefs.HasKey("Coin"))
        {
            gold = PlayerPrefs.GetInt("Coin");
        }
        if (PlayerPrefs.HasKey("MaxAmmo"))
        {
            maxAmmo = PlayerPrefs.GetInt("MaxAmmo");
        }
        UpdateUI();
    }

    public void UpgradeHealth()
    {
        if (gold >= healthUpgradeCost)
        {
            Debug.LogWarning("HP+");
            gold -= healthUpgradeCost;
            maxHealth += healthIncreaseAmount;
            UpdateUI();
            PlayerPrefs.SetInt("MaxHealth", maxHealth);
            PlayerPrefs.SetInt("Coin", gold); // Cập nhật lại số coin sau khi dùng
            PlayerPrefs.Save();
        }
    }

    public void UpgradeMana()
    {
        if (gold >= ammoUpgradeCost)
        {
            Debug.LogWarning("MP+");
            gold -= ammoUpgradeCost;
            maxAmmo += ammoIncreaseAmount;
            PlayerPrefs.SetInt("MaxAmmo", maxAmmo);
            PlayerPrefs.SetInt("Coin", gold); // Cập nhật lại số coin sau khi dùng
            PlayerPrefs.Save();
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        healthText.text = $"{maxHealth}";
        ammoText.text = $"{maxAmmo}";
        goldText.text = $"{gold}";
        healthCostText.text = $"{healthUpgradeCost}";
        ammoCostText.text = $"{ammoUpgradeCost}";
    }
}
