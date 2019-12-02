using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController
{
    private CharacterController target;
    public CharacterController player;   //tmp 

    private void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        stats = GetComponent<CharacterStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject==player.gameObject)
        {
            target = player;
        }
    }

    public override void OnUpdate()
    {
        movement.Move(target, CheckOnWhichChunkYouStayed(transform.position));
    }
}
