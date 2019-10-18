using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Field : MonoBehaviour
{
    [SerializeField] private int id = 0;
    public int ID { get; }
    [SerializeField] private Tile tile;
    public Tile TileRepresentation
    { get; }
    private bool isMovable;


    [SerializeField] private List<Transform> objectList;
    [SerializeField] [Range(0, 1)] private float probabilityOfGeneration = 0;
    public float ProbabilityOfGeneration { get; }
}