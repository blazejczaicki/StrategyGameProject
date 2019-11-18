using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstarTest : MonoBehaviour
{
    private Astar pathFinding;
    [SerializeField] private Chunk chunk;

    // Start is called before the first frame update
    void Start()
    {
        pathFinding = new Astar();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            List<Node> path = pathFinding.FindPath(chunk, Vector3.zero,mousePos);
            if (path!=null)
            {
                for (int i = 0; i < path.Count; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].position.x, path[i].position.y),
                        new Vector3(path[i + 1].position.x, path[i + 1].position.y));
                }
            }

        }
    }
}
