using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : ObjectMovement
{
    protected override void Awake()
    {
        base.Awake();
    }
    public override void Move(dynamic direction)
    {
        movementDirection = direction;
        animator.SetFloat(animatorHorizontalID, movementDirection.x);
        animator.SetFloat(animatorVerticalID, movementDirection.y);
        animator.SetFloat(animatorSpeedID, movementDirection.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movementDirection * stats.speed* Time.fixedDeltaTime);
    }
}
