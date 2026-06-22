using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;
    public float invincibilityTime = 1f;
    private float invincibilityTimer = 0f;

    void Start()
    {
        if (UpgradeManager.instance != null)
            maxHealth = 5 + (UpgradeManager.instance.healthLevel - 1) * 2;

        currentHealth = PlayerPrefs.GetInt("CurrentHealth", maxHealth);

        if (currentHealth <= 0)
            currentHealth = maxHealth;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    void Update()
    {
        if (invincibilityTimer > 0)
            invincibilityTimer -= Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        if (invincibilityTimer > 0) return;
        currentHealth -= damage;
        invincibilityTimer = invincibilityTime;

        PlayerPrefs.SetInt("CurrentHealth", currentHealth);
        PlayerPrefs.Save();

        Debug.Log("Player health: " + currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            if (GameOver.instance != null)
                GameOver.instance.ShowGameOver();
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        PlayerPrefs.SetInt("CurrentHealth", currentHealth);
        PlayerPrefs.Save();
        Debug.Log("Healed to: " + currentHealth);
    }
}