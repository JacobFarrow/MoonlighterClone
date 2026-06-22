using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI upgradeText;

    private PlayerHealth playerHealth;
    private float refreshTimer = 0f;

    void Start()
    {
        // Wait a moment before first read to let DontDestroyOnLoad objects settle
        Invoke("FindReferences", 0.1f);
    }

    void FindReferences()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            playerHealth = player.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (playerHealth == null)
            FindReferences();

        if (playerHealth != null && healthText != null)
            healthText.text = "Health: " + playerHealth.currentHealth + "/" + playerHealth.maxHealth;

        if (Inventory.instance != null && goldText != null)
            goldText.text = "Gold: " + Inventory.instance.gold;

        if (upgradeText != null)
        {
            if (UpgradeManager.instance != null)
                upgradeText.text = "ATK Lv" + UpgradeManager.instance.attackLevel +
                                 " | SPD Lv" + UpgradeManager.instance.speedLevel +
                                 " | HP Lv" + UpgradeManager.instance.healthLevel;
            else
                upgradeText.text = "";
        }
    }
}