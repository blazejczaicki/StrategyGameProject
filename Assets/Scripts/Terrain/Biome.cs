using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biome : MonoBehaviour
{
    public float topographyScaleOffset;
    //public float moistureScaleOffset { set; get; }
    //public float temperatureScaleOffset { set; get; }
    public float RiverScale;

    public float deepWaterOffset;
    public float shallowWaterOffset;
    public float lowLandOffset;
    public float upLandOffset;
    public float moutainOffset;

    public Biome Clone()
    {
        return (Biome)MemberwiseClone();
    }
}
