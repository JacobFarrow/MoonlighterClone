using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public int enemyLevel = 1;

    [Header("Hit Flash")]
    public Color flashColor = Color.white;
    public float flashDuration = 0.1f;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Coroutine flashRoutine;

    [System.Serializable]
    public class LootEntry
    {
        public GameObject prefab;
        public Rarity rarity;
        public int weight = 100;
    }

    public LootEntry[] lootTable;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void TakeDamage(int damage)
    {
        if (AudioManager.instance != null)
            AudioManager.instance.PlayHit();

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            DropLoot();
            Destroy(gameObject);
            return;
        }

        if (flashRoutine != null)
            StopCoroutine(flashRoutine);
        flashRoutine = StartCoroutine(HitFlash());
    }

    IEnumerator HitFlash()
    {
        spriteRenderer.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
        flashRoutine = null;
    }

    void DropLoot()
    {
        if (lootTable.Length == 0) return;
        int totalWeight = 0;
        foreach (LootEntry entry in lootTable)
            totalWeight += GetAdjustedWeight(entry);

        int roll = Random.Range(0, totalWeight);
        int cumulative = 0;
        foreach (LootEntry entry in lootTable)
        {
            cumulative += GetAdjustedWeight(entry);
            if (roll < cumulative)
            {
                Instantiate(entry.prefab, transform.position, Quaternion.identity);
                return;
            }
        }
    }

    int GetAdjustedWeight(LootEntry entry)
    {
        switch (entry.rarity)
        {
            case Rarity.Common:   return entry.weight;
            case Rarity.Uncommon: return entry.weight + (enemyLevel * 5);
            case Rarity.Rare:     return entry.weight + (enemyLevel * 10);
            case Rarity.Epic:     return entry.weight + (enemyLevel * 20);
            default: return entry.weight;
        }
    }
}