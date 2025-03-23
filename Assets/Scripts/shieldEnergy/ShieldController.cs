using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShieldController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cooldownText;
    [SerializeField] private Image cooldownFillImage;
    [SerializeField] private GameObject shieldObject;
    [SerializeField] private float shieldDuration = 5f;
    [SerializeField] private float shieldCooldown = 15f;
    [SerializeField] private float warningTime = 1.5f;
    [SerializeField] private float flashSpeed = 0.1f;

    private bool isShieldActive = false;
    private bool isOnCooldown = false;
    private float shieldTimeLeft = 0f;
    private float cooldownTimeLeft = 0f;
    private bool isFlashing = false;
    private SpriteRenderer shieldRenderer;

    private void Start()
    {
        shieldObject.SetActive(false);
        shieldRenderer = shieldObject.GetComponent<SpriteRenderer>();

        if (shieldRenderer == null && shieldObject != null)
        {
            shieldRenderer = shieldObject.GetComponentInChildren<SpriteRenderer>();
        }

        // Đổi màu khởi tạo thành vàng
        if (cooldownFillImage != null)
        {
            cooldownFillImage.fillAmount = 1f; // Fill đầy khi bắt đầu
            cooldownFillImage.color = Color.yellow; // Màu vàng khi sẵn sàng
        }

        if (cooldownText != null)
        {
            cooldownText.text = ""; // Ẩn text khi bắt đầu
        }
    }




    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && !isShieldActive && !isOnCooldown)
        {
            ActivateShield();
        }

        if (isShieldActive)
        {
            shieldTimeLeft -= Time.deltaTime;
            UpdateUI();

            if (shieldTimeLeft <= warningTime && !isFlashing)
            {
                StartFlashing();
            }

            if (shieldTimeLeft <= 0)
            {
                DeactivateShield();
            }
        }

        if (isOnCooldown)
        {
            cooldownTimeLeft -= Time.deltaTime;
            UpdateUI();

            if (cooldownTimeLeft <= 0)
            {
                isOnCooldown = false;
                cooldownTimeLeft = 0;  // Đảm bảo giá trị không bị âm
                UpdateUI(); // Cập nhật UI khi hồi chiêu kết thúc
            }
        }
    }


    private void ActivateShield()
    {
        isShieldActive = true;
        isFlashing = false;
        shieldTimeLeft = shieldDuration;
        shieldObject.SetActive(true);
        SetShieldAlpha(1f);
        StopFlashing();
    }

    private void DeactivateShield()
    {
        isShieldActive = false;
        isFlashing = false;
        shieldObject.SetActive(false);

        if (IsInvoking(nameof(FlashShield)))
        {
            CancelInvoke(nameof(FlashShield));
        }

        // Bắt đầu hồi chiêu
        isOnCooldown = true;
        cooldownTimeLeft = shieldCooldown;

        // Cập nhật giao diện ngay khi khiên biến mất
        UpdateUI();
    }


    private void StartFlashing()
    {
        isFlashing = true;
        InvokeRepeating(nameof(FlashShield), 0f, flashSpeed);
    }

    private void StopFlashing()
    {
        if (IsInvoking(nameof(FlashShield)))
        {
            CancelInvoke(nameof(FlashShield));
        }
        isFlashing = false;
    }

    private void FlashShield()
    {
        if (shieldRenderer != null)
        {
            float newAlpha = shieldRenderer.color.a > 0.5f ? 0.3f : 1f;
            SetShieldAlpha(newAlpha);
        }
        else
        {
            shieldObject.SetActive(!shieldObject.activeSelf);
        }
    }

    private void SetShieldAlpha(float alpha)
    {
        if (shieldRenderer != null)
        {
            Color color = shieldRenderer.color;
            color.a = alpha;
            shieldRenderer.color = color;
        }
    }

    private void UpdateUI()
    {
        if (cooldownText != null)
        {
            if (isOnCooldown)
            {
                cooldownText.text = Mathf.CeilToInt(cooldownTimeLeft) + "s"; // Hiển thị số giây nguyên
                cooldownText.color = Color.red; // Đổi thành màu đỏ khi hồi chiêu
            }
            else
            {
                cooldownText.text = ""; // Ẩn khiên khi không hồi chiêu
            }
        }

        if (cooldownFillImage != null)
        {
            if (isShieldActive)
            {
                cooldownFillImage.fillAmount = shieldTimeLeft / shieldDuration; // Giảm dần theo thời gian còn lại của khiên
                cooldownFillImage.color = Color.yellow; // Màu vàng khi khiên đang bật
            }
            else if (isOnCooldown)
            {
                cooldownFillImage.fillAmount = cooldownTimeLeft / shieldCooldown; // Giảm dần theo thời gian hồi chiêu
                cooldownFillImage.color = Color.red; // Màu đỏ khi hồi chiêu
            }
            else
            {
                cooldownFillImage.fillAmount = 1f; // Fill đầy khi hồi chiêu xong
                cooldownFillImage.color = Color.yellow; // Màu vàng khi sẵn sàng
            }
        }
    }






    public bool IsShieldActive()
    {
        return isShieldActive;
    }
}
