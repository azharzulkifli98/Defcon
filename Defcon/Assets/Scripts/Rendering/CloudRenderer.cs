using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer))]
public class CloudRenderer : MonoBehaviour
{
    const int DETAIL_SIZE = 2;

    static PerlinNoiseMap map = null;

    [SerializeField]
    Vector3 minPoint;

    [SerializeField]
    Vector3 maxPoint;

    const float waveLength = 7.46f;

    const float frequency = 0.9f;

    const float amplitude = 0.03f;

    const float offset = .2f;

    const float rollingMod = 0.025f;

    MeshFilter filter;

    private void Start()
    {
        Prime(minPoint, maxPoint);
    }

    public void Prime(Vector3 minPoint, Vector3 maxPoint)
    {
        if(map == null)
        {
            map = new PerlinNoiseMap(13.1531f, .123423f, 3, .4f, .015f, 13, 1.5f);
        }

        this.minPoint = minPoint;
        this.maxPoint = maxPoint;

        Material mat = Resources.Load<Material>("Materials/Fog");

        GetComponent<MeshRenderer>().sharedMaterial = mat;

        filter = GetComponent<MeshFilter>();
    }

    public void Update()
    {
        Render(Time.time);
    }

    public void Render(float time)
    {
        TriangleMap map = new TriangleMap();

        Vector3[,] vertices = new Vector3[DETAIL_SIZE + 1, DETAIL_SIZE + 1];

        for (int x = 0; x <= DETAIL_SIZE; x++)
        {
            for (int y = 0; y <= DETAIL_SIZE; y++)
            {
                float vertX = (minPoint.x * (DETAIL_SIZE - x))/DETAIL_SIZE + maxPoint.x * x / DETAIL_SIZE;
                float vertZ = (minPoint.z * (DETAIL_SIZE - y)) / DETAIL_SIZE + maxPoint.z * y / DETAIL_SIZE;

                float vertY = amplitude * Mathf.Cos(frequency * time + vertX * waveLength) + amplitude * Mathf.Sin(frequency * time + vertZ * waveLength)
                    + offset + CloudRenderer.map.GetPoint(vertX + time * rollingMod, vertZ + time * rollingMod);

                vertices[x, y] = new Vector3(vertX, vertY, vertZ);
            }
        }

        for (int x = 0; x < DETAIL_SIZE; x++)
        {
            for (int y = 0; y < DETAIL_SIZE; y++)
            {
                map.RegisterTriangle(vertices[x + 1, y], vertices[x, y], vertices[x, y + 1]);
                map.RegisterTriangle(vertices[x + 1, y + 1], vertices[x + 1, y], vertices[x, y + 1]);
            }
        }

        filter.sharedMesh = map.GenerateMesh();
    }
}
