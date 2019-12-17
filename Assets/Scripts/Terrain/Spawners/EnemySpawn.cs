using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private int minEnemiesOnChunk=0;
    [SerializeField] private int maxEnemiesOnChunk=0;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private GeneratorManager generatorManager;
    
    [SerializeField] private List<Field> fields;

    private void CreateEnemy(int tileID, Vector3 position)
    {
        if (fields[tileID].enemyKinds.Count==0)
        {
            return;
        }
        var newEnemy=Instantiate(fields[tileID].GetRandomEnemy(), position, Quaternion.identity).GetComponent<EnemyController>();
        newEnemy.Generator = generatorManager;
        enemyManager.AddEnemy(newEnemy);
        newEnemy.GetComponentInChildren<Canvas>().worldCamera = Camera.main;
    }

     public void SpawnOnChunk(Chunk chunk)
    {
        int countEnemies = Random.Range(minEnemiesOnChunk, maxEnemiesOnChunk);
        Vector2Int localPosition = Vector2Int.zero;
        for (int i = 0; i < countEnemies; i++)
        {
            localPosition.x = Random.Range(4, Chunk.chunkSize - 4);
            localPosition.y = Random.Range(4, Chunk.chunkSize - 4);
            for (int j = 0; j < countEnemies; j++)
            {
                if (chunk.GridMovementArray[localPosition.x][localPosition.y]==true)
                {
                    CreateEnemy(chunk.Fields[localPosition.x][localPosition.y], GridCellCalculator.GetGlobalPositionOnGrid(localPosition, chunk));
                    break;
                }
                localPosition.x = Random.Range(4, Chunk.chunkSize - 4);
                localPosition.y = Random.Range(4, Chunk.chunkSize - 4);
            }
        }
    }
}
