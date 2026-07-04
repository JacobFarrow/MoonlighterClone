using UnityEngine;

public class LootItem : MonoBehaviour
{
    public string itemName = "Unknown Item";
    public int value = 10;
    public Rarity rarity = Rarity.Common;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory inventory = other.GetComponent<Inventory>();
            if (inventory != null)
            {
                if (AudioManager.instance != null)
                    AudioManager.instance.PlayPickup();

                inventory.AddItem(itemName, value, rarity);
                Destroy(gameObject);
            }
        }
    }
}

public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Epic
}