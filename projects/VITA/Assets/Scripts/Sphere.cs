using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[ExecuteAlways]
[RequireComponent(typeof(MeshFilter))]
public class Sphere : MonoBehaviour
{
    public const float PI = 3.141592653589793238462643383279f;
    public float radius = 5;
    public int xspan = 50;
    public int yspan = 50;
    public GameObject obj;
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    bool isCreated = false;

    // Start is called before the first frame update
    void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        //UpdateMesh();
        Debug.Log("Awake");
        CreateShape();
        isCreated = true;

        //Debug.Log(vertices[0 + yspan]);
        //UpdateMeshNow();

        //Create spheres to visualize:

        //-------------------------------------

    }

    private void CreateShape()
    {

        triangles = new int[xspan * yspan * 6];
        vertices = new Vector3[xspan * yspan];

        /*   for (int i = 1; i < xspan + 1; i++)
          {
               float ref_x = Mathf.Cos((2 * PI / 50) * i) * radius;
                float ref_z = Mathf.Sin((2 * PI / 50) * i) * radius; 
              // vertices[i - 1] = new Vector3(x, 0, z);
              //Debug.DrawLine(Vector3.zero, vertices[i - 1], Color.green);
          } */
        // return
        for (int indx = 1; indx < xspan / 2 + 1; indx++)
        {

            float ref_x = Mathf.Cos((2 * PI / xspan) * indx) * radius;
            float ref_z = Mathf.Sin((2 * PI / xspan) * indx) * radius;
            Vector3 refVector = new Vector3(ref_x, 0, ref_z);
            for (int i = 1; i < yspan + 1; i++)
            {

                try
                {
                    int currentVert = yspan * (indx - 1) + i - 1;

                    // float rad = (Vector3.Project(vertices[(indx - 1)], Vector3.left) - vertices[indx - 1]).magnitude;
                    float rad = (Vector3.Project(refVector, Vector3.left) - refVector).magnitude;
                    float x = Mathf.Cos((2 * Mathf.PI / yspan) * i) * rad;
                    float z = Mathf.Sin((2 * Mathf.PI / yspan) * i) * rad;
                    vertices[currentVert] = new Vector3(refVector.x, x, z);

                }
                catch (IndexOutOfRangeException e)
                {
                    Debug.Log(e.Message);
                }
            }



        }


    }

    IEnumerator UpdateTris()
    {
        int tris = 0;
        for (int i = 0; i < xspan; i++)
        {
            for (int j = 0; j < yspan; j++)
            {
                int vert = yspan * i + j;
                triangles[tris + 0] = vert;
                triangles[tris + 1] = vert + yspan;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + yspan;
                triangles[tris + 5] = 1 + vert + yspan;

                tris += 6;

                mesh.Clear();
                mesh.vertices = vertices;
                mesh.triangles = triangles;
                mesh.RecalculateNormals();
                yield return null;

            }
   
        }
    }
    public void UpdateMeshNow()
    {
        if (vertices == null)
        {
            return;
        }
        int i = 0;
        foreach (Vector3 v in vertices)
        {
            var instance = Instantiate(obj, v, Quaternion.identity, this.transform);
            instance.GetComponent<sphere_point>().index = i;
            i++;
        }
        
        /*    mesh.Clear();
           mesh.vertices = vertices;
           mesh.triangles = triangles;
           mesh.RecalculateNormals(); 
           */

    }
    public void DeleteSpherePoints()
    {

        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i));
        }

    }
    public void DestroyAllObjectsOfTag(string tag)
    {
        var gameObjects = GameObject.FindGameObjectsWithTag(tag);

        for (var i = 0; i < gameObjects.Length; i++)
        {
            DestroyImmediate(gameObjects[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Update ran");
        StartCoroutine("UpdateTris");
    }
    private void OnDrawGizmosSelected()
    {
        if (isCreated)
        {
            foreach (Vector3 v in vertices)
            {
                //Gizmos.DrawSphere(v, 0.02f);
            }
        }
        else
        {
            return;
        }

    }
}
