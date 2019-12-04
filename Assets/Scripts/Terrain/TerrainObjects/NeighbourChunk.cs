using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { N, S, W, E }

public class NeighbourChunk
{
    public Direction direction { get; set; }
    public Direction opposedDirection { get; set; }
    public Vector2 position { get; set; }
    public bool exist { get; set; }
    public Chunk chunk { get; set; }

    public NeighbourChunk(Vector2 _position, Direction _direction, bool _exist)
    {
        direction = _direction;
        position = _position;
        exist = _exist;
        if ((int)direction % 2 == 0)
            opposedDirection = direction + 1;
        else
            opposedDirection = direction - 1;
    }
}
