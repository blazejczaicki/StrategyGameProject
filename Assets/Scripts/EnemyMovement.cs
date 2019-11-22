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
    private void Start()
    {
        pathfinding = new Astar();
    }

    private void UpdatePath(Transform target)
    {        
            path = pathfinding.FindPath(ChunkSeeker.CheckOnWhichChunkYouStayed(transform.position), ChunkSeeker.CheckOnWhichChunkYouStayed(target.position), 
                transform.position, target.transform.position);
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
        return Vector2.Distance(transform.position, path[pathIterator].position)<= pointTargetRadius;
    }

    private void UpdateDirection(Vector3 targetPosition)
    {
        var heading = transform.position - targetPosition;
        movementDirection = -heading / heading.magnitude;
    }

    public override void Move(dynamic target)//transform
    {
        if (target != null)
        {
            if (Time.time > pathUpdateLastTime + pathUpdateTime)
            {
                pathUpdateLastTime = Time.time;
                UpdatePath(target);
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
            //try
            //{
            //}
            //catch(Index)
            //{

            //}
            //UpdateAnimation();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movementDirection * speed * Time.fixedDeltaTime);
    }
}
