using System.Collections;
using UnityEngine;

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

    public void AnimateWalk()
    {
        transform.localScale = new Vector3(-initialPlayerScale.x, initialPlayerScale.y, initialPlayerScale.z);
        animator.ResetTrigger("Idle");
        animator.SetTrigger("Walk");
    }

    public void PlayDeath()
    {
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Walk");
        animator.SetTrigger("Die");
    }

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
