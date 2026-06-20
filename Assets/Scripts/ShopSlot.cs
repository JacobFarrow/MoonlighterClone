using UnityEngine;
using TMPro;

public class ShopSlot : MonoBehaviour
{
    public TextMeshPro priceText;
    public int price = 10;
    public bool hasItem = false;
    public string itemName = "";

    private bool playerNearby = false;

    void Start()
    {
        UpdateDisplay();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerNearby = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerNearby = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerNearby)
        {
            Inventory inv = Inventory.instance;
            if (inv == null) return;

            if (!hasItem && inv.itemNames.Count > 0)
            {
                itemName = inv.itemNames[0];
                price = inv.itemValues[0];
                inv.itemNames.RemoveAt(0);
                inv.itemValues.RemoveAt(0);
                hasItem = true;

                // Save to PlayerPrefs so it persists
                PlayerPrefs.SetString("SlotItemName", itemName);
                PlayerPrefs.SetInt("SlotItemPrice", price);
                PlayerPrefs.SetInt("SlotHasItem", 1);
                PlayerPrefs.Save();

                UpdateDisplay();
                Debug.Log("Placed " + itemName + " on counter");
            }
            else if (hasItem)
            {
                inv.itemNames.Insert(0, itemName);
                inv.itemValues.Insert(0, price);
                hasItem = false;
                itemName = "";

                // Clear saved slot
                PlayerPrefs.SetInt("SlotHasItem", 0);
                PlayerPrefs.Save();

                UpdateDisplay();
            }
        }
    }

    public void SellItem()
    {
        if (!hasItem) return;
        if (Inventory.instance != null)
            Inventory.instance.gold += price;
        Debug.Log("Sold " + itemName + " for " + price + " gold!");
        hasItem = false;
        itemName = "";
        PlayerPrefs.SetInt("SlotHasItem", 0);
        PlayerPrefs.Save();
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        if (priceText != null)
            priceText.text = hasItem ? itemName + "\n" + price + "g" : "Empty";
    }

    // Load saved state when scene loads
    void OnEnable()
    {
        if (PlayerPrefs.GetInt("SlotHasItem", 0) == 1)
        {
            itemName = PlayerPrefs.GetString("SlotItemName", "");
            price = PlayerPrefs.GetInt("SlotItemPrice", 0);
            hasItem = true;
            UpdateDisplay();
        }
    }
}