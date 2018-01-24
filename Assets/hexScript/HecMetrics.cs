using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum HexEdgeType {
    flat,slope,cliff
}
public class HecMetrics {

    public const float outerRadius = 10f;
    public const float innerRadius = outerRadius * .866025404f;
    public const float solidFactor = .5f;
    public const float blendFactor = 1f - solidFactor;
    public static float elevationStep = 4f;
    public const int terrancePerSlope = 2;
    public const int terraceSteps = terrancePerSlope * 2 + 1;
    public static Texture2D noiseSource;
    public const float cellPerturbStrength = 5f;
    public const float elevationPerturbStrength = 1.5f;
    public const int chunkSizeX = 5, chunkSizeZ = 5;

    public static Vector3[] corners = {
        new Vector3(0f,0f,outerRadius),
        new Vector3(innerRadius,0f,0.5f *outerRadius),
        new Vector3(innerRadius,0f,-0.5f *outerRadius),
        new Vector3(0f,0f,-outerRadius),
        new Vector3(-innerRadius,0f,-0.5f *outerRadius),
        new Vector3(-innerRadius,0f,0.5f *outerRadius),
        new Vector3(0f,0f,outerRadius)
        };

    public static Vector3 GetFirstCorner(HexDirection direction) {
        return corners[(int)direction];
    }

    public static Vector3 GetSecondCorner(HexDirection direction) {
        return corners[(int)direction + 1];
    }

    public const float noiseScale = 0.003f;

    public static Vector4 SampleNoise(Vector3 position)
    {
        return noiseSource.GetPixelBilinear(position.x * noiseScale, position.z * noiseScale);
    }

    
    public static Vector3 GetFirstSolidCorner(HexDirection direction)
    {
        return corners[(int)direction] * solidFactor;
    }

    public static Vector3 GetSecondSolidCorner(HexDirection direction)
    {
        return corners[(int)direction + 1] * solidFactor;
    }

    public static Vector3 GetBridge(HexDirection direction) {
        return (corners[(int)direction] + corners[(int)direction + 1]) * blendFactor;
    }

    public const float horizontalTerraceStepSize = 1f / terraceSteps;
    public const float verticalTerraceStepSize = 1f / (terrancePerSlope + 1);

    public static Vector3 TerraceLerp(Vector3 a,Vector3 b,int step) {
        float h = step * HecMetrics.horizontalTerraceStepSize;
        a.x += (b.x - a.x) * h;
        a.z += (b.z - a.z) * h;
        float v = ((step + 1) / 2) * HecMetrics.verticalTerraceStepSize;
        a.y += (b.y - a.y) * v;
        return a;
    }

    public static Color TerranceLerp(Color a, Color b, int step) {
        float h = step * HecMetrics.horizontalTerraceStepSize;
        return Color.Lerp(a, b, h);
    }

    public static HexEdgeType GetEdgeType(int elevation1, int elevation2) {
        if (elevation1 == elevation2) {
            return HexEdgeType.flat;
        }
        int delta = elevation2 - elevation1;
        if (delta == 1 || delta == -1) {
            return HexEdgeType.slope;
    }
        return HexEdgeType.cliff;
    }

    
}
