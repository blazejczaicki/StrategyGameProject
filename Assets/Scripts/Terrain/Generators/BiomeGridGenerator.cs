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
        //startPosition- pozycja początku generacji
        Vector2 position;
        for (int i = 0; i <= 1; i++) // pętla obsługująca po dwie krawędzie przy generacji siatki bez środka
        {
            position = startPosition;
            position.x += i * Mathf.Abs(startPosition.x) * 2;                // za pierwszym razem krawedz lewa, za drugim prawa
            for (int j = 0; j < quadMakingFromGridBoxIndex * 2 + 1; j++)     //przejście po krawędzi pionowej
            {
                biomeGenerator.GenerateBiome(position, gridBoxSize);
                position.y += gridBoxSize;
            }
            position = startPosition;
            position.y += i * Mathf.Abs(startPosition.y) * 2;            //za pierwszym razem krawedz dolna, za drugim gorna
            for (int k = 1; k < quadMakingFromGridBoxIndex * 2; k++)     //przejście po krawędzi poziomej
            {
                position.x += gridBoxSize;
                biomeGenerator.GenerateBiome(position, gridBoxSize);
            }
            //mnozenie razy 2 wynika z dodawania liczb dodatnich do umenych, by uzyskać ich odpowiedniki o zmienionym znaku
        }
        distanceFromCenter = quadMakingFromGridBoxIndex * gridBoxSize;
        quadMakingFromGridBoxIndex++;
    }


}