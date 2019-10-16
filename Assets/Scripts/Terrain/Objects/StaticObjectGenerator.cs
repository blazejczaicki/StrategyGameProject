using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObjectGenerator : MonoBehaviour
{
    [SerializeField] private List<Field> fields;

    private void CreateObject()
    {

    }

    public void Generate(Chunk chunk)
    {
        for (int x = 0; x < Chunk.chunkSize; x++)
        {
            for (int y = 0; y < Chunk.chunkSize; y++)
            {
                //chunk.Fields[x][y]
            }
        }
    }
}
