using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public Transform target;
    public GameObject chunkTerrainPrefab;
    public int worldDiameter = 3;

    private Dictionary<Vector3, GameObject> chunks = new Dictionary<Vector3, GameObject>();
    private Vector3 chunkSize;
    private float offset;
    private Vector3 lastUpdatedTargetPos = Vector3.one;

    void Start()
    {
        Vector3 ogChunkSize = chunkTerrainPrefab.GetComponent<MeshRenderer>().localBounds.size;
        Vector3 ogChunkScale = chunkTerrainPrefab.transform.localScale;

        chunkSize = new Vector3(ogChunkSize.x * ogChunkScale.x, ogChunkSize.y * ogChunkScale.y, ogChunkSize.z * ogChunkScale.z);
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

        HashSet<Vector3> chunkKeysToDelete = chunks.Keys.ToHashSet();

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

                if (!chunks.ContainsKey(pos))
                {
                    chunks.Add(pos, Instantiate(chunkTerrainPrefab, pos, chunkTerrainPrefab.transform.rotation, transform));
                }
                else
                {
                    chunkKeysToDelete.Remove(pos);
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
    }
}
