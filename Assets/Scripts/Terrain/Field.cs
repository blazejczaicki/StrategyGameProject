using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileType { water, shallowWater, sand, stone, grass, snow}

public class Field : MonoBehaviour
{

    [SerializeField] TileType tileType;
    [SerializeField] private Tile tile;
    public Tile tileProp
    {
        get { return tile; }
        set { tile = value; }
    }
    private bool isMovable;

}