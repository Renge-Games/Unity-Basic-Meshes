using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MeshType {
    Plane,
    Cube,
    Sphere
}

[ExecuteAlways]
public class MeshGeneratorComponent : MonoBehaviour {
    bool _isMeshDirty = false;

    [SerializeField]
    private MeshType meshType = MeshType.Plane;
    [SerializeReference]
    private MeshGenerator generator;
    [Space]
    public string assetName = "MyMesh";
    public bool bakeMesh = false;

    private void Start() {
        ValidateComponents();
    }

    private void ValidateComponents() {
        if(GetComponent<MeshFilter>() == null) {
            var comp = gameObject.AddComponent<MeshFilter>();
#if UNITY_EDITOR
            UnityEditorInternal.InternalEditorUtility.SetIsInspectorExpanded(comp, false);
#endif
        }
        if(GetComponent<MeshRenderer>() == null) {
            var comp = gameObject.AddComponent<MeshRenderer>();
            comp.sharedMaterial = new Material(Shader.Find("Standard"));
#if UNITY_EDITOR
            UnityEditorInternal.InternalEditorUtility.SetIsInspectorExpanded(comp, false);
#endif
        }
    }

    private void OnValidate() {
        ValidateComponents();
        switch (meshType) {
            case MeshType.Plane:
            case MeshType.Cube:
            case MeshType.Sphere:
                if (generator == null || generator.GetType() != typeof(PlaneGenerator))
                    generator = new PlaneGenerator();
                break;
            default:
                break;
        }
        SetMeshDirty();
    }

    private void SetMeshDirty() {
        _isMeshDirty = true;
    }

    private void Update() {
        if (_isMeshDirty) {
            _isMeshDirty = false;
            generator.Generate(GetComponent<MeshFilter>());
        }
#if UNITY_EDITOR
        if(bakeMesh) {
            bakeMesh = false;
            generator.Generate(GetComponent<MeshFilter>());
            BakeMesh();
        }
#endif
    }

#if UNITY_EDITOR
    private void BakeMesh() {
        //get assets folder path and save mesh to it
        var mesh = generator.GetMesh();
        string path = $"Assets/{assetName}.asset";
        path = UnityEditor.AssetDatabase.GenerateUniqueAssetPath(path);
        UnityEditor.AssetDatabase.CreateAsset(CloneMesh(mesh), path);
    }
#endif

    Mesh CloneMesh(Mesh mesh) {
        Mesh newMesh = new Mesh();
        newMesh.vertices = mesh.vertices;
        newMesh.triangles = mesh.triangles;
        newMesh.uv = mesh.uv;
        newMesh.normals = mesh.normals;
        newMesh.colors = mesh.colors;
        newMesh.tangents = mesh.tangents;
        return newMesh;
    }

    private void OnDrawGizmosSelected() {
        
    }
}
