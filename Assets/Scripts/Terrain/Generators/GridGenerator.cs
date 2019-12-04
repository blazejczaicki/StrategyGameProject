using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public abstract class GridGenerator : MonoBehaviour
    {
        protected int quadMakingFromGridBoxIndex = 1;
        public float gridBoxSize { get; protected set; }
        protected float distanceFromCenter = 0;
        protected BiomeGenerator biomeGenerator;

        public abstract void GenerateGridEdges();

        public bool IsIncreaseGrid(float playerDistanceFromCenter)
        {
            return distanceFromCenter < playerDistanceFromCenter;
        }

    public List<Vector2> GenerateGridEdgesTest()
    {
        Debug.Log(gridBoxSize);
        Vector2 startPosition = new Vector2(-quadMakingFromGridBoxIndex * gridBoxSize, -quadMakingFromGridBoxIndex * gridBoxSize);
        Vector2 position;
        List<Vector2> testPos = new List<Vector2>();
        for (int i = 0; i <= 1; i++)
        {
            position = startPosition;
            position.x += i * Mathf.Abs(startPosition.x) * 2;
            for (int j = 0; j < quadMakingFromGridBoxIndex * 2 + 1; j++)
            {
                testPos.Add(position);
                position.y += gridBoxSize;
            }
            position = startPosition;
            position.y += i * Mathf.Abs(startPosition.y) * 2;
            for (int k = 1; k < quadMakingFromGridBoxIndex * 2; k++)
            {
                position.x += gridBoxSize;
                testPos.Add(position);
            }
        }
        distanceFromCenter = quadMakingFromGridBoxIndex * gridBoxSize;
        quadMakingFromGridBoxIndex++;
        return testPos;
    }

    private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Vector2 startPosition = new Vector2(-quadMakingFromGridBoxIndex * gridBoxSize, -quadMakingFromGridBoxIndex * gridBoxSize);
            Vector2 position;
            for (int i = 0; i <= 1; i++)
            {
                position = startPosition;
                position.x += i * Mathf.Abs(startPosition.x) * 2;
                for (int j = 0; j < quadMakingFromGridBoxIndex * 2 + 1; j++)
                {
                    Gizmos.DrawCube(position, new Vector3(10, 10));
                    Gizmos.DrawWireCube(position, new Vector2(gridBoxSize, gridBoxSize));
                    position.y += gridBoxSize;
                }
                position = startPosition;
                position.y += i * Mathf.Abs(startPosition.y) * 2;
                for (int k = 1; k < quadMakingFromGridBoxIndex * 2; k++)
                {
                    position.x += gridBoxSize;
                    Gizmos.DrawCube(position, new Vector3(10, 10));
                    Gizmos.DrawWireCube(position, new Vector2(gridBoxSize, gridBoxSize));
                }
            }
        }
    }
