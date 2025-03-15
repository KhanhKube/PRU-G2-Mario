using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class UpgradeStats : MonoBehaviour
{
    public int maxHealth;
    public int maxMana = 100;
    private int gold; // Số vàng ban đầu

    public int healthUpgradeCost = 20; // Giá nâng cấp máu
    public int manaUpgradeCost = 20; // Giá nâng cấp mana
    public int healthIncreaseAmount = 10; // Mỗi lần nâng cấp, tăng bao nhiêu HP
    public int manaIncreaseAmount = 10; // Mỗi lần nâng cấp, tăng bao nhiêu MP

    public Text healthText;
    public Text manaText;
    public Text healthCostText;
    public Text manaCostText;
  


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

        }
    }

    public void UpgradeMana()
    {
        if (gold >= manaUpgradeCost)
        {
            Debug.LogWarning("MP+");
            gold -= manaUpgradeCost;
            maxMana += manaIncreaseAmount;
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        healthText.text = $"HP Max: {maxHealth}";
        manaText.text = $"MP Max: {maxMana}";
        goldText.text = $"{gold}";
        healthCostText.text = $"{healthUpgradeCost}";
        manaCostText.text = $"{manaUpgradeCost}";
    }
}
