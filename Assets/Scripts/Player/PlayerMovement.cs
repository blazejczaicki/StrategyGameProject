using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : ObjectMovement
{
    protected override void Awake()
    {
        base.Awake();
    }
    public override void Move(dynamic direction, Chunk chunk)
    {
        movementDirection = direction;
        animator.SetFloat(animatorHorizontalID, movementDirection.x);
        animator.SetFloat(animatorVerticalID, movementDirection.y);
        animator.SetFloat(animatorSpeedID, movementDirection.sqrMagnitude);
    }

    public override void ResetMovement()
    {
        movementDirection = Vector2.zero;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movementDirection * stats.speed* Time.fixedDeltaTime);
    }
}
