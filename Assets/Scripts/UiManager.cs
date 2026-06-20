using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI upgradeText;

    private PlayerHealth playerHealth;

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            playerHealth = player.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (playerHealth != null)
            healthText.text = "Health: " + playerHealth.currentHealth + "/" + playerHealth.maxHealth;

        if (Inventory.instance != null)
            goldText.text = "Gold: " + Inventory.instance.gold;

        if (UpgradeManager.instance != null && upgradeText != null)
            upgradeText.text = "ATK Lv" + UpgradeManager.instance.attackLevel +
                             " | SPD Lv" + UpgradeManager.instance.speedLevel +
                             " | HP Lv" + UpgradeManager.instance.healthLevel;
    }
}