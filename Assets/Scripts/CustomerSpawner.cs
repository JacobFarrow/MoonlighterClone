using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;
    public float spawnInterval = 10f;
    private float spawnTimer = 3f;

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnCustomer();
            spawnTimer = spawnInterval;
        }
    }

    void SpawnCustomer()
    {
        if (customerPrefab != null)
            Instantiate(customerPrefab, new Vector3(0, -4, 0), Quaternion.identity);
    }
}