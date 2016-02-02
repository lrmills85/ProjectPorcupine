using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[ExecuteInEditMode]
public class HoverHex : MonoBehaviour
{
    public Vector2 hexPosition;
    public Material material;

    Vector3[] _vertices;
    Vector3[] _normals;
    Vector2[] _uv;
    int[] _triangles;

    void Start()
    {
        //create vertices and uv array size
        _vertices = new Vector3[24];
        _uv = new Vector2[24];

        //populate hex arrays
        DrawTopAndBottom();
        DrawSides();
        SetTriangles();

        //setup mesh
        Mesh mesh = new Mesh { name = "Hex Mesh" };
        mesh.vertices = _vertices;
        mesh.uv = _uv;
        mesh.triangles = _triangles;
        mesh.normals = _normals;
        GetComponent<MeshFilter>().mesh = mesh;

        //setup renderer
        Renderer renderer = GetComponent<Renderer>();
        renderer.material = material;
    }

    private void SetTriangles()
    {
        _normals = new Vector3[]
            {
                new Vector3(0, 1, 0),
                new Vector3(0, 1, 0),
                new Vector3(0, 1, 0),
                new Vector3(0, 1, 0),
                new Vector3(0, 1, 0),
                new Vector3(0, 1, 0),

                new Vector3(1, 0, 0),
                new Vector3(1, 0, 1),
                new Vector3(-1, 0, 1),
                new Vector3(-1, 0, 0),
                new Vector3(-1, 0, -1),
                new Vector3(1, 0, -1),

                new Vector3(1, 0, 0),
                new Vector3(1, 0, 1),
                new Vector3(-1, 0, 1),
                new Vector3(-1, 0, 0),
                new Vector3(-1, 0, -1),
                new Vector3(1, 0, -1),

                new Vector3(0, -1, 0),
                new Vector3(0, -1, 0),
                new Vector3(0, -1, 0),
                new Vector3(0, -1, 0),
                new Vector3(0, -1, 0),
                new Vector3(0, -1, 0),
            };

        _triangles = new int[]
            {
                1, 0, 5, 2, 4, 3, 2, 1, 4, 1, 5, 4,
                7, 12, 6, 7, 13, 12,
                8, 13, 7, 8, 14, 13,
                9, 14, 8, 9, 15, 14,
                10, 15, 9, 10, 16, 15,
                11, 16, 10, 11, 17, 16,
                6, 17, 11, 6, 12, 17,
                19, 23, 18, 20, 21, 22, 20, 23, 19, 20, 22, 23
            };
    }

    void DrawTopAndBottom()
    {
        //top
        _vertices[0] = new Vector3(0, 0, -World.HexRadius);
        _uv[0] = new Vector2(0.5f, 1);
        //topright
        _vertices[1] = new Vector3(World.HexHalfWidth, 0, -World.HexRadius / 2);
        _uv[1] = new Vector2(1, 0.75f);
        //bottomright
        _vertices[2] = new Vector3(World.HexHalfWidth, 0, World.HexRadius / 2);
        _uv[2] = new Vector2(1, 0.25f);
        //bottom
        _vertices[3] = new Vector3(0, 0, World.HexRadius);
        _uv[3] = new Vector2(0.5f, 0);
        //bottomleft
        _vertices[4] = new Vector3(-World.HexHalfWidth, 0, World.HexRadius / 2);
        _uv[4] = new Vector2(0, 0.25f);
        //topleft
        _vertices[5] = new Vector3(-World.HexHalfWidth, 0, -World.HexRadius / 2);
        _uv[5] = new Vector2(0, 0.75f);

        //top
        _vertices[18] = new Vector3(0, -1, -World.HexRadius);
        _uv[18] = new Vector2(0.5f, 1);
        //topright
        _vertices[19] = new Vector3(World.HexHalfWidth, -1, -World.HexRadius / 2);
        _uv[19] = new Vector2(1, 0.75f);
        //bottomright
        _vertices[20] = new Vector3(World.HexHalfWidth, -1, World.HexRadius / 2);
        _uv[20] = new Vector2(1, 0.25f);
        //bottom
        _vertices[21] = new Vector3(0, -1, World.HexRadius);
        _uv[21] = new Vector2(0.5f, 0);
        //bottomleft
        _vertices[22] = new Vector3(-World.HexHalfWidth, -1, World.HexRadius / 2);
        _uv[22] = new Vector2(0, 0.25f);
        //topleft
        _vertices[23] = new Vector3(-World.HexHalfWidth, -1, -World.HexRadius / 2);
        _uv[23] = new Vector2(0, 0.75f);
    }

    void DrawSides()
    {
        //top
        _vertices[6] = new Vector3(0, 0, -World.HexRadius);
        _uv[6] = new Vector2(0.5f, 1);
        //topright
        _vertices[7] = new Vector3(World.HexHalfWidth, 0, -World.HexRadius / 2);
        _uv[7] = new Vector2(1, 0.75f);
        //bottomright
        _vertices[8] = new Vector3(World.HexHalfWidth, 0, World.HexRadius / 2);
        _uv[8] = new Vector2(1, 0.25f);
        //bottom
        _vertices[9] = new Vector3(0, 0, World.HexRadius);
        _uv[9] = new Vector2(0.5f, 0);
        //bottomleft
        _vertices[10] = new Vector3(-World.HexHalfWidth, 0, World.HexRadius / 2);
        _uv[10] = new Vector2(0, 0.25f);
        //topleft
        _vertices[11] = new Vector3(-World.HexHalfWidth, 0, -World.HexRadius / 2);
        _uv[11] = new Vector2(0, 0.75f);

        //top
        _vertices[12] = new Vector3(0, -1, -World.HexRadius);
        _uv[12] = new Vector2(0, 0.75f);
        //topright
        _vertices[13] = new Vector3(World.HexHalfWidth, -1, -World.HexRadius / 2);
        _uv[13] = new Vector2(0.5f, 1);
        //bottomright
        _vertices[14] = new Vector3(World.HexHalfWidth, -1, World.HexRadius / 2);
        _uv[14] = new Vector2(1, 0.75f);
        //bottom
        _vertices[15] = new Vector3(0, -1, World.HexRadius);
        _uv[15] = new Vector2(1, 0.25f);
        //bottomleft
        _vertices[16] = new Vector3(-World.HexHalfWidth, -1, World.HexRadius / 2);
        _uv[16] = new Vector2(0.5f, 0);
        //topleft
        _vertices[17] = new Vector3(-World.HexHalfWidth, -1, -World.HexRadius / 2);
        _uv[17] = new Vector2(0, 0.25f);
    }
}
