using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//http://www.iquilezles.org/www/articles/texturerepetition/texturerepetition.htm
//https://www.reddit.com/r/gamedev/comments/8j3qbx/barycentric_perlin_noise/
//https://www.gamedev.net/forums/topic/314856-2d-vector-to-barycentric-coordinate/
//https://www.gamedev.net/forums/topic/295943-is-this-a-better-point-in-triangle-test-2d/
//https://www.boristhebrave.com/2018/05/12/barycentric-perlin-noise/
//https://github.com/keijiro/PerlinNoise/blob/master/Assets/Perlin.cs
//https://www.gamedev.net/forums/topic/646350-perlin-noise-for-rivers/
//https://www.redblobgames.com/x/1723-procedural-river-growing/
//https://gamedev.stackexchange.com/questions/45403/algorithms-for-rainfall-river-creation-in-procedurally-generated-terrain
//midpoint displacement, algorytm knn, Closest_pair_of_points_problem, quadtree, zwykłe sortowanie, tylko wartość kwadratowa bez sqrt
//https://www.reddit.com/r/gamedev/comments/wtwv6/how_does_notch_solve_generating_biomes_over_an/
//https://github.com/jceipek/Unity-delaunay
//https://squeakyspacebar.github.io/2017/07/12/Procedural-Map-Generation-With-Voronoi-Diagrams.html
//http://www-cs-students.stanford.edu/~amitp/game-programming/polygon-map-generation/
//https://github.com/SteveJohnstone/VoronoiMapGen/blob/master/Assets/Scripts/Voronoi/VoronoiGenerator.cs
//https://www.gamedev.net/forums/topic/646350-perlin-noise-for-rivers/
//https://forum.unity.com/threads/after-playing-minecraft.63149/
//https://www.reddit.com/r/proceduralgeneration/comments/7vhp04/thoughts_on_procedural_2d_map_generation_with/
//https://www.youtube.com/watch?v=EDv69onIETk
//https://www.gamasutra.com/blogs/JonGallant/20160128/264595/Procedurally_Generating_Wrapping_World_Maps_in_Unity_C__Part_1.php
//https://www.reddit.com/r/proceduralgeneration/comments/61ehsa/procedurally_generated_roads_in_an_infinite/
//https://medium.com/@eigenbom/dynamic-vs-static-procedural-generation-ed3e7a7a68a3
//https://gist.github.com/DGoodayle/2b4fa13037b209887852
//http://rab.ict.pwr.wroc.pl/~arent/rr/vivo/download/voronoi.pdf
//algorytmy graficzne diagram voronoi, quill18,  Hollow Knight’s Discord/making process
//https://www.habrador.com/tutorials/math/13-voronoi/
//https://www.reddit.com/r/proceduralgeneration/comments/5ykjz4/applying_voronoi_to_an_infinite_2d_tile_map/
//https://github.com/Scrawk/Procedural-Noise
//http://reedbeta.com/blog/quadrilateral-interpolation-part-2/
//https://catlikecoding.com/unity/tutorials/
//https://www.gamedev.net/forums/topic/576541-voronoi--worley-noise-knowlege-dump/
//https://www.reddit.com/r/Unity3D/comments/4u9ny7/i_did_it_i_made_a_quadtreelod_terrain_yay/
//https://www.scratchapixel.com/lessons/3d-basic-rendering/minimal-ray-tracer-rendering-simple-shapes/ray-sphere-intersection
//https://stackoverflow.com/questions/52878336/terrain-generation-interpolating-between-multiple-biome-height-maps
//https://www.youtube.com/watch?v=f0m73RsBik4&list=PLFt_AvWsXl0eBW2EiBtl_sxmDtSgZBxB3&index=8
//https://stackoverflow.com/questions/52460051/unity-job-system-and-multi-dimensional-arrays
//https://www.youtube.com/watch?v=gibqhg0wMA0
//https://www.reddit.com/r/Unity3D/comments/859rmv/i_tried_unitys_job_system_the_last_2_days_here_is/
//https://stackoverflow.com/questions/52389547/how-to-change-sprite-uv-in-unity
//https://assetstore.unity.com/packages/vfx/shaders/fullscreen-camera-effects/simple-2d-fog-storm-smoke-115617
//https://andrewhungblog.wordpress.com/2018/06/23/implementing-fog-of-war-in-unity/

public class ChunkGenerator : MonoBehaviour
{
    private List<Vector2> chunkToGenerate;
    [SerializeField] private Transform chunkParent;
    [SerializeField] private TileGenerator tileGenerator;
    

    private void Awake()
    {
        chunkToGenerate = new List<Vector2>();
    }

    public void GenerateStartChunk(Dictionary<Vector3, Chunk> setOfChunks)
    {
        GameObject newChunkObject = new GameObject();
        newChunkObject.transform.position = Vector2.zero;
        Chunk newChunk = newChunkObject.AddComponent<Chunk>();
        tileGenerator.generateTiles(newChunk);
        setOfChunks.Add(Vector2.zero, newChunk);
        PlayerManager.instance.SetStartChunk(setOfChunks);
    }

    private void AddPotentialDirections(float cameraRange, Vector2 currentChunkPosition, Vector2 playerPosition, List<Direction> potentialDirections)
    {

        //var a = playerPosition - currentChunkPosition;
        //if(Mathf.Abs(a)>cameraRange)

        if (playerPosition.x > currentChunkPosition.x + Chunk.chunkSizeHalf - cameraRange)
        {
            potentialDirections.Add(Direction.E);
        }
        if (playerPosition.x < currentChunkPosition.x - Chunk.chunkSizeHalf + cameraRange)
        {
            potentialDirections.Add(Direction.W);
        }
        if (playerPosition.y > currentChunkPosition.y + Chunk.chunkSizeHalf - cameraRange)
        {
            potentialDirections.Add(Direction.N);
        }
        if (playerPosition.y < currentChunkPosition.y - Chunk.chunkSizeHalf + cameraRange)
        {
            potentialDirections.Add(Direction.S);
        }
    }

    private void loadChunksToGenerate()
    {
        Chunk currentChunk = PlayerManager.instance.CurrentChunkTransform.GetComponent<Chunk>();
        List<Direction> potentialDirections= new List<Direction>();

        AddPotentialDirections(PlayerManager.instance.CameraRange, PlayerManager.instance.CurrentChunkTransform.transform.position,
            PlayerManager.instance.Player.position, potentialDirections);
        
        NeighbourChunk neighbour;
        foreach (var direction in potentialDirections)
        {            
            neighbour=Array.Find(currentChunk.Neighbours4Chunks, (n => n.direction == direction));
            if(!neighbour.exist)
            {
                neighbour.exist = true;
                chunkToGenerate.Add(neighbour.position);
            }
        }
    }

    private void CheckNeighbours(Dictionary<Vector3, Chunk> chunks, Chunk newChunk, Dictionary<Vector3, Chunk> visibleChunks)
    {
        Chunk chunk;
        int neighbourIndex;
        foreach (var neighbour in newChunk.Neighbours4Chunks)
        {
            if (chunks.TryGetValue(neighbour.position, out chunk))// visible chunks? wymaga zwiększonej strefy widoczności
            {
                neighbourIndex = (int)neighbour.opposedDirection;
                newChunk.Neighbours4Chunks[(int)neighbour.direction].exist = true;
                chunk.Neighbours4Chunks[neighbourIndex].exist = true;
            } 
        } 
    }

    private void diagonallyChunkGenerate(Dictionary<Vector3, Chunk> chunks, Dictionary<Vector3, Chunk> visibleChunks)
    {
        Vector2 positionDiagonallyChunk = PlayerManager.instance.CurrentChunk.checkDiagonallChunkNeeded(chunks);
        if (!chunks.ContainsKey(positionDiagonallyChunk) && positionDiagonallyChunk != Vector2.zero)// to samo co wyżej z widzialnymi
        {
            createChunk(positionDiagonallyChunk,chunks, visibleChunks);
        //Debug.Log("diagorigin "+ newOriginChunkForDiagonal.transform.position);
        }

    }

    private void createChunk(Vector2 chunkPosition, Dictionary<Vector3, Chunk> chunks, Dictionary<Vector3, Chunk> visibleChunks)
    {
        GameObject newChunkObject = new GameObject();
        newChunkObject.transform.position = chunkPosition;
       // newChunkObject.transform.SetParent(chunkParent);
        Chunk newChunk = newChunkObject.AddComponent<Chunk>();
        tileGenerator.generateTiles(newChunk);
        chunks.Add(chunkPosition, newChunk);
        visibleChunks.Add(chunkPosition, newChunk);
        CheckNeighbours(chunks, newChunk, visibleChunks);
    }
    public void generateChunk( Dictionary<Vector3, Chunk> chunks, Dictionary<Vector3, Chunk> visibleChunks)
    {
        loadChunksToGenerate();
        foreach (var newChunkPosition in chunkToGenerate)
        {
            createChunk(newChunkPosition,chunks, visibleChunks);
        }
        diagonallyChunkGenerate(chunks, visibleChunks);
        chunkToGenerate.Clear();
     }
}