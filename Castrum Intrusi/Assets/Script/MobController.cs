using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MobController : MonoBehaviour
{
    [Header("References")]
    public Animator animator;       // si vide on essayera GetComponentInChildren
    public Transform player;        // assigné via trigger ou inspector

    [Header("Settings")]
    public float speed = 4f;
    public float attackDistance = 1.8f;
    public float detectionRange = 5f;

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

    void MoveTowardPlayer()
    {
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
        SafeSetFloat(hashSpeed, 0f);
        SafeSetTrigger(hashAttack);
    }

    void UpdateDirectionAnimation(Vector2 dir)
    {
        SafeSetFloat(hashX, dir.x);
        SafeSetFloat(hashY, dir.y);

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            transform.localScale = new Vector3(dir.x < 0 ? -1 : 1, 1, 1);
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
