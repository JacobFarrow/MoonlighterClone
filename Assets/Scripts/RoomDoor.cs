using UnityEngine;

public class RoomDoor : MonoBehaviour
{
    public Vector2 teleportTarget;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = teleportTarget;
        }
    }
}