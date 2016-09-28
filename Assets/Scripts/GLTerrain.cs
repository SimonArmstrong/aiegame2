﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GLTerrain {

    [System.Serializable]
    public class Octave {
        public float x, y;
        [HideInInspector]
        public float pNoise;
        public float persitence;
        public float lacinarity;

        public Octave() {
            this.x = Mathf.Pow(x, lacinarity);
            this.y = Mathf.Pow(y, lacinarity);
            this.pNoise = Mathf.PerlinNoise(this.x, this.y);
        }
    };
    public Octave[] octaves = new Octave[3];

    public int w, h;
    public List<Vector3> verts = new List<Vector3>();
    public List<Vector2> uvs = new List<Vector2>();
    public List<Vector3> normals = new List<Vector3>();
    public int[] triangles;
    private int triangleIndex = 0;

    public Mesh mesh;
    public Material mat;

    public static void Generate(ref GLTerrain terrain, Texture2D heightMap) {

        terrain.triangles = new int[(terrain.w-1)*(terrain.h-1)*6];
        int h = 0;
        for (int i = 0; i < terrain.w; i++) {
            for (int j = 0; j < terrain.h; j++) {
                terrain.verts.Add(new Vector3(i, heightMap.GetPixel(i, j).grayscale * 10, j));
                //terrain.normals.Add(new Vector3(i, Mathf.PerlinNoise(i, j), j));
                //terrain.normals.Add(new Vector3(i, Mathf.PerlinNoise(i, j), j));
                //terrain.uvs.Add(new Vector3(i, 0, j));
                if (i < terrain.w - 1 && j < terrain.h - 1)
                {
                    terrain.AddTriangles(h, h + terrain.w + 1, h + terrain.w);
                    terrain.AddTriangles(h + terrain.w + 1, h, h + 1);
                }
                h++;
            }
        }

        terrain.mesh = new Mesh();

        terrain.mesh.vertices = terrain.verts.ToArray();
        //terrain.mesh.normals = terrain.normals.ToArray();
        terrain.mesh.uv = terrain.uvs.ToArray();
        terrain.mesh.triangles = terrain.triangles;
        terrain.mesh.SetNormals(terrain.verts);
        terrain.mesh.RecalculateNormals();
        terrain.mat = Resources.Load<Material>("default");
    }

    public void AddTriangles(int a, int b, int c) {
        triangles[triangleIndex]     = a;
        triangles[triangleIndex + 1] = b;
        triangles[triangleIndex + 2] = c;
        triangleIndex += 3;
    }

    public void Draw() {
    }
}