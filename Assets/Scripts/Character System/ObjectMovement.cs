using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectMovement : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    protected Vector2 movementDirection = Vector2.zero;
    protected CharacterStats stats;
    protected Rigidbody2D rb;

    protected int animatorHorizontalID = 0;
    protected int animatorVerticalID = 0;
    protected int animatorSpeedID = 0;

    public abstract void Move(dynamic direction, Chunk chunk);
    public abstract void ResetMovement();

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<CharacterStats>();

        animatorHorizontalID = Animator.StringToHash("Horizontal");
        animatorVerticalID = Animator.StringToHash("Vertical");
        animatorSpeedID = Animator.StringToHash("Speed");
    }

    protected void UpdateAnimation()
    {
        animator.SetFloat(animatorHorizontalID, movementDirection.x);
        animator.SetFloat(animatorVerticalID, movementDirection.y);
        animator.SetFloat(animatorSpeedID, movementDirection.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movementDirection * stats.speed * Time.fixedDeltaTime);
    }
}
