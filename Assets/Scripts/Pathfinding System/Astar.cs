using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar
{
    [SerializeField] private Grid grid;
    private List<Node> openList;
    private List<Node> closedList;

    public List<Node> FindPath(Chunk chunk, Vector2 startPos, Vector2 endPos)
    {
        Node start= new Node(startPos, true);
        Node end= new Node(endPos, true);

        openList = new List<Node> { start };
        closedList = new List<Node>();

        start.g = 0;
        start.h = DistanceCost(start, end);
        start.CalculateF();

        while (openList.Count>0)
        {
            Node currentNode=GetLowestF(openList);
            if (currentNode==end)
            {
                return ComputePath(end);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (var neighbourNode in GetNeighbours(currentNode))
            {
                if (!closedList.Contains(neighbourNode))
                {
                    int tentativeG = currentNode.g + DistanceCost(currentNode, neighbourNode);
                    if (tentativeG < neighbourNode.g)
                    {
                        neighbourNode.previousNode = currentNode;
                        neighbourNode.g = tentativeG;
                        neighbourNode.h = DistanceCost(neighbourNode, end);
                        neighbourNode.CalculateF();
                        if (!openList.Contains(neighbourNode))
                        {
                            openList.Add(neighbourNode);
                        }
                    }
                }
            }
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



    private Node GetNode(Vector2 _position)
    {
        return new Node(Vector2.zero, true);
    }

    private List<Node> GetNeighbours(Node current)
    {
        List<Node> neighbourList = new List<Node>();
        Vector2 position = Vector2.zero;     
        for (int i = -1; i <= 1; i+=2)//left right
        {
            for (int j = -1; j <= 1; j++)
            {
                position.x = current.position.x + i;
                position.y = current.position.y + j;
                neighbourList.Add(GetNode(position));
            }
        }        

        if (true)//top down
        {

        }
        return neighbourList;
    }

    private int DistanceCost(Node start, Node end)
    {        
        return (int)(Vector2.Distance(start.position, end.position)*10.0f);
    }

}
