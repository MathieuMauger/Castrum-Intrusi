using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float fireRate = 0.3f;
    public float projectileSpeed = 10f;

    private float fireTimer;
    private PlayerMovement movement;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        fireTimer -= Time.deltaTime;

        // Si le joueur bouge -> tirer automatiquement
        if (movement.moveDirection != Vector2.zero && fireTimer <= 0f)
        {
            FireProjectile(movement.moveDirection);
            fireTimer = fireRate;
        }
    }

    void FireProjectile(Vector2 direction)
    {
        Vector3 spawnPos = transform.position + (Vector3)direction * 0.5f;
        GameObject proj = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);

        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * projectileSpeed;
    }

}
