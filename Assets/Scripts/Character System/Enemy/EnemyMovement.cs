using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : ObjectMovement
{
    private Astar pathfinding;
    private int pathIterator = 0;
    private float pointTargetRadius = 0.2f;
    private float pathUpdateTime = 1.0f;
    private float pathUpdateLastTime = 0.0f;
    List<Node> path;

    protected override void Awake()
    {
        base.Awake();
        path = new List<Node>();
        pathfinding = new Astar();
    }

    private void UpdatePath(CharacterObjectController target, Chunk chunk)
    {
        Vector3 startPosition = (path != null && path.Count > pathIterator + 1) ? (Vector3)path[pathIterator].position : transform.position;
        path = pathfinding.FindPath(chunk, target.currentChunk,
            startPosition, target.transform.position);
        pathIterator = 0;
        if (path != null)
        {
            Vector2 centerPosition = Vector2.zero;
            for (int i = 0; i < path.Count - 1; i++)
            {
                //Debug.DrawLine(new Vector3(path[i].position.x, path[i].position.y),
                //    new Vector3(path[i + 1].position.x, path[i + 1].position.y), Color.green, 10.0f);
                centerPosition = path[i].position;
                centerPosition.x += 0.5f;                               // przesunięcie z wierzchołka płytki do jej środka
                centerPosition.y += 0.5f;                               //
                path[i].position = centerPosition;
            }
        }
    }

    private bool IsPointReached()
    {
        return Vector2.Distance(transform.position, path[pathIterator].position) <= pointTargetRadius;
    }

    private void UpdateDirection(List<Node> path)
    {
        if (path != null)
        {
            if (pathIterator >= path.Count)
            {
                path = null;
            }
            else
            {
                var heading = (Vector2)transform.position - path[pathIterator].position;
                movementDirection = -heading / heading.magnitude;
                if (IsPointReached())
                {
                    pathIterator++;
                }
            }
        }
    }

    public override void Move(dynamic target, Chunk chunk)
    {
        if (target != null)
        {
            if (Time.time > pathUpdateLastTime + pathUpdateTime)
            {
                pathUpdateLastTime = Time.time;
                UpdatePath(target, chunk);
            }
            UpdateDirection(path);
        }
    }

    public override void ResetMovement()
    {
        pathIterator = 0;
        pathUpdateLastTime = 0.0f;
        movementDirection = Vector2.zero;
    }
    
}
