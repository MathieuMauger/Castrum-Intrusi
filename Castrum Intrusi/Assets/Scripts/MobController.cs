using UnityEngine;

public class MobController : MonoBehaviour
{
    public Animator animator;

    public AnimatorOverrideController frontAOC;
    public AnimatorOverrideController backAOC;
    public AnimatorOverrideController sideAOC;

    private Vector2 lastMoveDir;

    public void UpdateDirection(Vector2 moveDir)
    {
        if (moveDir.sqrMagnitude > 0.01f)
            lastMoveDir = moveDir; // on garde la dernière direction connue

        // Choix du set d'animations
        if (Mathf.Abs(lastMoveDir.x) > Mathf.Abs(lastMoveDir.y))
        {
            // Mouvement horizontal
            animator.runtimeAnimatorController = sideAOC;

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
                animator.runtimeAnimatorController = backAOC; // vers le haut
            else
                animator.runtimeAnimatorController = frontAOC; // vers le bas
        }
    }
}