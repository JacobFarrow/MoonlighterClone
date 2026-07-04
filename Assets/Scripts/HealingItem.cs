using UnityEngine;

public class HealingItem : MonoBehaviour
{
    public int healAmount = 2;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                if (AudioManager.instance != null)
                    AudioManager.instance.PlayPickup();

                health.Heal(healAmount);
                Destroy(gameObject);
            }
        }
    }
}