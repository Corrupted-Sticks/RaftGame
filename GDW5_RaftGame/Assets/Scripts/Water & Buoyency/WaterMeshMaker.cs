using UnityEngine;


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class WaterMeshMaker : MonoBehaviour
{
    public int innerResolution = 128;   // High density near center
    public int outerResolution = 32;    // Low density at far edges
    public float radius = 50f;          // Radius of dense region
    public float maxDistance = 200f;    // Extent of plane

    void Start()
    {
        Mesh mesh = GenerateMesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    Mesh GenerateMesh()
    {
        // We'll build a square grid from -maxDistance to +maxDistance
        // Vertex spacing increases with distance.

        // Pre-calculate vertex positions dynamically
        // Nonlinear interpolation of steps
        var verticesList = new System.Collections.Generic.List<Vector3>();
        var uvsList = new System.Collections.Generic.List<Vector2>();
        var trianglesList = new System.Collections.Generic.List<int>();

        // Create a radial grid
        for (int y = 0; y < innerResolution; y++)
        {
            for (int x = 0; x < innerResolution; x++)
            {
                // Map x,y to [-1,1] range
                float fx = (float)x / (innerResolution - 1) * 2f - 1f;
                float fy = (float)y / (innerResolution - 1) * 2f - 1f;

                // Compute radial distance for spacing
                float distance = new Vector2(fx, fy).magnitude;

                // Interpolate position outward nonlinearly
                float scaledDistance = Mathf.Lerp(radius, maxDistance, distance);

                // Position on plane
                Vector3 pos = new Vector3(fx * scaledDistance, 0, fy * scaledDistance);

                verticesList.Add(pos);
                uvsList.Add(new Vector2((float)x / innerResolution, (float)y / innerResolution));
            }
        }

        // Generate triangles for grid
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

        // Create mesh
        Mesh mesh = new Mesh();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32; // Allows >65k verts
        mesh.SetVertices(verticesList);
        mesh.SetUVs(0, uvsList);
        mesh.SetTriangles(trianglesList, 0);
        mesh.RecalculateNormals();

        return mesh;
    }
}
