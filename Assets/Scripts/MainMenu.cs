using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject continueButton;
    public Button newGameButton;
    public Button continueGameButton;
    public Button quitButton;

    void Start()
    {
        // Link buttons via script instead of Inspector
        newGameButton.onClick.AddListener(NewGame);
        continueGameButton.onClick.AddListener(ContinueGame);
        quitButton.onClick.AddListener(QuitGame);

        // Only show continue if a save exists
        if (PlayerPrefs.GetInt("HasSave", 0) == 1)
            continueButton.SetActive(true);
        else
            continueButton.SetActive(false);
    }

    void NewGame()
    {
        PlayerPrefs.SetInt("Gold", 0);
        PlayerPrefs.SetInt("ItemCount", 0);
        PlayerPrefs.SetInt("CurrentHealth", 0);
        PlayerPrefs.SetInt("AttackLevel", 1);
        PlayerPrefs.SetInt("AttackCost", 50);
        PlayerPrefs.SetInt("SpeedLevel", 1);
        PlayerPrefs.SetInt("SpeedCost", 30);
        PlayerPrefs.SetInt("HealthLevel", 1);
        PlayerPrefs.SetInt("HealthCost", 40);
        PlayerPrefs.SetInt("SlotHasItem", 0);
        PlayerPrefs.SetInt("HasSave", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("DungeonScene");
    }

    void ContinueGame()
    {
        SceneManager.LoadScene("DungeonScene");
    }

    void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
}