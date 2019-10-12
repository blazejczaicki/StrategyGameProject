using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILayerCalculation
{
    int CalculateLayer(Biome biome, float tileHeight);
}
