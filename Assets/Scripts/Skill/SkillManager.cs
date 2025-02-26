using UnityEngine;
using System.Collections.Generic;

public class SkillManager : MonoBehaviour
{
    // Lưu danh sách kỹ năng với giá tiền
    public Dictionary<string, int> skillPrices = new Dictionary<string, int>();

    // Giá tiền của từng kỹ năng
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
    public bool BuySkill(string skillName, ref int playerCoins)
    {
        if (skillPrices.ContainsKey(skillName) && playerCoins >= skillPrices[skillName])
        {
            playerCoins -= skillPrices[skillName]; // Trừ tiền
            Debug.Log("Đã mua kỹ năng: " + skillName);
            return true;
        }
        Debug.Log("Không đủ tiền để mua: " + skillName);
        return false;
    }
}
