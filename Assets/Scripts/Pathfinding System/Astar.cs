using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class Astar
{
    [SerializeField] private Grid grid;
    private List<Node> openList;
    private List<Node> closedList;

    public List<Node> FindPath(Chunk startChunk, Chunk endChunk, Vector2 startPos, Vector2 endPos)
    {
        Node start= new Node(startPos, true, startChunk);
        Node end= new Node(endPos, true, endChunk);

        openList = new List<Node> { start };
        closedList = new List<Node>();

        start.g = 0;
        start.h = DistanceCost(start, end);
        start.CalculateF();
        while (openList.Count>0)
        {
            Node currentNode=GetLowestF(openList);
            if (currentNode.position==end.position || (currentNode.positionOnChunkGrid== end.positionOnChunkGrid && currentNode.chunk==end.chunk) || Vector2.Distance(currentNode.position, end.position)<=1.0f)
            {
                
                end = currentNode;
                return ComputePath(end);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);
            
            //Profiler.BeginSample("FindInNeighbour");
            foreach (var neighbourNode in GetNeighbours(currentNode))
            {
                if (closedList.Exists((x)=>x.position==neighbourNode.position)!=true && neighbourNode.isMoveable)
                {
                    int tentativeG = currentNode.g + DistanceCost(currentNode, neighbourNode);
                    if (tentativeG < neighbourNode.g)
                    {
                        neighbourNode.previousNode = currentNode;
                        neighbourNode.g = tentativeG;
                        neighbourNode.h = DistanceCost(neighbourNode, end);
                        neighbourNode.CalculateF();
                        if (openList.Exists((x)=>x.position == neighbourNode.position)!=true)
                        {
                            openList.Add(neighbourNode);
                        }
                    }
                }
            }
            //Profiler.EndSample();
        }
        return null;
    }

    private List<Node> ComputePath(Node end)
    {
        List<Node> path = new List<Node>();
        path.Add(end);
        Node currenNode = end;
        while (currenNode.previousNode!=null)
        {
            path.Add(currenNode.previousNode);
            currenNode = currenNode.previousNode;
        }
        path.Reverse();
        return path;
    }

    private Node GetLowestF(List<Node> nodes)
    {
        Node lowestFNode = nodes[0];
        foreach (var node in nodes)
        {
            if (node.f<lowestFNode.f)
            {
                lowestFNode = node;
            }
        }
        return lowestFNode;
    }

    private void ComputeNodePosition(out Chunk chunk, out Vector2Int localPos, int i, int j, Node current)
    {
        chunk = current.chunk;
        localPos = Vector2Int.zero;
        localPos.x = current.positionOnChunkGrid.x + i;
        localPos.y = current.positionOnChunkGrid.y + j;
        bool vertexX = false;
        bool vertexY=false;
        int direction =0;
        //Direction dir = Direction;
        if (localPos.x >= 64) //left
        {
            chunk = current.chunk.Neighbours4Chunks[(int)Direction.W].chunk;
            localPos.x = 0;
            vertexX = true;
            direction = (int)Direction.W;
            Debug.Log(Direction.W);
        }
        else if (localPos.x < 0) //right
        {
            chunk = current.chunk.Neighbours4Chunks[(int)Direction.E].chunk;
            localPos.x = 64;
            vertexX = true;
            direction = (int)Direction.E;
            Debug.Log(Direction.E);
        }

        if (localPos.y >= 64) //down
        {
            chunk = current.chunk.Neighbours4Chunks[(int)Direction.S].chunk;
            localPos.y = 0;
            vertexY = true;
           Debug.Log(Direction.S);
        }
        else if (localPos.y < 0) //top
        {
            chunk = current.chunk.Neighbours4Chunks[(int)Direction.N].chunk;
            localPos.y = 64;
            vertexY = true;
            Debug.Log(Direction.N);
        }

        if (vertexX && vertexY)
        {
            chunk = chunk.Neighbours4Chunks[direction].chunk;
        }
        
    }

    private List<Node> GetNeighbours(Node current)
    {
        List<Node> neighbourList = new List<Node>();
        Vector2Int localPos;
        //Debug.Log(current.positionOnChunkGrid);
        Chunk chunk;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
              //  Debug.Log(current.positionOnChunkGrid);
                ComputeNodePosition(out chunk, out localPos, i, j, current);
                Debug.DrawLine(new Vector2(-localPos.x + chunk.transform.position.x + Chunk.chunkSizeHalf, -localPos.y + chunk.transform.position.y + Chunk.chunkSizeHalf),
                    new Vector2(-localPos.x + chunk.transform.position.x + Chunk.chunkSizeHalf+1, -localPos.y + chunk.transform.position.y + Chunk.chunkSizeHalf+1), Color.blue, 10.0f);
               //Debug.Log(chunk.transform.position);
                neighbourList.Add(chunk.GetNode(localPos));         // na krawędziach xd
            }
        }
        neighbourList.Remove(neighbourList.Find((neighbour) => neighbour.positionOnChunkGrid == current.positionOnChunkGrid));

        //foreach (var nei in neighbourList)
        //{
        //    Debug.DrawLine(nei.position, new Vector2(nei.position.x + 1, nei.position.y + 1), Color.red, 10.0f);
        //}

        return neighbourList;
    }

    private int DistanceCost(Node start, Node end)
    {        
        return (int)(Vector2.Distance(start.position, end.position)*10.0f);
    }

}
