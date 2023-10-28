using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Render : MonoBehaviour
{
    int pointsCount;

    string terrainPath = @"D:\Github Clones\VSIM-Folder\VSIMFolder\Assets\Height Data\terrain.txt";

    GraphicsBuffer meshTriangles;
    GraphicsBuffer meshPositions;

    [SerializeField] Material material;
    [SerializeField] Mesh mesh;

    void Start() {
        pointsCount = File.ReadLines(terrainPath).Count();

        List<Vector3> points = new List<Vector3>();

        StreamReader read = new StreamReader(terrainPath);

        string line;

        for (int i = 0; i < pointsCount; i++)
        {   
            line = read.ReadLine();
            List<String> pointValues = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList<string>();
            
            if (pointValues.Count() != 3)
            {
                continue;
            }
            Vector3 p = new Vector3(float.Parse(pointValues[0]),
                                    float.Parse(pointValues[1]),
                                    float.Parse(pointValues[2]));
            points.Add(p);
            Debug.Log(p);
        }

        /*
        Code below obtained form Unity's documentation on RenderPrimitives
        https://docs.unity3d.com/ScriptReference/Graphics.RenderPrimitives.html
        */
        meshTriangles = new GraphicsBuffer(GraphicsBuffer.Target.Structured, mesh.triangles.Length, sizeof(int));
        meshTriangles.SetData(mesh.triangles);
        meshPositions = new GraphicsBuffer(GraphicsBuffer.Target.Structured, mesh.vertices.Length, 3 * sizeof(float));
        meshPositions.SetData(mesh.vertices);
    }


    /*
    Function obtained form Unity's documentation on RenderPrimitives
    https://docs.unity3d.com/ScriptReference/Graphics.RenderPrimitives.html
    */
    void OnDestroy()
    {
        meshTriangles?.Dispose();
        meshTriangles = null;
        meshPositions?.Dispose();
        meshPositions = null;
    }

    void Update()
    {
        RenderParams rp = new RenderParams(material);
        rp.worldBounds = new Bounds(Vector3.zero, 10000*Vector3.one); // use tighter bounds
        rp.matProps = new MaterialPropertyBlock();
        rp.matProps.SetBuffer("_Triangles", meshTriangles);
        rp.matProps.SetBuffer("_Positions", meshPositions);
        rp.matProps.SetInt("_StartIndex", (int)mesh.GetIndexStart(0));
        rp.matProps.SetInt("_BaseVertexIndex", (int)mesh.GetBaseVertex(0));
        rp.matProps.SetMatrix("_ObjectToWorld", Matrix4x4.Translate(new Vector3(-4.5f, 0, 0)));
        rp.matProps.SetFloat("_NumInstances", 10.0f);
        Graphics.RenderPrimitives(rp, MeshTopology.Triangles, (int)mesh.GetIndexCount(0), 10);
    }
}
