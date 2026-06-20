using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public int enemyLevel = 1; // Higher level = better loot chance

    [System.Serializable]
    public class LootEntry
    {
        public GameObject prefab;
        public Rarity rarity;
        public int weight = 100; // Higher = more common
    }

    public LootEntry[] lootTable;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            DropLoot();
            Destroy(gameObject);
        }
    }

    void DropLoot()
    {
        if (lootTable.Length == 0) return;

        // Calculate total weight, boosted by enemy level for rarer items
        int totalWeight = 0;
        foreach (LootEntry entry in lootTable)
        {
            int adjustedWeight = GetAdjustedWeight(entry);
            totalWeight += adjustedWeight;
        }

        // Pick random item based on weight
        int roll = Random.Range(0, totalWeight);
        int cumulative = 0;

        foreach (LootEntry entry in lootTable)
        {
            cumulative += GetAdjustedWeight(entry);
            if (roll < cumulative)
            {
                Instantiate(entry.prefab, transform.position, Quaternion.identity);
                Debug.Log("Dropped: " + entry.rarity + " item");
                return;
            }
        }
    }

    int GetAdjustedWeight(LootEntry entry)
    {
        // Higher level enemies boost rare item chances
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