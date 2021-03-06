﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class AstarTest : MonoBehaviour
{
    private Astar pathFinding;
    private Chunk chunk;

    [SerializeField] private PlayerController player;
    [SerializeField] private GeneratorManager ge;

    void Start()
    {
        pathFinding = new Astar();
    }
    
    void Update()
    {
        chunk = player.currentChunk;
        if (Input.GetMouseButtonDown(0))
        {
            //Profiler.BeginSample("FindPath");
            List<Node> path = pathFinding.FindPath(chunk, GetChunkOnMouse(), new Vector2(-4, 6), GetMousePosToCell());
            //Profiler.EndSample();
            if (path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].position.x, path[i].position.y),
                        new Vector3(path[i + 1].position.x, path[i + 1].position.y), Color.green, 10.0f);
                }
            }
            else
                Debug.Log("elsexd");
        }
    }

    private Chunk GetChunkOnMouse()
    {
        Vector2 newCurrentChunkPos = new Vector2();

        newCurrentChunkPos.x = Mathf.Round(GetMousePosToCell().x / Chunk.chunkSize) * Chunk.chunkSize;
        newCurrentChunkPos.y = Mathf.Round(GetMousePosToCell().y / Chunk.chunkSize) * Chunk.chunkSize;
        return ge.getChunkTransform(newCurrentChunkPos).GetComponent<Chunk>();
    }

    private Vector2 GetMousePosToCell()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
