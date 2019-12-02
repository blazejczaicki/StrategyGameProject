using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : ObjectMovement
{
    private Astar pathfinding;
    private bool nodeIsReached = false;
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

    private void UpdatePath(CharacterController target, Chunk chunk)
    {
        Vector3 startPosition = (path!=null && path.Count > pathIterator + 1) ?  (Vector3)path[pathIterator].position: transform.position; 
            path = pathfinding.FindPath(chunk, target.currentChunk,
                startPosition, target.transform.position);
        pathIterator = 0;
            if (path != null)
            {
                Vector2 centerPosition = Vector2.zero;
                for (int i = 0; i < path.Count - 1; i++)
                {                    
                    Debug.DrawLine(new Vector3(path[i].position.x, path[i].position.y),
                        new Vector3(path[i + 1].position.x, path[i + 1].position.y), Color.green, 10.0f);
                    centerPosition = path[i].position;
                    centerPosition.x += 0.5f;
                    centerPosition.y += 0.5f;
                    path[i].position = centerPosition;
                }
            }
    }

    private bool IsPointReached()
    {
        Debug.Log(pathIterator);// były błędy
        return Vector2.Distance(transform.position, path[pathIterator].position)<= pointTargetRadius;
    }

    private void UpdateDirection(Vector3 targetPosition)
    {
        var heading = transform.position - targetPosition;
        movementDirection = -heading / heading.magnitude;
    }

    public override void Move(dynamic target, Chunk chunk)//transform
    {
        if (target != null)
        {
            if (Time.time > pathUpdateLastTime + pathUpdateTime)
            {
                pathUpdateLastTime = Time.time;
                UpdatePath(target, chunk);
            }
          
                if (path != null)
                {
                    if (IsPointReached())
                    {
                        pathIterator++;
                    }
                    if (pathIterator >= path.Count)
                    {
                        path = null;
                    }
                    else
                    {
                        UpdateDirection(path[pathIterator].position);
                    }
                }
            //UpdateAnimation();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movementDirection * stats.speed * Time.fixedDeltaTime);
    }
}
