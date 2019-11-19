using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MovementObject
{
    private Astar pathfinding;
    [SerializeField] private Transform target;

    private void Awake()
    {
        pathfinding = new Astar();
    }

    public void MoveToTarget()
    {
       // pathfinding.FindPath(CurrentChunk, transform.position, target.position);
    }

    private void Update()
    {
        CheckOnWhichChunkYouStayed();
    }
}
