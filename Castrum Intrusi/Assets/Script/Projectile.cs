using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 3f;
    public int damage = 2;

    private playerStats playerStats;
    
    private void Start()
    {
        Destroy(gameObject, lifetime);

        // On récupère le player automatiquement
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            playerStats = player.GetComponent<playerStats>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 🟡 Dégâts au PLAYER si on touche un Enemy
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("🔥 Player hit by an enemy projectile!");
            if (playerStats != null)
                playerStats.health -= damage;

            Destroy(gameObject);
            return;
        }


        // 🔴 Dégâts aux Intrusi si on touche un Intrusi
        if (other.CompareTag("Intrusis"))
        {
            Debug.Log("💥 Intrusi hit!");
            MobStats mob = other.GetComponent<MobStats>();
            if (mob != null)
                mob.TakeDamage();

            Destroy(gameObject);
            return;
        }


        // ❌ Si on touche un obstacle, on détruit juste le projectile
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Debug.Log("🧱 Projectile destroyed by obstacle");
            Destroy(gameObject);
        }
    }
}
