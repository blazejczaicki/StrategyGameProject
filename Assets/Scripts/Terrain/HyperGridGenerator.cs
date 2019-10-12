using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class HyperGridGenerator : GridGenerator
    {
        private void Awake()
        {
            gridBoxSize = Chunk.chunkSize * 100;
        biomeGenerator = GetComponent<HyperBiomeGenerator>();
        if (biomeGenerator == null)
            Debug.Log("lul");
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
            //Debug.Log(gridBoxSize);
            distanceFromCenter = quadMakingFromGridBoxIndex * gridBoxSize;
            quadMakingFromGridBoxIndex++;
        }
    }
