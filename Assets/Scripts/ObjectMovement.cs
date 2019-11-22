using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectMovement : MonoBehaviour
{
    public Chunk CurrentChunk { get; set; }
    public Transform CurrentChunkTransform { get; set; }

    [SerializeField] protected float speed = 5;
    [SerializeField] protected Animator animator;
    protected Vector2 movementDirection = Vector2.zero;


    protected Rigidbody2D rb;

    protected int animatorHorizontalID = 0;
    protected int animatorVerticalID = 0;
    protected int animatorSpeedID = 0;

    public abstract void Move(dynamic direction);

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log(rb);

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

    protected void CheckOnWhichChunkYouStayed()
    {
        Vector2 newCurrentChunkPos = new Vector2();

        newCurrentChunkPos.x = Mathf.Round(transform.position.x / Chunk.chunkSize) * Chunk.chunkSize;
        newCurrentChunkPos.y = Mathf.Round(transform.position.y / Chunk.chunkSize) * Chunk.chunkSize;

        CurrentChunkTransform = GameManager.instance.getChunkTransform(newCurrentChunkPos);
        CurrentChunk = CurrentChunkTransform.GetComponent<Chunk>();
    }
}
