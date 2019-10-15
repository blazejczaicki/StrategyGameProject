using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BiomeGenerator : MonoBehaviour
{
    protected abstract void CreateBiome(Vector2 position);

    protected Vector2 DesignateBiomeFeaturePoint(Vector2 positionGridBox, float gridBoxSize)
    {
        Vector2 featurePoint = new Vector2();
        featurePoint.x = Random.Range(positionGridBox.x, positionGridBox.x + gridBoxSize);
        featurePoint.y = Random.Range(positionGridBox.y, positionGridBox.y + gridBoxSize);
        return featurePoint;
    }

    public void GenerateBiome(Vector2 gridBoxPosition, float gridBoxSize)
    {
        CreateBiome(DesignateBiomeFeaturePoint(gridBoxPosition, gridBoxSize));
    }
}
