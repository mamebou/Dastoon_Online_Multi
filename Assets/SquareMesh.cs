using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Es.InkPainter;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class SquareMesh : MonoBehaviour
{

    void Start()
    {
  
    }

    public void MakePlane(Vector3[] size)
    {
        Material mat = (Material)Resources.Load("whiteboard");
        Mesh mesh = new Mesh();
        //Quaternion rot = Quaternion.Euler(90.0f, 0.0f, 0.0f);
        //for (int i = 0; i < size.Count; i++)
        //{
        //    size[i] = rot * vertices[i];
        //    uvs[i] = rot * uvs[i];
        //}
        mesh.vertices = size;
        //mesh.vertices = new Vector3[] {
        //new Vector3 (-1.78f, -0.71f, 0),
        //new Vector3 (-1.78f,  0.71f, 0),
        //new Vector3 (1.78f , -0.71f, 0),
        //new Vector3 (1.78f ,  0.71f, 0),
        //};

        mesh.uv = new Vector2[] {
        new Vector2 (0, 0),
        new Vector2 (0, 1),
        new Vector2 (1, 0),
        new Vector2 (1, 1),
        };

        mesh.triangles = new int[] {
        0, 1, 2,
        1, 3, 2,
        };
        GetComponent<MeshFilter>().sharedMesh = mesh;
        GetComponent<MeshRenderer>().material = mat;
        GetComponent<MeshCollider>().sharedMesh = mesh;

        InkCanvas.PaintSet paintSet = new InkCanvas.PaintSet()
        {
            mainTextureName = "_MainTex",
            normalTextureName = "_BumpMap",
            heightTextureName = "_ParallaxMap",
            useMainPaint = true,
            useNormalPaint = false,
            useHeightPaint = false,
        };
        GameObjectExtension.AddInkCanvas(this.gameObject, paintSet);
    }
}
