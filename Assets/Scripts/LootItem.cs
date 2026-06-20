using UnityEngine;

public class LootItem : MonoBehaviour
{
    public string itemName = "Unknown Item";
    public int value = 10;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Add to inventory
            Inventory inventory = other.GetComponent<Inventory>();
            if (inventory != null)
            {
                inventory.AddItem(itemName, value);
                Debug.Log("Picked up: " + itemName + " worth " + value + " gold");
                Destroy(gameObject);
            }
        }
    }
}