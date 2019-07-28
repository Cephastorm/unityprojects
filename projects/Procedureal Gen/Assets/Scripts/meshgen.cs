using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
[ExecuteAlways]
public class meshgen : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] tris;
    public int xsize = 20;
    public int zsize = 20;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateShape();
        UpdateMesh();


    }

    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = tris;
        mesh.RecalculateNormals();
    }

    // Update is called once per frame
    void CreateShape()
    {

        vertices = new Vector3[(xsize + 1) * (zsize + 1)];
        for (int z = 0, index = 0; z <= zsize; z++)
        {
            for (int x = 0; x <= xsize; x++)
            {
                float y = Mathf.PerlinNoise(x * .3f, z * 0.3f) * 2f;
                vertices[index] = new Vector3(x, y, z);
                index++;
            }
        }
        int vert = 0;
        int triangles = 0;
        tris = new int[zsize * xsize * 6];
        for (int z = 0; z < zsize; z++)
        {
            for (int x = 0; x < xsize; x++)
            {
                tris[triangles] = vert;
                tris[triangles + 1] = vert + xsize + 1;
                tris[triangles + 2] = vert + 1;
                tris[triangles + 3] = vert + 1;
                tris[triangles + 4] = vert + xsize + 1;
                tris[triangles + 5] = vert + xsize + 2;
                vert++;
                triangles += 6;
            }
            vert++;
        }



    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
}

