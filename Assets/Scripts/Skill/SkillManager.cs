using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class SkillManager : MonoBehaviour
{
    // Lưu danh sách kỹ năng với giá tiền
    public Dictionary<string, int> skillPrices = new Dictionary<string, int>();
    public Text SkillCostYellow;
    public Text SkillCostRed;
    // Giá tiền của từng kỹ năng
    private int playerCoins = 100;
    [System.Serializable]
    public class Skill
    {
        public string skillName;
        public int price;
    }

    public Skill[] skills; // Mảng chứa danh sách kỹ năng

    private void Start()
    {
        // Đưa kỹ năng vào Dictionary để dễ quản lý
        foreach (Skill skill in skills)
        {
            skillPrices[skill.skillName] = skill.price;
        }
        UpdateUI();
    }

    // Hàm lấy giá của kỹ năng
    public int GetSkillPrice(string skillName)
    {
        if (skillPrices.ContainsKey(skillName))
        {
            return skillPrices[skillName];
        }
        return -1; // Trả về -1 nếu không tìm thấy kỹ năng
    }

    // Hàm mua kỹ năng
    public void BuySkillYeLLow()
    {
        if (skillPrices.ContainsKey("Skill_Yellow_Low") && playerCoins >= skillPrices["Skill_Yellow_Low"])
        {
            playerCoins -= skillPrices["Skill_Yellow_Low"]; // Trừ tiền
            Debug.Log("Đã mua kỹ năng: " + "Skill_Yellow_Low");
           
        }
        Debug.Log("Không đủ tiền để mua: " + "Skill_Yellow_Low");
       
    }
    public void BuySkillRed()
    {
        if (skillPrices.ContainsKey("Skill_Red_Low") && playerCoins >= skillPrices["Skill_Red_Low"])
        {
            playerCoins -= skillPrices["Skill_Red_Low"]; // Trừ tiền
            Debug.Log("Đã mua kỹ năng: " + "Skill_Red_Low");
           
        }
        Debug.Log("Không đủ tiền để mua: " + "Skill_Red_Low");
       
    }
    private void UpdateUI()
    {
        SkillCostYellow.text = $"{skillPrices.GetValueOrDefault("Skill_Yellow_Low", 0)}";
        SkillCostRed.text = $"{skillPrices.GetValueOrDefault("Skill_Red_Low", 0)}";
    }
}
