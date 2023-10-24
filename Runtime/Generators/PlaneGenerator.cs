using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlaneGenerator : MeshGenerator {
    [SerializeField]
    public float width = 1;
    public float height = 1;
    protected override void CreateGeometry() {
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        for (int i = 0; i < 4; i++) {
            vertices.Add(new Vector3((i % 2) * width, 0, (i / 2) * height));
        }

        triangles.Add(0);
        triangles.Add(2);
        triangles.Add(1);
        triangles.Add(1);
        triangles.Add(2);
        triangles.Add(3);

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
    }

}
