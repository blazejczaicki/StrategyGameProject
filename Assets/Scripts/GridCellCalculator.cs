﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridCellCalculator 
{
    public static Vector2 GetMousePosToCell()
    {
        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2Int xyOnGrid = Vector2Int.zero;
        xyOnGrid.x = (int)(position.x + 0.5f);
        xyOnGrid.y = (int)(position.y + 0.5f);
        return xyOnGrid;
    }

    public static Vector2Int GetLocalPositionOnGrid(Vector2 positionGlobal, Chunk chunk)
    {
        Vector2Int positionLocal = Vector2Int.zero;
        positionLocal.x = (int)(-positionGlobal.x + chunk.transform.position.x + Chunk.chunkSizeHalf);
        positionLocal.y = (int)(-positionGlobal.y + chunk.transform.position.y + Chunk.chunkSizeHalf);
        return positionLocal;
    }

    public static Vector2 GetGlobalPositionOnGrid(Vector2Int Local, Chunk chunk)
    {
        Vector2 positionGlobal = Vector2.zero;
        positionGlobal.x = -Local.x + chunk.transform.position.x + Chunk.chunkSizeHalf;
        positionGlobal.y = -Local.y + chunk.transform.position.y + Chunk.chunkSizeHalf;
        return positionGlobal;
    }
}
