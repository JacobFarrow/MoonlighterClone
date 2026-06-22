using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance;

    public GameObject inventoryPanel;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI totalItemsText;
    public Transform itemListParent;

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

        goldText.text = "Gold: " + Inventory.instance.gold + "g";
        totalItemsText.text = "Items: " + Inventory.instance.itemNames.Count;

        for (int i = 0; i < Inventory.instance.itemNames.Count; i++)
        {
            // Create row
            GameObject row = new GameObject("Row_" + i, typeof(RectTransform));
            row.transform.SetParent(itemListParent, false);

            RectTransform rowRect = row.GetComponent<RectTransform>();
            rowRect.sizeDelta = new Vector2(520, 50);

            // Add horizontal layout
            HorizontalLayoutGroup hlg = row.AddComponent<HorizontalLayoutGroup>();
            hlg.spacing = 10;
            hlg.childForceExpandWidth = false;
            hlg.childForceExpandHeight = true;
            hlg.padding = new RectOffset(10, 10, 5, 5);

            // Add background
            Image bg = row.AddComponent<Image>();
            bg.color = new Color(0, 0, 0, 0.3f);

            Rarity rarity = Inventory.instance.itemRarities[i];
            Color rarityColor = GetRarityColor(rarity);

            // Name
            CreateText(row.transform, Inventory.instance.itemNames[i], rarityColor, 200, TextAlignmentOptions.Left);
            // Rarity
            CreateText(row.transform, rarity.ToString(), rarityColor, 150, TextAlignmentOptions.Center);
            // Value
            CreateText(row.transform, Inventory.instance.itemValues[i] + "g", Color.white, 100, TextAlignmentOptions.Right);
        }
    }

    void CreateText(Transform parent, string text, Color color, float width, TextAlignmentOptions alignment)
    {
        GameObject obj = new GameObject("Text", typeof(RectTransform));
        obj.transform.SetParent(parent, false);

        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(width, 40);

        // Add layout element
        LayoutElement le = obj.AddComponent<LayoutElement>();
        le.preferredWidth = width;
        le.preferredHeight = 40;

        TextMeshProUGUI tmp = obj.AddComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.color = color;
        tmp.fontSize = 16;
        tmp.alignment = alignment;
        tmp.textWrappingMode = TextWrappingModes.NoWrap;
        tmp.overflowMode = TextOverflowModes.Overflow;
    }

    Color GetRarityColor(Rarity rarity)
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