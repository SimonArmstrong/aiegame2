using UnityEngine;
using System.Collections;

public class TerrainGenerator : MonoBehaviour {
    public GLTerrain terrain;
    public Texture2D heightMap;
    public HeightMapPainter painter;
    public float amplitude = 5;

    void Start() {
        heightMap = painter.heightMap;
    }

    private void FixedUpdate() {
        GLTerrain.Generate(ref terrain, heightMap, amplitude);
        GetComponent<MeshFilter>().mesh = terrain.mesh;
        GetComponent<MeshRenderer>().material = terrain.mat;
    }

    void OnPostRender() {
        //terrain.Draw();
    }
}
