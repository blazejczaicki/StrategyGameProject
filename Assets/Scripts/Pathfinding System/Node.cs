using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    [SerializeField] private Grid grid;
    public Vector2 position { get; set; }
    public Vector2Int positionOnChunkGrid { get; set; }
    public Chunk chunk { get; set; }
    private const int gDefaultValue = 9999999; 

    public int g { get; set; }
    public int h { get; set; }
    public int f { get; set; }

    public bool isMoveable { get; set; }
    public Node previousNode { get; set; }

    private void SetFields(bool _moveable, Chunk _chunk)
    {
        chunk = _chunk;
        g = gDefaultValue;
        h = f = 0;
        isMoveable = _moveable;
        previousNode = null;
    }

    public Node(Vector2 _position, bool _moveable, Chunk _chunk)
    {

        positionOnChunkGrid = _chunk.GetLocalPositionOnGrid(_position);
        position = _position;
        SetFields(_moveable, _chunk);
    }

    public Node(Vector2Int _position, bool _moveable, Chunk _chunk)
    {
        positionOnChunkGrid =_position ;
        position = _chunk.GetGlobalPositionOnGrid(_position);
        SetFields(_moveable, _chunk);
    }

    public void CalculateF()
    {
        f = g + h;
    }
}
