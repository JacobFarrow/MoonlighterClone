using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public static GameOver instance;

    public GameObject gameOverPanel;
    public TextMeshProUGUI statsText;

    void Awake()
    {
        instance = this;
        gameOverPanel.SetActive(false);
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;

        int gold = Inventory.instance != null ? Inventory.instance.gold : 0;
        statsText.text = "You collected " + gold + " gold";

        // Reset health and gold on death
        PlayerPrefs.SetInt("CurrentHealth", 0);

        // Clear inventory
        if (Inventory.instance != null)
        {
            Inventory.instance.itemNames.Clear();
            Inventory.instance.itemValues.Clear();
            Inventory.instance.itemRarities.Clear();
            Inventory.instance.gold = 0;
            PlayerPrefs.SetInt("Gold", 0);
            PlayerPrefs.SetInt("ItemCount", 0);
        }

        // Clear shop slot
        PlayerPrefs.SetInt("SlotHasItem", 0);

        PlayerPrefs.Save();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("CurrentHealth", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene("DungeonScene");
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("CurrentHealth", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene("DungeonScene");
    }
}