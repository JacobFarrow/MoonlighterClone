using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance;

    public GameObject inventoryPanel;
    public Transform itemListParent;
    public GameObject itemRowPrefab;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI totalItemsText;

    private bool isOpen = false;

    void Awake()
    {
        instance = this;
        inventoryPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isOpen) CloseInventory();
            else OpenInventory();
        }
    }

    public void OpenInventory()
    {
        isOpen = true;
        inventoryPanel.SetActive(true);
        Time.timeScale = 0f;
        RefreshInventory();
    }

    public void CloseInventory()
    {
        isOpen = false;
        inventoryPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    void RefreshInventory()
    {
        // Clear existing rows
        foreach (Transform child in itemListParent)
            Destroy(child.gameObject);

        if (Inventory.instance == null) return;

        // Update gold and item count
        goldText.text = "Gold: " + Inventory.instance.gold + "g";
        totalItemsText.text = "Items: " + Inventory.instance.itemNames.Count;

        // Create a row for each item
        for (int i = 0; i < Inventory.instance.itemNames.Count; i++)
        {
            GameObject row = Instantiate(itemRowPrefab, itemListParent);
            
            // Find the children explicitly by name with safety checks
            Transform nameTrans = row.transform.Find("NameText");
            Transform rarityTrans = row.transform.Find("RarityText");
            Transform valueTrans = row.transform.Find("ValueText");

            if (nameTrans != null && rarityTrans != null && valueTrans != null)
            {
                TextMeshProUGUI nameText = nameTrans.GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI rarityText = rarityTrans.GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI valueText = valueTrans.GetComponent<TextMeshProUGUI>();

                string rarityColor = GetRarityColor(Inventory.instance.itemRarities[i]);
                
                nameText.text = "<color=" + rarityColor + ">" + Inventory.instance.itemNames[i] + "</color>";
                rarityText.text = Inventory.instance.itemRarities[i].ToString();
                rarityText.color = GetRarityUnityColor(Inventory.instance.itemRarities[i]);
                valueText.text = Inventory.instance.itemValues[i].ToString() + "g";
            }
            else
            {
                Debug.LogError("InventoryUI: Could not find child text objects in ItemRow prefab! Ensure they are named correctly.");
            }
        }
    }

    string GetRarityColor(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Common:   return "white";
            case Rarity.Uncommon: return "green";
            case Rarity.Rare:     return "cyan";
            case Rarity.Epic:     return "purple";
            default: return "white";
        }
    }

    Color GetRarityUnityColor(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Common:   return Color.white;
            case Rarity.Uncommon: return Color.green;
            case Rarity.Rare:     return Color.cyan;
            case Rarity.Epic:     return new Color(0.5f, 0f, 0.5f);
            default: return Color.white;
        }
    }
}