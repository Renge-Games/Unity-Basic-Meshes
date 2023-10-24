using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class MeshGenerator {
    protected Mesh mesh;
    [SerializeField]
    protected float vertexCount;
    public void Generate(MeshFilter meshFilter) {
        if (mesh == null) {
            mesh = new Mesh();
            meshFilter.sharedMesh = mesh;
        } else {
            mesh.Clear();
        }
        CreateGeometry();

        vertexCount = mesh.vertexCount;
    }
    public Mesh GetMesh() {
        return mesh;
    }

    protected abstract void CreateGeometry();
}
