using UnityEngine;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    public static UpgradeUI instance;

    public GameObject upgradePanel;
    public TextMeshProUGUI goldText;

    // Attack
    public TextMeshProUGUI attackLevelText;
    public TextMeshProUGUI attackCostText;
    public TextMeshProUGUI attackStatText;

    // Speed
    public TextMeshProUGUI speedLevelText;
    public TextMeshProUGUI speedCostText;
    public TextMeshProUGUI speedStatText;

    // Health
    public TextMeshProUGUI healthLevelText;
    public TextMeshProUGUI healthCostText;
    public TextMeshProUGUI healthStatText;

    // Prompt
    public TextMeshProUGUI promptText;

    private bool isOpen = false;
    private bool playerAtDesk = false;

    void Awake()
    {
        instance = this;
        upgradePanel.SetActive(false);
        if (promptText != null)
            promptText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (playerAtDesk && Input.GetKeyDown(KeyCode.U))
        {
            if (isOpen) CloseUpgrades();
            else OpenUpgrades();
        }

        if (isOpen)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                UpgradeManager.instance.UpgradeAttack();
                RefreshUI();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                UpgradeManager.instance.UpgradeSpeed();
                RefreshUI();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                UpgradeManager.instance.UpgradeHealth();
                RefreshUI();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
                CloseUpgrades();
        }
    }

    public void PlayerApproached()
    {
        playerAtDesk = true;
        if (promptText != null)
        {
            promptText.gameObject.SetActive(true);
            promptText.text = "Press U to upgrade";
        }
    }

    public void PlayerLeft()
    {
        playerAtDesk = false;
        if (promptText != null)
            promptText.gameObject.SetActive(false);
        if (isOpen)
            CloseUpgrades();
    }

    public void OpenUpgrades()
    {
        isOpen = true;
        upgradePanel.SetActive(true);
        Time.timeScale = 0f;
        if (promptText != null)
            promptText.gameObject.SetActive(false);
        RefreshUI();
    }

    public void CloseUpgrades()
    {
        isOpen = false;
        upgradePanel.SetActive(false);
        Time.timeScale = 1f;
        if (promptText != null && playerAtDesk)
            promptText.gameObject.SetActive(true);
    }

    void RefreshUI()
    {
        if (Inventory.instance != null)
            goldText.text = "Gold: " + Inventory.instance.gold + "g";

        if (UpgradeManager.instance == null) return;

        attackLevelText.text = "Attack  Lv " + UpgradeManager.instance.attackLevel;
        attackStatText.text = "Damage: " + UpgradeManager.instance.attackLevel;
        attackCostText.text = "Cost: " + UpgradeManager.instance.attackUpgradeCost + "g  [Press 1]";

        speedLevelText.text = "Speed  Lv " + UpgradeManager.instance.speedLevel;
        speedStatText.text = "Speed: " + (5f + (UpgradeManager.instance.speedLevel - 1) * 1.5f).ToString("F1");
        speedCostText.text = "Cost: " + UpgradeManager.instance.speedUpgradeCost + "g  [Press 2]";

        healthLevelText.text = "Health  Lv " + UpgradeManager.instance.healthLevel;
        healthStatText.text = "Max HP: " + (5 + (UpgradeManager.instance.healthLevel - 1) * 2);
        healthCostText.text = "Cost: " + UpgradeManager.instance.healthUpgradeCost + "g  [Press 3]";
    }
}