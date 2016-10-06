using UnityEngine;
using System.Collections;

public class HeightMapPainter : MonoBehaviour {
    public Texture2D heightMap;
    public float size = 1;
    public Color color = Color.black;
    public Vector2 textureCoord;
    
    public GameObject PainterObject;

    private void Start() {
        heightMap = new Texture2D(256, 256);
        /*
        for (int i = 0; i < heightMap.width; i++) {
            for (int j = 0; j < heightMap.height; j++) {
                heightMap.SetPixel(i, j, Color.black);
            }
        }
        */
    }

    private void Update() {
        textureCoord = new Vector2(transform.position.x, transform.position.z);
        //heightMap.Resize(256, 256);
        heightMap.SetPixel((int)textureCoord.x, (int)textureCoord.y, color);

        heightMap.Apply();
    }
}
