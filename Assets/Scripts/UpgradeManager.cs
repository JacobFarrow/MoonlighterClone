using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;

    [Header("Attack Upgrades")]
    public int attackLevel = 1;
    public int attackUpgradeCost = 50;

    [Header("Speed Upgrades")]
    public int speedLevel = 1;
    public int speedUpgradeCost = 30;

    [Header("Health Upgrades")]
    public int healthLevel = 1;
    public int healthUpgradeCost = 40;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Load();
        }
        else
        {
            // Before destroying, make sure the existing instance has latest data
            instance.Load();
            Destroy(gameObject);
        }
    }

    public bool UpgradeAttack()
    {
        if (Inventory.instance.gold >= attackUpgradeCost)
        {
            Inventory.instance.gold -= attackUpgradeCost;
            attackLevel++;
            attackUpgradeCost = Mathf.RoundToInt(attackUpgradeCost * 1.5f);
            ApplyUpgrades();
            Save();
            return true;
        }
        Debug.Log("Not enough gold!");
        return false;
    }

    public bool UpgradeSpeed()
    {
        if (Inventory.instance.gold >= speedUpgradeCost)
        {
            Inventory.instance.gold -= speedUpgradeCost;
            speedLevel++;
            speedUpgradeCost = Mathf.RoundToInt(speedUpgradeCost * 1.5f);
            ApplyUpgrades();
            Save();
            return true;
        }
        Debug.Log("Not enough gold!");
        return false;
    }

    public bool UpgradeHealth()
    {
        if (Inventory.instance.gold >= healthUpgradeCost)
        {
            Inventory.instance.gold -= healthUpgradeCost;
            healthLevel++;
            healthUpgradeCost = Mathf.RoundToInt(healthUpgradeCost * 1.5f);
            ApplyUpgrades();
            Save();
            return true;
        }
        Debug.Log("Not enough gold!");
        return false;
    }

    public void ApplyUpgrades()
    {
        PlayerController pc = FindAnyObjectByType<PlayerController>();
        PlayerHealth ph = FindAnyObjectByType<PlayerHealth>();

        if (pc != null)
        {
            pc.attackDamage = attackLevel;
            pc.moveSpeed = 5f + (speedLevel - 1) * 1.5f;
        }

        if (ph != null)
        {
            ph.maxHealth = 5 + (healthLevel - 1) * 2;
            if (ph.currentHealth > ph.maxHealth)
                ph.currentHealth = ph.maxHealth;
            PlayerPrefs.SetInt("CurrentHealth", ph.currentHealth);
            PlayerPrefs.Save();
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt("AttackLevel", attackLevel);
        PlayerPrefs.SetInt("AttackCost", attackUpgradeCost);
        PlayerPrefs.SetInt("SpeedLevel", speedLevel);
        PlayerPrefs.SetInt("SpeedCost", speedUpgradeCost);
        PlayerPrefs.SetInt("HealthLevel", healthLevel);
        PlayerPrefs.SetInt("HealthCost", healthUpgradeCost);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        attackLevel = PlayerPrefs.GetInt("AttackLevel", 1);
        attackUpgradeCost = PlayerPrefs.GetInt("AttackCost", 50);
        speedLevel = PlayerPrefs.GetInt("SpeedLevel", 1);
        speedUpgradeCost = PlayerPrefs.GetInt("SpeedCost", 30);
        healthLevel = PlayerPrefs.GetInt("HealthLevel", 1);
        healthUpgradeCost = PlayerPrefs.GetInt("HealthCost", 40);
    }
}