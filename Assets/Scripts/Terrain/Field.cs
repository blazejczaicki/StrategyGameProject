using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Field : MonoBehaviour
{
    [SerializeField] private int id = 0;
    public int ID { get { return id; } }
    [SerializeField] private Tile tile;
    public Tile TileRepresentation
    { get { return tile; } }
    private bool isMovable;

    [SerializeField] private List<GameObject> _enemyKinds;
    public List<GameObject> enemyKinds { get => _enemyKinds;}
    [SerializeField] private List<GameObject> objectList;
    [SerializeField] [Range(0, 1)] private float probabilityOfGeneration = 0;
    public float ProbabilityOfGeneration { get { return probabilityOfGeneration; } }

    public GameObject GetRandomEnemy()
    {
        int index = Random.Range(0, _enemyKinds.Count);
        return _enemyKinds[index];
    }

    public GameObject GetRandomEnviroObject()
    {
        int index = Random.Range(0, objectList.Count);
        return objectList[index];
    }

}