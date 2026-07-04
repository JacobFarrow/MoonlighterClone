using UnityEngine;

public class EnemyRanged : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float detectionRange = 7f;
    public float preferredDistance = 4f;
    public float moveSpeed = 1.5f;
    public float fireCooldown = 2f;

    private Transform player;
    private Rigidbody2D rb;
    private float fireTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (fireTimer > 0)
            fireTimer -= Time.deltaTime;

        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < detectionRange && fireTimer <= 0)
        {
            Shoot();
            fireTimer = fireCooldown;
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance >= detectionRange) return;

        Vector2 direction = (player.position - transform.position).normalized;

        if (distance < preferredDistance - 0.5f)
        {
            rb.MovePosition(rb.position - direction * moveSpeed * Time.fixedDeltaTime);
        }
        else if (distance > preferredDistance + 0.5f)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void Shoot()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        proj.GetComponent<EnemyProjectile>().SetDirection(direction);
    }
}