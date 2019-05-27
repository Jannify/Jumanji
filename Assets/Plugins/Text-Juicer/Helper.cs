using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class Helper : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public bool active;
    public float var = 1;
    public Color32 color;
    Mesh mesh;
    int charCount;

    void Start()
    {
        if(textComponent == null) textComponent = GetComponent<TextMeshProUGUI>();
        textComponent.enableWordWrapping = true;
        textComponent.ForceMeshUpdate();
    }

    private void UpdateMesh()
    {
        charCount = textComponent.textInfo.characterCount;
        mesh = new Mesh();
        mesh.Clear();
        Mesh tmpMesh = textComponent.textInfo.meshInfo[0].mesh;
        mesh.vertices = tmpMesh.vertices;
        mesh.triangles = tmpMesh.triangles;
        mesh.uv = tmpMesh.uv;
        mesh.normals = tmpMesh.normals;
        mesh.colors = tmpMesh.colors;
        mesh.tangents = tmpMesh.tangents;
        Debug.Log("Updated Mesh");
    }

    void Update()
    {
        if (textComponent?.textInfo?.meshInfo?[0].mesh != null)
        {
            if (mesh == null || charCount != textComponent.textInfo.characterCount) UpdateMesh();

            for (var i = 0; i < mesh?.vertices?.Length; i++)
            {
                textComponent.textInfo.meshInfo[0].vertices[i].x = mesh.vertices[i].x * var;
            }

            for (int i = 0; i < textComponent.textInfo.meshInfo.Length; i++)
            {
                textComponent.textInfo.meshInfo[i].mesh.normals = textComponent.textInfo.meshInfo[i].normals;
                textComponent.textInfo.meshInfo[i].mesh.tangents = textComponent.textInfo.meshInfo[i].tangents;
                textComponent.textInfo.meshInfo[i].mesh.triangles = textComponent.textInfo.meshInfo[i].triangles;
                textComponent.textInfo.meshInfo[i].mesh.vertices = textComponent.textInfo.meshInfo[i].vertices;
                textComponent.textInfo.meshInfo[i].mesh.uv = textComponent.textInfo.meshInfo[i].uvs0;
                textComponent.textInfo.meshInfo[i].mesh.uv2 = textComponent.textInfo.meshInfo[i].uvs2;
                textComponent.textInfo.meshInfo[i].mesh.colors32 = textComponent.textInfo.meshInfo[i].colors32;

                textComponent.UpdateGeometry(textComponent.textInfo.meshInfo[i].mesh, i);
            }
            //textComponent.textInfo.meshInfo[0].mesh.RecalculateBounds();
        }
    }
}
