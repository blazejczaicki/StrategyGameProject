using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObjectSpawn : MonoBehaviour
{
    [SerializeField] private List<Field> fields;

    private void CreateObject(int tileID, Vector3 position, Chunk chunk)
    {
        GameObject newObject= Instantiate(fields[tileID].GetRandomEnviroObject(), position, Quaternion.identity);


        var healthCanvas= newObject.GetComponentInChildren<Canvas>();
        healthCanvas.worldCamera = Camera.main;
        healthCanvas.gameObject.SetActive(false);

        newObject.transform.SetParent(chunk.transform);
    }

    public IEnumerator Generate(Chunk chunk)
    {
        float randomVal;
        Vector3 position= Vector3.zero;
        Vector2Int localPosition = Vector2Int.zero;
        int breakGeneration = (int)(Chunk.chunkSize * 0.4f);
        for (int x = 0; x < Chunk.chunkSize; x++)
        {
            for (int y = 0; y < Chunk.chunkSize; y++)
            {
                randomVal = Random.Range(0.0f, 1.0f);
                if (randomVal<fields[chunk.Fields[x][y]].ProbabilityOfGeneration)
                {
                    position.x = -x + chunk.transform.position.x + Chunk.chunkSizeHalf + chunk.grid.cellSize.x*0.5f;
                    position.y = -y + chunk.transform.position.y + Chunk.chunkSizeHalf+ chunk.grid.cellSize.y * 0.5f;
                    CreateObject(chunk.Fields[x][y], position, chunk);
                    chunk.SetGridArrayField(x, y, false);
                }
                else
                {
                    chunk.SetGridArrayField(x, y, true);
                }
            }
            if (x == breakGeneration)
            {
                breakGeneration += breakGeneration;
                yield return null;
            }
        }
    }
}
