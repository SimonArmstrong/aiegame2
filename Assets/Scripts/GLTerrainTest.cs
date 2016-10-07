using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GLTerrainTest
{

    [System.Serializable]
    public class Octave
    {
        public float x, y;
        [HideInInspector]
        public float pNoise;
        public float persitence;
        public float lacinarity;

        public Octave()
        {
            this.x = Mathf.Pow(x, lacinarity);
            this.y = Mathf.Pow(y, lacinarity);
            this.pNoise = Mathf.PerlinNoise(this.x, this.y);
        }
    };
    public Octave[] octaves = new Octave[3];

    public int w, h;
    public List<Vector3> vertPositions = new List<Vector3>();
    public List<Vector3> verts = new List<Vector3>();
    public List<Vector2> uvs = new List<Vector2>();
    public List<Vector3> normals = new List<Vector3>();
    public int[] triangles;
    private int triangleIndex = 0;

    public Mesh mesh;
    public Material mat;

    public static float offset = 0;
    public class GLVert
    {
        public int id;
        public Vector3 position;

        public static implicit operator Vector3(GLVert gv)
        {
            return gv.position;
        }

        public GLVert(Vector3 position, int id)
        {
            this.position = position;
            this.id = id;
        }
    }

    public class GLTri
    {
        public Vector3[] verts = new Vector3[3];

        GLTri(Vector3[] verts)
        {
            if (verts.Length > 3)
            {
                Debug.LogError("The vertices for Triangle " + this + " has too many verts!");
                return;
            }

            this.verts = verts;
        }
    }


    public static void Generate(ref GLTerrainTest terrain, Texture2D heightMap, float amplitude)
    {

        //offset++;
        if (terrain.mesh != null) terrain.mesh.Clear();
        terrain.mesh = new Mesh();
        terrain.mesh.MarkDynamic();

        terrain.triangleIndex = 0;
        terrain.vertPositions.Clear();
        terrain.verts.Clear();

        terrain.triangles = new int[(terrain.w - 1) * (terrain.h - 1) * 6];
        int h = 0;
        for (int x = 0; x < terrain.w; x++)
        {
            for (int y = 0; y < terrain.h; y++)
            {
                terrain.vertPositions.Add(new Vector3(x, heightMap.GetPixel(x + (int)offset, y + (int)offset / 2).grayscale * amplitude, y));

                if ((x > 0 && y > 0) && (x < terrain.w && y < terrain.h))
                {
                    terrain.verts.Add(terrain.vertPositions[h]);
                    terrain.verts.Add(terrain.vertPositions[h]);
                    terrain.verts.Add(terrain.vertPositions[h]);
                    terrain.verts.Add(terrain.vertPositions[h]);
                }
                else if (x > 0 && y < 1 && !(x == terrain.w))
                {
                    terrain.verts.Add(terrain.vertPositions[h]);
                    terrain.verts.Add(terrain.vertPositions[h]);
                }
                else if (y > 0 && x < 1 && !(y == terrain.h))
                {
                    terrain.verts.Add(terrain.vertPositions[h]);
                    terrain.verts.Add(terrain.vertPositions[h]);
                }
                else
                {
                    terrain.verts.Add(terrain.vertPositions[h]);
                }
                //terrain.normals.Add(-Vector3.up);
                //terrain.normals.Add(new Vector3(i, Mathf.PerlinNoise(i, j), j));
                //terrain.normals.Add(new Vector3(i, Mathf.PerlinNoise(i, j), j));
                //terrain.uvs.Add(new Vector3(i, 0, j));
                if (x < terrain.w - 1 && y < terrain.h - 1)
                {
                    //GLTri tri = new GLTri()
                }
                h++;
            }
        }
        terrain.mesh.vertices = terrain.verts.ToArray();

        terrain.normals.AddRange(terrain.mesh.normals);

        terrain.mesh.normals = terrain.normals.ToArray();
        terrain.mesh.triangles = terrain.triangles;
        terrain.mesh.RecalculateNormals();
        terrain.mesh.uv = terrain.uvs.ToArray();
        if (!terrain.mat) terrain.mat = Resources.Load<Material>("default");
    }

    public void AddTriangles(int a, int b, int c)
    {
        triangles[triangleIndex] = a;
        triangles[triangleIndex + 1] = b;
        triangles[triangleIndex + 2] = c;
        triangleIndex += 3;
    }

    public void Draw()
    {
    }
}
