using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 2f;
    public float detectionRange = 5f;
    public int damage = 1;
    public float attackCooldown = 1f;

    private Rigidbody2D rb;
    private float attackTimer = 0f;

    void Start()
{
    rb = GetComponent<Rigidbody2D>();
    // Find the player automatically instead of relying on the reference
    player = GameObject.FindWithTag("Player").transform;
}

    void Update()
    {
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
            if (health != null && attackTimer <= 0)
            {
                health.TakeDamage(damage);
                attackTimer = attackCooldown;
            }
        }
    }
}