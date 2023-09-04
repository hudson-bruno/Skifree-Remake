using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class ChunkManager : MonoBehaviour
{
    public float minimumDistance = 2f;
    public float padding = 3;
    public GameObject[] staticProps;
    public GameObject[] dinamicProps;
    public GameObject terrain;

    void Start()
    {
        MeshRenderer meshRenderer = terrain.GetComponent<MeshRenderer>();

        Vector2 topRight = new Vector2(
            meshRenderer.localBounds.max.x * meshRenderer.transform.localScale.x - padding,
            meshRenderer.localBounds.max.z * meshRenderer.transform.localScale.z - padding
        );
        Vector2 bottomLeft = new Vector2(
            meshRenderer.localBounds.min.x * meshRenderer.transform.localScale.x + padding,
            meshRenderer.localBounds.min.z * meshRenderer.transform.localScale.z + padding
        );

        Vector2[] projectedPoints = FastPoissonDiskSampling.Sampling(bottomLeft, topRight, minimumDistance).ToArray();

        for (int i = 0; i < projectedPoints.Length; i++)
        {
            Vector3 pos = new Vector3(
                projectedPoints[i].x,
                -Mathf.Sin(terrain.transform.rotation.eulerAngles.x * Mathf.Deg2Rad) * projectedPoints[i].y,
                Mathf.Cos(terrain.transform.rotation.eulerAngles.x * Mathf.Deg2Rad) * projectedPoints[i].y
            ) + transform.position;

            int randomIndex = Random.Range(0, staticProps.Length + dinamicProps.Length);
            if (randomIndex < staticProps.Length)
            {
                GameObject prop = staticProps[randomIndex];
                Instantiate(prop, pos, prop.transform.rotation, transform);
            }
            else
            {
                GameObject prop = dinamicProps[randomIndex - staticProps.Length]; 
                Instantiate(prop, pos, prop.transform.rotation);
            }
        }
    }
}
