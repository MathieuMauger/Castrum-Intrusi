using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // vitesse max
    private Rigidbody2D rb;
    public Animator animator;


    public Vector2 moveDirection {  get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;        // pas de gravité
        rb.linearDamping = 10f;               // freine automatiquement
        rb.angularDamping = 0f;
        rb.freezeRotation = true;    // empêche la rotation
        animator = GetComponent<Animator>();
    }


    private void FixedUpdate()
    {

        moveDirection = KeyboardMovement();
        rb.linearVelocity = moveDirection * moveSpeed;

        UpdateAnimation(moveDirection);

    }

    private void Update()
    {
        Debug.Log($"X = {moveDirection.x}, Y = {moveDirection.y}");
    }

    private void UpdateAnimation(Vector2 dir)
    {
        animator.SetFloat("X", dir.x);
        animator.SetFloat("Y", dir.y);
        animator.SetFloat("Speed", dir.magnitude);
    }


    private Vector2 KeyboardMovement()
    {
        Vector2 move = Vector2.zero;

        if (Keyboard.current.wKey.isPressed) move += Vector2.up;
        if (Keyboard.current.sKey.isPressed) move += Vector2.down;
        if (Keyboard.current.dKey.isPressed) move += Vector2.right;
        if (Keyboard.current.aKey.isPressed) move += Vector2.left;

        return move.normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            CircleCollider2D enemyCircle = collision.GetComponent<CircleCollider2D>();

            if (enemyCircle != null)
            {
                Debug.Log("Trigger");
            }
        }
    }
}
