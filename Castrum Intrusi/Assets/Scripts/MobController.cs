using UnityEngine;

public class MobController : MonoBehaviour
{
    public Animator animator;

    public bool playerDetected;

    public AnimatorOverrideController eyeFront;
    public AnimatorOverrideController eyeBack;
    public AnimatorOverrideController eyeSide;

    private Vector2 lastMoveDir;

    public void UpdateDirection(Vector2 moveDir)
    {
        if (moveDir.sqrMagnitude > 0.01f)
            lastMoveDir = moveDir; // on garde la dernière direction connue

        // Choix du set d'animations
        if (Mathf.Abs(lastMoveDir.x) > Mathf.Abs(lastMoveDir.y))
        {
            // Mouvement horizontal
            animator.runtimeAnimatorController = eyeSide;

            // Flip sprite si on va à gauche
            if (lastMoveDir.x < 0)
                transform.localScale = new Vector3(-1, 1, 1);
            else
                transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            // Mouvement vertical
            if (lastMoveDir.y > 0)
                animator.runtimeAnimatorController = eyeBack; // vers le haut
            else
                animator.runtimeAnimatorController = eyeFront; // vers le bas
        }
    }
}