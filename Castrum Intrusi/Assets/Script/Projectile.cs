using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 3f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Projectile touched: " + other.name);

        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") || other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
