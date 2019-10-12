using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperBiomeGenerator : BiomeGenerator
{
    public static Dictionary<Vector2, HyperBiome> hyperBiomes;
    [SerializeField] private List<HyperBiome> hyperBiomeKinds;


    private void Awake()
    {
        hyperBiomes = new Dictionary<Vector2, HyperBiome>();
    }

    protected override void CreateBiome(Vector2 position)
    {
        int biomeNumber = Random.Range(0, hyperBiomeKinds.Count);
        Debug.Log(biomeNumber);
        HyperBiome hyperBiome = hyperBiomeKinds[biomeNumber].Clone();
        hyperBiome.transform.position = position;
        hyperBiomes.Add(hyperBiome.transform.position, hyperBiome);
    }
}
