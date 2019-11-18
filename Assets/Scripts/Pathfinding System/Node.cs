using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    [SerializeField] private Grid grid;

    public Vector2 position { get; set; }

    private const int gDefaultValue = 9999999; 

    public int g { get; set; }
    public int h { get; set; }
    public int f { get; set; }
    public bool isMoveable { get; set; }

    public Node previousNode { get; set; }

    public Node(Vector2 _position, bool _moveable)
    {
        position = _position;
        g = gDefaultValue;
        h = f = 0;
        isMoveable = _moveable;
    }

    public void CalculateF()
    {
        f = g + h;
    }
}
