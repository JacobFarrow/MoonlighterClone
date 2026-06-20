using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI goldText;

    private PlayerHealth playerHealth;
    private Inventory inventory;

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
            inventory = player.GetComponent<Inventory>();
        }
    }

    void Update()
    {
        if (playerHealth != null)
            healthText.text = "Health: " + playerHealth.currentHealth + "/" + playerHealth.maxHealth;

        if (inventory != null)
            goldText.text = "Gold: " + inventory.gold;
    }
}