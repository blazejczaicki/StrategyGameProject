using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperBiome : MonoBehaviour
{
    public Dictionary<Vector2, HyperBiome> biomes;

    private void Awake()
    {
        biomes = new Dictionary<Vector2, HyperBiome>();
    }

    public HyperBiome Clone()
    {
        return (HyperBiome)MemberwiseClone();
    }
}
