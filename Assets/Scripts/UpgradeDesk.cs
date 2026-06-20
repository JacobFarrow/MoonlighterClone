using UnityEngine;

public class UpgradeDesk : MonoBehaviour
{
    private bool playerNearby = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            Debug.Log("Press 1 to upgrade Attack (" + UpgradeManager.instance.attackUpgradeCost + "g)");
            Debug.Log("Press 2 to upgrade Speed (" + UpgradeManager.instance.speedUpgradeCost + "g)");
            Debug.Log("Press 3 to upgrade Health (" + UpgradeManager.instance.healthUpgradeCost + "g)");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerNearby = false;
    }

    void Update()
    {
        if (!playerNearby) return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
            UpgradeManager.instance.UpgradeAttack();

        if (Input.GetKeyDown(KeyCode.Alpha2))
            UpgradeManager.instance.UpgradeSpeed();

        if (Input.GetKeyDown(KeyCode.Alpha3))
            UpgradeManager.instance.UpgradeHealth();
    }
}