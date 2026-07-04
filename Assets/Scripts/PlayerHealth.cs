using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;
    public float invincibilityTime = 1f;
    private float invincibilityTimer = 0f;

    [Header("Hit Flash")]
    public Color flashColor = Color.red;
    public float flashInterval = 0.1f;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Coroutine flashRoutine;

    void Start()
    {
        if (UpgradeManager.instance != null)
            maxHealth = 5 + (UpgradeManager.instance.healthLevel - 1) * 2;

        currentHealth = PlayerPrefs.GetInt("CurrentHealth", maxHealth);

        if (currentHealth <= 0)
            currentHealth = maxHealth;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
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

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            if (GameOver.instance != null)
                GameOver.instance.ShowGameOver();
            return;
        }

        if (flashRoutine != null)
            StopCoroutine(flashRoutine);
        flashRoutine = StartCoroutine(InvincibilityFlash());
    }

    IEnumerator InvincibilityFlash()
    {
        while (invincibilityTimer > 0)
        {
            spriteRenderer.color = flashColor;
            yield return new WaitForSeconds(flashInterval);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(flashInterval);
        }
        spriteRenderer.color = originalColor;
        flashRoutine = null;
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        PlayerPrefs.SetInt("CurrentHealth", currentHealth);
        PlayerPrefs.Save();
    }
}