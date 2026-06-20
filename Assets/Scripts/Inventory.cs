using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public List<string> itemNames = new List<string>();
    public List<int> itemValues = new List<int>();
    public int gold = 0;

    void Awake()
    {
        instance = this;
        Load();
    }

    void OnDestroy()
    {
        Save();
    }

    public void AddItem(string name, int value)
    {
        itemNames.Add(name);
        itemValues.Add(value);
        Save();
    }

    public void SellItem(int index)
    {
        if (index < 0 || index >= itemNames.Count) return;
        gold += itemValues[index];
        itemNames.RemoveAt(index);
        itemValues.RemoveAt(index);
        Save();
    }

    void Save()
    {
        PlayerPrefs.SetInt("Gold", gold);
        PlayerPrefs.SetInt("ItemCount", itemNames.Count);
        for (int i = 0; i < itemNames.Count; i++)
        {
            PlayerPrefs.SetString("ItemName" + i, itemNames[i]);
            PlayerPrefs.SetInt("ItemValue" + i, itemValues[i]);
        }
        PlayerPrefs.Save();
    }

    void Load()
    {
        gold = PlayerPrefs.GetInt("Gold", 0);
        int count = PlayerPrefs.GetInt("ItemCount", 0);
        itemNames.Clear();
        itemValues.Clear();
        for (int i = 0; i < count; i++)
        {
            itemNames.Add(PlayerPrefs.GetString("ItemName" + i, ""));
            itemValues.Add(PlayerPrefs.GetInt("ItemValue" + i, 0));
        }
    }
}