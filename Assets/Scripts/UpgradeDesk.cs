using UnityEngine;

public class UpgradeDesk : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (UpgradeUI.instance != null)
                UpgradeUI.instance.PlayerApproached();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (UpgradeUI.instance != null)
                UpgradeUI.instance.PlayerLeft();
        }
    }
}