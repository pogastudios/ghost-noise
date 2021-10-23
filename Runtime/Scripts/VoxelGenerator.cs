using UnityEngine;
using System.Collections;
using Packages.GhostNoise;
using Packages.GhostNoise.Generation.Fractal;
using System;

public class VoxelGenerator : MonoBehaviour
{
    public GameObject SurfaceObject;
    public int GridWidth = 30;
    public int GridLength = 30;
    public int GridHeight = 4;
    public int HeightSeed = 22412;
    public float Threshold = 0.5f;
    public float NoiseScale = 47.03f;

    private Generator generator;

    // Use this for initialization
    void Start()
    {
        generator = new RidgeNoise(HeightSeed) {Exponent = 2, Gain = 1 };
        for (int x = 0; x < GridWidth; x++)
        {
            for (int y = 0; y < GridHeight; y++)
            {
                for (int z = 0; z < GridLength; z++)
                {
                    var noise = generator.GetValue(x / NoiseScale, y / NoiseScale, z / NoiseScale);                    //if (noise > 1f)                    //    noise = 1;
                    if (noise < Threshold && y != 0)
                        continue;

                    if (!IsCubeSideExposed(noise, x, y, z))
                        continue;

                    var obj = Instantiate(SurfaceObject, new Vector3(x, y, z), Quaternion.identity) as GameObject;
                    obj.transform.parent = transform;


                    var rend = obj.GetComponent<Renderer>();
                    rend.material.color = new Color(noise, noise, noise, 1);

                }
            }
        }
    }

    private bool IsCubeSideExposed(float noise, int x, int y, int z)
    {
        if (y == 0 || x == 0 || z == 0 || y == GridHeight - 1 || x == GridWidth - 1 || z == GridLength - 1)
            return true;

        //if (noise > 1f)
        //    noise = 1;

        var noiseSide1 = generator.GetValue((x - 1) / NoiseScale, y / NoiseScale, z / NoiseScale);
        var noiseSide2 = generator.GetValue((x + 1) / NoiseScale, y / NoiseScale, z / NoiseScale);
        var noiseSide3 = generator.GetValue(x / NoiseScale, (y - 1) / NoiseScale, z / NoiseScale);
        var noiseSide4 = generator.GetValue(x / NoiseScale, (y + 1) / NoiseScale, z / NoiseScale);
        var noiseSide5 = generator.GetValue(x / NoiseScale, y / NoiseScale, (z - 1) / NoiseScale);
        var noiseSide6 = generator.GetValue(x / NoiseScale, y / NoiseScale, (z + 1) / NoiseScale);

        return noiseSide1 < Threshold || noiseSide2 < Threshold || noiseSide3 < Threshold || noiseSide4 <  Threshold || noiseSide5 < Threshold || noiseSide6 < Threshold;
    }
}
