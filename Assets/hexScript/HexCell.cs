using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCell : MonoBehaviour {
    public HexCoordinates coordinates;

    //public Color Color;
    [SerializeField]
    HexCell[] neighbors;
    public RectTransform uiRect;
    public HexGridChunk chunk;
    int elevation = int.MinValue;

    public HexCell GetNeighbor(HexDirection direction) {
        return neighbors[(int)direction];
    }

    public void SetNeighbor(HexDirection direction, HexCell cell) {
        neighbors[(int)direction] = cell;
        cell.neighbors[(int)direction.Opposite()] = this;
    }

    public Color Color
    {
        get
        {
            return color;
        }
        set
        {
            if (color == value)
            {
                return;
            }
            color = value;
            Refresh();
        }
    }

    Color color;

    public int Elevation
    {
        get { return elevation; }
        set {
            if (elevation == value)
            {
                return;
            }
            Refresh();

            Vector3 postion = transform.localPosition;
            postion.y = value * HecMetrics.elevationStep;
            postion.y +=(HecMetrics.SampleNoise(postion).y * 2f - 1f) *HecMetrics.elevationPerturbStrength;
            transform.localPosition = postion;

            Vector3 uiPosition = uiRect.localPosition;
            uiPosition.z = elevation * -HecMetrics.elevationStep;
            uiRect.localPosition = uiPosition;
        }
    }

    public HexEdgeType GetEdgeType(HexCell othercell) {
        return HecMetrics.GetEdgeType(elevation, othercell.elevation);
    }

    public HexEdgeType GetEdgeType(HexDirection direction) {
        return HecMetrics.GetEdgeType(elevation, neighbors[(int)direction].elevation);
    }

    public Vector3 Position
    {
        get
        {
            return transform.localPosition;
        }
    }

    void Refresh()
    {
        if (chunk)
        {
            chunk.Refresh();
            for (int i = 0; i < neighbors.Length; i++)
            {
                HexCell neighbor = neighbors[i];
                if (neighbor != null && neighbor.chunk != chunk)
                {
                    neighbor.chunk.Refresh();
                }
            }
        }
    }

}
