using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TemperatureAreaType { cold, cool, hot }

[System.Serializable]
public class Temperature : MonoBehaviour, ILayerCalculation
{
    public List<Topography> temperatureKinds;
    private float cold = 0.3f;
    private float cool = 0.7f;
 //   private float hot = 1f;

    public int CalculateLayer(Biome biome,float tileHeight)
    {
        TemperatureAreaType tempType;
        if (tileHeight <= cold)
            tempType = TemperatureAreaType.cold;
        else if (tileHeight <= cool)
            tempType = TemperatureAreaType.cool;
        else
            tempType = TemperatureAreaType.hot;
        return (int)tempType;
    }

    public Topography GetTopologyForTemperature(Biome biome, float tileHeight)
    {
        return temperatureKinds[CalculateLayer(biome,tileHeight)];
    }
}
