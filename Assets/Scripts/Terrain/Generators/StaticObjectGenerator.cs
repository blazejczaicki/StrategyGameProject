using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObjectGenerator : MonoBehaviour
{
    [SerializeField] private List<Field> fields;

    private void CreateObject(int tileID, Vector3 position)
    {
        Instantiate(fields[tileID].GetRandomEnviroObject(), position, Quaternion.identity);
    }

    public IEnumerator Generate(Chunk chunk)
    {
        float randomVal;
        Vector3 position= Vector3.zero;
        int breakGeneration = (int)(Chunk.chunkSize * 0.4f);
        for (int x = 0; x < Chunk.chunkSize; x++)
        {
            for (int y = 0; y < Chunk.chunkSize; y++)
            {
                randomVal = Random.Range(0.0f, 1.0f);
               // Debug.Log(fields[chunk.Fields[x][y]].ID);
                if (randomVal<fields[chunk.Fields[x][y]].ProbabilityOfGeneration)
                {

                    position.x = -x + chunk.transform.position.x + Chunk.chunkSizeHalf + chunk.grid.cellSize.x*0.5f;
                    position.y = -y + chunk.transform.position.y + Chunk.chunkSizeHalf+ chunk.grid.cellSize.y * 0.5f;
                    CreateObject(chunk.Fields[x][y], position);
                    chunk.SetGridArrayField(x, y, MovementType.NOTmoveable);
                }
                else
                {
                    chunk.SetGridArrayField(x, y, MovementType.Moveable);
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
