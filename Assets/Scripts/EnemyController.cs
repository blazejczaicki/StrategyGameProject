using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Astar pathfinding;
    private EnemyMovement movement;
    private Transform target;

    [SerializeField]private Transform player;
    

    private void Awake()
    {
        pathfinding = new Astar();
        movement = GetComponent<EnemyMovement>();
    }

    private void SeekTarget()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject==player.gameObject)
        {
            target = player.transform;
        }
    }

    private void Update()
    {
        movement.Move(target);
    }
}
