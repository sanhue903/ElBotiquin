using UnityEngine;

public class DrawMesh : MonoBehaviour
{

    Mesh mesh;
    private void Awake()
    {
        mesh = new Mesh();
    }
    
    void Update()
    {

        Vector3[] vertices = new Vector3[4]
        {
            new Vector3(-1, +1),
            new Vector3(-1, -1),
            new Vector3(+1, -1),
            new Vector3(+1, +1)
        };

        Vector2[] uv = new Vector2[4]
        {
            Vector2.zero,
            Vector2.zero,
            Vector2.zero,
            Vector2.zero
        };

        int[] triangles = new int[6]
        {
            0, 3, 1,
            1, 3, 2
        };

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.MarkDynamic();

        GetComponent<MeshFilter>().mesh = mesh;

        transform.position = transform.parent.position; 
 
    }
}