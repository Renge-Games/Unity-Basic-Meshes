using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlaneGenerator : MeshGenerator {
    [Min(1)] public int widthSegments = 1;
    [Min(1)] public int heightSegments = 1;
    public float width = 1;
    public float height = 1;
    protected override void CreateGeometry() {
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uv = new List<Vector2>();

        //generate plane vertices and uvs
        for (int i = 0; i < widthSegments + 1; i++) {
            for (int j = 0; j < heightSegments + 1; j++) {
                vertices.Add(new Vector3(i * width / widthSegments, 0, j * height / heightSegments));
                uv.Add(new Vector2(i / (float)widthSegments, j / (float)heightSegments));
            }
        }

        //generate plane triangles
        for (int i = 0; i < widthSegments; i++) {
            for (int j = 0; j < heightSegments; j++) {
                triangles.Add(i * (heightSegments + 1) + j);
                triangles.Add(i * (heightSegments + 1) + j + 1);
                triangles.Add((i + 1) * (heightSegments + 1) + j);

                triangles.Add((i + 1) * (heightSegments + 1) + j);
                triangles.Add(i * (heightSegments + 1) + j + 1);
                triangles.Add((i + 1) * (heightSegments + 1) + j + 1);
            }
        }

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uv.ToArray();
        mesh.RecalculateNormals();
    }

}
