using UnityEngine;
using System.Collections;

namespace AI.PCG
{

    public class TextureGenerator : MonoBehaviour
    {
        public int width = 128;
        public int height = 128;

        private Texture2D texture;

        void Start()
        {
            texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            //texture.filterMode = FilterMode.Point;
            GetComponent<MeshRenderer>().material.mainTexture = texture;

        }

        public float frequency = 1f;
        public float timeFrequency = 10f;

        public int octaves = 5;

        public Color lowColor = Color.white;
        public Color highColor = Color.white;

        void Update()
        {

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    float v = 0;
                    /*  for (int oct = 1; oct <= octaves; oct++)
                      {
                          if (oct == 1)
                          {
                              v += Mathf.PerlinNoise(
                                  oct * frequency * (x + timeFrequency * Time.time) / (float)width,
                                  oct * frequency * (y + timeFrequency * Time.time) / (float)height
                                  );
                          }
                          else
                          {
                              float noise = Mathf.PerlinNoise(
                                  oct * frequency * (x + timeFrequency * Time.time) / (float)width,
                                  oct * frequency * (y + timeFrequency * Time.time) / (float)height);
                              v += (1f / oct) * ((2 * noise ) - 1);
                          }
                      }*/
                    var xx = (x / (float)width - 0.5f) * 2f;
                    var yy = (y /  (float)height - 0.5f) * 2f;

                    v = xx * xx + yy * yy;

                    v = Mathf.Repeat(v + 1 * Mathf.PerlinNoise(1*Time.time, 0 ), 1f);
                    lowColor.r = Mathf.PerlinNoise(1 * Time.time, 0);
                    highColor.g = Mathf.PerlinNoise(1 * Time.time, 0);

                    var col = Color.Lerp(lowColor, highColor, v);
                    texture.SetPixel(x, y, col);
                }
            }

            texture.Apply();
        }
    }

}