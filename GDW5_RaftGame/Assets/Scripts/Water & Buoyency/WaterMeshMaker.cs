using System.Collections;
using UnityEngine;


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class WaterMeshMaker : MonoBehaviour
{
    [SerializeField]int innerResolution = 128;   // High density near center
    [SerializeField]int outerResolution = 32;    // Low density at far edges
    [SerializeField]float radius = 50f;          // Radius of dense region
    [SerializeField] float maxDistance = 200f;    // Extent of plane

    MeshFilter _meshFilter;
    MeshRenderer _meshRenderer;


    void Start()
    {
        Mesh mesh = GenerateMesh();
        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshFilter.mesh = mesh;
        _meshRenderer.material = WaveManager.instance.DefaultWaterMaterial;

    }




    Mesh GenerateMesh()
    {

        var verticesList = new System.Collections.Generic.List<Vector3>();
        var uvsList = new System.Collections.Generic.List<Vector2>();
        var trianglesList = new System.Collections.Generic.List<int>();

        // create radial grid
        for (int y = 0; y < innerResolution; y++)
        {
            for (int x = 0; x < innerResolution; x++)
            {
                // map x,y to [-1,1] range
                float fx = (float)x / (innerResolution - 1) * 2f - 1f;
                float fy = (float)y / (innerResolution - 1) * 2f - 1f;

                // compute radial distance 
                float distance = new Vector2(fx, fy).magnitude;

                // interpolate position outward 
                float scaledDistance = Mathf.Lerp(radius, maxDistance, distance);

                // position on plane
                Vector3 pos = new Vector3(fx * scaledDistance, 0, fy * scaledDistance);

                verticesList.Add(pos);
                uvsList.Add(new Vector2((float)x / innerResolution, (float)y / innerResolution));
            }
        }

        // generate triangles for grid
        int res = innerResolution;
        for (int y = 0; y < res - 1; y++)
        {
            for (int x = 0; x < res - 1; x++)
            {
                int i = y * res + x;
                trianglesList.Add(i);
                trianglesList.Add(i + res);
                trianglesList.Add(i + res + 1);

                trianglesList.Add(i);
                trianglesList.Add(i + res + 1);
                trianglesList.Add(i + 1);
            }
        }

        // create mesh
        Mesh mesh = new Mesh();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32; // Allows >65k verts
        mesh.SetVertices(verticesList);
        mesh.SetUVs(0, uvsList);
        mesh.SetTriangles(trianglesList, 0);
        mesh.RecalculateNormals();

        return mesh;
    }
}
