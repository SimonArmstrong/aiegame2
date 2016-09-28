using UnityEngine;
using System.Collections;

public class TerrainGenerator : MonoBehaviour {
    public GLTerrain terrain;
    public Texture2D heightMap;

    private void Start() {
        GLTerrain.Generate(ref terrain, heightMap);
        GetComponent<MeshFilter>().mesh = terrain.mesh;
    }

    void OnPostRender() {
        //terrain.Draw();
    }
}
