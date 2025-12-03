using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float fireRate = 0.3f;
    public float projectileSpeed = 10f;

    private float fireTimer;

    void Update()
    {
        fireTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && fireTimer <= 0f)
        {
            Debug.Log("Sarrko");
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 direction = (mousePos - transform.position).normalized;

            FireProjectile(direction);
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
