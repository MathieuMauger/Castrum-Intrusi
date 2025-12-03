using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 3f;
    public GameObject mobBody;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        MobStats mob = other.GetComponent<MobStats>();
        if (mob != null)
        {
            mob.TakeDamage();
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") || other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Debug.Log("Projectile touched: " + other.name);
            Destroy(gameObject);
        }
    }
}
