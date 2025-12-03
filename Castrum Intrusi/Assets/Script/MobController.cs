using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MobController : MonoBehaviour
{
    [Header("References")]
    public Animator animator;
    public Transform player;
    public GameObject playerBody;
    private int health;

    [Header("Settings")]
    public float speed = 8f;
    //public float detectionRange = 10f;
    public float attackCooldown = 5f;

    [Header("Colliders")]
    public CircleCollider2D detectionCollider;
    public CircleCollider2D attackCollider;

    Rigidbody2D rb;
    Vector2 moveDir;

    bool canAttack = true;
    bool playerDetected = false;
    bool playerAttacked = false;


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

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            this.player = player.transform;
            playerBody = player;
        }
    }

    void Update()
    {
        if (player == null)
        {
            SafeSetFloat(hashSpeed, 0f);
            return;
        }

        float distance = Vector2.Distance(transform.position, player.position);

        float attackRange = attackCollider.radius;
        float detectionRange = detectionCollider.radius;

        if (distance <= detectionRange)
        {
            if (distance > attackRange)
            {
                MoveTowardPlayer();
            }
            else
            {
                if (canAttack)
                    StartAttack();
            }
        }
        else
        {
            SafeSetFloat(hashSpeed, 0f);
        }
    }

    async void StartAttack()
    {
        canAttack = false;

        health = playerBody.GetComponent<playerStats>().health -= 2;
        print("Player health: " + health);

        SafeSetFloat(hashSpeed, 0f);
        SafeSetTrigger(hashAttack);

        await Awaitable.WaitForSecondsAsync(attackCooldown);

        canAttack = true;
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
        moveDir = (player.position - transform.position).normalized;
        SafeSetFloat(hashSpeed, 1f);

        Vector2 newPos = Vector2.MoveTowards(rb.position, (Vector2)player.position, speed * Time.deltaTime);
        rb.MovePosition(newPos);

        UpdateDirectionAnimation(moveDir);
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
        if (!collision.CompareTag("Player")) return;

        if (collision == detectionCollider)
            playerDetected = true;

        if (collision == attackCollider)
            playerAttacked = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (collision == detectionCollider)
            playerDetected = false;

        if (collision == attackCollider)
            playerAttacked = false;
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