using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public Transform target;
    public GameObject chunkPrefab;
    public int worldDiameter = 3;
    public MeshCollider meshCollider;

    private Dictionary<Vector3Int, GameObject> chunks = new Dictionary<Vector3Int, GameObject>();
    private Vector3 chunkSize;
    private float offset;
    private Vector3 lastUpdatedTargetPos = Vector3.one;
    private GameObject chunkTerrainPrefab; 

    void Start()
    {
        chunkTerrainPrefab = chunkPrefab.GetComponent<ChunkManager>().terrain;
        Vector3 ogChunkTerrainSize = chunkTerrainPrefab.GetComponent<MeshRenderer>().localBounds.size;
        Vector3 ogChunkTerrainScale = chunkTerrainPrefab.transform.localScale;

        chunkSize = new Vector3(ogChunkTerrainSize.x * ogChunkTerrainScale.x, ogChunkTerrainSize.y * ogChunkTerrainScale.y, ogChunkTerrainSize.z * ogChunkTerrainScale.z);
        offset = Mathf.FloorToInt(worldDiameter / 2f);

        ManageChunkGeneration();
    }

    void Update()
    {
        ManageChunkGeneration();
    }

    void ManageChunkGeneration()
    {
        float angle = Mathf.Deg2Rad * chunkTerrainPrefab.transform.rotation.eulerAngles.x;
        float angledZOffset = chunkSize.z * MathF.Cos(angle);
        float angledYOffset = chunkSize.z * MathF.Sin(angle);

        // Player position centered related to chunks
        Vector3 newTargetPos = new Vector3(
            Mathf.Round(target.position.x / chunkSize.x) * chunkSize.x,
            -Mathf.Round(target.position.z / angledZOffset) * angledYOffset,
            Mathf.Round(target.position.z / angledZOffset) * angledZOffset
        );

        if (newTargetPos == lastUpdatedTargetPos)
        {
            return; 
        }

        HashSet<Vector3Int> chunkKeysToDelete = chunks.Keys.ToHashSet();

        // generate new chunks if necessary
        for (int x = 0; x < worldDiameter; x++)
        {
            float relativeXPosition = (x - offset) * chunkSize.x;
            for (int z = 0; z < worldDiameter; z++)
            {
                float relativeZPosition = (z - offset) * angledZOffset;
                float relativeYPosition = (z - offset) * angledYOffset;

                Vector3 pos = new Vector3(
                    relativeXPosition + newTargetPos.x,
                    newTargetPos.y - relativeYPosition,
                    relativeZPosition + newTargetPos.z
                );
                Vector3Int posKey = new Vector3Int(
                    Mathf.RoundToInt(pos.x / chunkSize.x),
                    Mathf.RoundToInt(pos.y / angledYOffset),
                    Mathf.RoundToInt(pos.z / angledZOffset)
                );

                if (chunks.ContainsKey(posKey))
                {
                    chunkKeysToDelete.Remove(posKey);
                }
                else
                {
                    chunks.Add(posKey, Instantiate(chunkPrefab, pos, Quaternion.identity, transform));
                }
            }
        }
        
        // delete unneded chunks
        foreach (var chunkToDeleteKey in chunkKeysToDelete)
        {
            GameObject chunkToDestroy;
            chunks.Remove(chunkToDeleteKey, out chunkToDestroy);
            Destroy(chunkToDestroy);
        }

        lastUpdatedTargetPos = newTargetPos;

        MergeChunksTerrainMesh();
    }

    void MergeChunksTerrainMesh()
    {
        MeshFilter[] meshFilters = new MeshFilter[chunks.Count];

        int i = 0;
        foreach(var chunk in chunks.Values)
        {
            meshFilters[i] = chunk.GetComponent<ChunkManager>().terrain.GetComponent<MeshFilter>();
            i++;
        }

        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;

            i++;
        }

        Mesh mesh = new Mesh();
        mesh.CombineMeshes(combine);
        meshCollider.sharedMesh = mesh;
    }
}
