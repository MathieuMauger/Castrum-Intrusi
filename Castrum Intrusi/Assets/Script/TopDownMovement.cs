using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // vitesse max
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;        // pas de gravité
        rb.linearDamping = 10f;               // freine automatiquement
        rb.angularDamping = 0f;
        rb.freezeRotation = true;    // empêche la rotation
    }

    private void FixedUpdate()
    {
        Vector2 move = KeyboardMovement();
        rb.linearVelocity = move * moveSpeed;
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
}
