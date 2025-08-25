using System.Collections;
using UnityEngine;

// Handles player movement
public class PlayerAnimationController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private Animator animator;
    private Vector3 initialPlayerScale;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        initialPlayerScale = transform.localScale;
        ResetToIdle();
    }

    // Move the player horizontally to the right
    public IEnumerator WalkTo(Vector3 targetPosition)
    {
        AnimateWalk();

        Vector3 straightLineTarget = new Vector3(targetPosition.x, transform.position.y, transform.position.z);

        while (Vector3.Distance(transform.position, straightLineTarget) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, straightLineTarget, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = straightLineTarget;
        SetIdle();
    }

    // Move the player to a target position (x and y)
    public IEnumerator MovePlayerTo(Vector3 targetPosition)
    {
        AnimateWalk();

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition;
        SetIdle();
    }

    // Flips the player's sprite and sets the animation state to Walk
    public void AnimateWalk()
    {
        transform.localScale = new Vector3(-initialPlayerScale.x, initialPlayerScale.y, initialPlayerScale.z);
        animator.ResetTrigger("Idle");
        animator.SetTrigger("Walk");
    }

    // Plays the death animation
    public void PlayDeath()
    {
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Walk");
        animator.SetTrigger("Die");
    }

    // Sets the animation state to Idle
    public void ResetToIdle()
    {
        transform.localScale = new Vector3(-initialPlayerScale.x, initialPlayerScale.y, initialPlayerScale.z);
        SetIdle();
    }

    private void SetIdle()
    {
        animator.ResetTrigger("Walk");
        animator.SetTrigger("Idle");
    }
}
