using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeGridGenerator : GridGenerator
{
    private void Awake()
    {
        biomeGenerator = GetComponent<NormalBiomeGenerator>();
    }

    public BiomeGridGenerator() : base()
    {
        gridBoxSize = 10 * Chunk.chunkSize;
    }

    public override void GenerateGridEdges()
    {
        Vector2 startPosition = new Vector2(-quadMakingFromGridBoxIndex * gridBoxSize, -quadMakingFromGridBoxIndex * gridBoxSize);
        Vector2 position;
        for (int i = 0; i <= 1; i++)
        {
            position = startPosition;
            position.x += i * Mathf.Abs(startPosition.x) * 2;
            for (int j = 0; j < quadMakingFromGridBoxIndex * 2 + 1; j++)
            {
                biomeGenerator.GenerateBiome(position, gridBoxSize);
                position.y += gridBoxSize;
            }
            position = startPosition;
            position.y += i * Mathf.Abs(startPosition.y) * 2;
            for (int k = 1; k < quadMakingFromGridBoxIndex * 2; k++)
            {
                position.x += gridBoxSize;
                biomeGenerator.GenerateBiome(position, gridBoxSize);
            }
        }
        distanceFromCenter = quadMakingFromGridBoxIndex * gridBoxSize;
        quadMakingFromGridBoxIndex++;
    }


}