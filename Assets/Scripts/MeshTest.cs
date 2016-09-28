using UnityEngine;
using System.Collections;

public class MeshTest : MonoBehaviour {

    Texture2D texture;
    public float amplitude;

    // Use this for initialization
    void Start()
    {      
        Mesh mesh = GetComponent<MeshFilter>().mesh;

        float xCoord  = 0;
        float yCoord  = 0;
        float sample  = 0;
        float sample2 = 0;
        float sample3 = 0;

        Vector3[] vecs = mesh.vertices;
        int h = 0;
        for (int i = 0; i < 11; i++) {
            for (int j = 0; j < 11; j++) {
                xCoord = 10 + i;
                yCoord = 10 + j;

                sample = Mathf.PerlinNoise(xCoord, yCoord);
                sample2 = Mathf.PerlinNoise(xCoord / 4, yCoord / 4);
                sample3 = Mathf.PerlinNoise(xCoord / 10, yCoord / 10);

                sample += sample2 + sample3;
                vecs[h].y += sample * amplitude;
                h++;
            }
        }

        mesh.vertices = vecs;
    }
}

