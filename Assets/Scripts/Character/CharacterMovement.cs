using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Animator animator;
    private Vector2 movementDirection = Vector2.zero;


    private Rigidbody2D rb;

    private int animatorHorizontalID=0;
    private int animatorVerticalID=0;
    private int animatorSpeedID=0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animatorHorizontalID = Animator.StringToHash("Horizontal");
        animatorVerticalID = Animator.StringToHash("Vertical");
        animatorSpeedID = Animator.StringToHash("Speed");
    }

    public void Move(Vector2 direction)
    {
        movementDirection = direction;
        animator.SetFloat(animatorHorizontalID, movementDirection.x);
        animator.SetFloat(animatorVerticalID, movementDirection.y);
        animator.SetFloat(animatorSpeedID, movementDirection.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movementDirection * speed* Time.fixedDeltaTime);
    }
}
