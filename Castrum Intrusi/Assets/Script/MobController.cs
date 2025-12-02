using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MobController : MonoBehaviour
{
    [Header("References")]
    public Animator animator;
    public Transform player;

    [Header("Settings")]
    public float speed = 8f;
    public float attackDistance = 0.8f;
    public float detectionRange = 10f;

    Rigidbody2D rb;
    Vector2 moveDir;

    // Hash des paramètres (optionnel mais propre)
    int hashSpeed = Animator.StringToHash("Speed");
    int hashX = Animator.StringToHash("X");
    int hashY = Animator.StringToHash("Y");
    int hashAttack = Animator.StringToHash("Attack");

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (player == null)
        {
            SafeSetFloat(hashSpeed, 0f);
            return;
        }

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            if (distance > attackDistance)
            {
                
                MoveTowardPlayer();
            }
            else
            {

                StartAttack();
            }
        }
        else
        {
            SafeSetFloat(hashSpeed, 0f);
        }
    }

    public void TakeDamage()
    {
        if (animator != null)
            animator.SetTrigger("Hit");
    }

    public void Die()
    {
        if (animator != null)
            animator.SetTrigger("Death");
        rb.linearVelocity = Vector2.zero;
        this.enabled = false;
    }

    void MoveTowardPlayer()
    {

        print("Déplacement vers le joueur");
        moveDir = (player.position - transform.position).normalized;

        // Animation de marche
        SafeSetFloat(hashSpeed, 1f);

        // Déplacement avec Rigidbody2D (physique propre)
        Vector2 newPos = Vector2.MoveTowards(rb.position, (Vector2)player.position, speed * Time.deltaTime);
        rb.MovePosition(newPos);

        UpdateDirectionAnimation(moveDir);
    }

    void StartAttack()
    {
        print("Distance d'attaque");
        SafeSetFloat(hashSpeed, 0f);
        SafeSetTrigger(hashAttack);
    }

    void UpdateDirectionAnimation(Vector2 dir)
    {
        SafeSetFloat(hashX, dir.x);
        SafeSetFloat(hashY, dir.y);

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            Vector3 s = transform.localScale;
            s.x = dir.x < 0 ? -Mathf.Abs(s.x) : Mathf.Abs(s.x);
            transform.localScale = s;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            player = collision.transform;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.transform == player)
            player = null;
    }

    bool HasParameter(int hash)
    {
        if (animator == null) return false;

        var pars = animator.parameters;
        for (int i = 0; i < pars.Length; i++)
            if (pars[i].nameHash == hash) return true;
        return false;
    }

    void SafeSetFloat(int hash, float value)
    {
        if (animator == null) return;
        if (HasParameter(hash)) animator.SetFloat(hash, value);
    }

    void SafeSetTrigger(int hash)
    {
        if (animator == null) return;
        if (HasParameter(hash)) animator.SetTrigger(hash);
    }
}
