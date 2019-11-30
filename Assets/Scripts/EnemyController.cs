using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController
{
    private Transform target;
    [SerializeField] private Transform player;    

    private void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        stats = GetComponent<CharacterStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject==player.gameObject)
        {
            target = player.transform;
        }
    }

    public override void OnUpdate()
    {
        movement.Move(target);
    }

    private void Update()
    {
        OnUpdate();
    }
}
