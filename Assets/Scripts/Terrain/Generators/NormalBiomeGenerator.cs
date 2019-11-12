using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class NormalBiomeGenerator : BiomeGenerator
{
    public static Dictionary<Vector2, Biome> biomes;
    [SerializeField] private List<Biome> biomeKinds;

    private void Awake()
    {
        biomes = new Dictionary<Vector2, Biome>();

    }
     
    protected override void CreateBiome(Vector2 position)
    {
        int biomeNumber = Random.Range(0, biomeKinds.Count);
        Biome biome = biomeKinds[biomeNumber].Clone();
        biome.transform.position = position;
        biomes.Add(biome.transform.position, biome);
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            foreach (var biome in biomes)
            {
                Gizmos.DrawCube(biome.Key, new Vector3(10, 10));
            }
        }
    }

}
