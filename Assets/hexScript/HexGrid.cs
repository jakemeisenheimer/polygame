using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour {

    int cellCountX = 6;

    int cellCountZ = 6;
    public Color[] mountainColors;
    public Color[] hillColors;
    public Color[] desertColors;
    public Color[] forestColors;
    public Color[] plainColors;
    public int chunkCountX = 4, chunkCountZ = 3;
    public int chunkNumber = 0;
    public int ChunkRow;
    public HexCell cellPrefab;

  public  HexCell[] cells;

    public Color defaultColor = Color.white;
    public Color touchedColor = Color.magenta;

    public Text cellLabelPrefab;
    public Texture2D noiseSource;
    public HexGridChunk chunkPrefab;
    public HexGridChunk[] chunks;
    void Awake()
    {
        HecMetrics.noiseSource = noiseSource;


        cellCountX = chunkCountX * HecMetrics.chunkSizeX;
        cellCountZ = chunkCountZ * HecMetrics.chunkSizeZ;

        CreateChunks();
        CreateCells();
        createFlatSurface();
    }

    void CreateChunks()
    {
        chunks = new HexGridChunk[chunkCountX * chunkCountZ];

        for (int z = 0, i = 0; z < chunkCountZ; z++)
        {
            for (int x = 0; x < chunkCountX; x++)
            {
                HexGridChunk chunk = chunks[i++] = Instantiate(chunkPrefab);
                chunk.transform.SetParent(transform);
            }
        }
    }

    void CreateCells()
    {
        cells = new HexCell[cellCountZ * cellCountX];

        for (int z = 0, i = 0; z < cellCountZ; z++)
        {
            for (int x = 0; x < cellCountX; x++)
            {
                CreateCell(x, z, i++);
            }
        }
    }

    void OnEnable()
    {
        HecMetrics.noiseSource = noiseSource;
    }

   

    void Update() {
        
    }

   
  

     public HexCell GetCell(Vector3 postion) {
        postion = transform.InverseTransformPoint(postion);
        HexCoordinates coordinates = HexCoordinates.FromPostion(postion);
        int index = coordinates.X + coordinates.Z * cellCountX + coordinates.Z / 2;
        return cells[index];
    }

    void CreateCell(int x, int z, int i) {
        Vector3 postion;
        postion.x = (x+z * .5f - z /2) * (HecMetrics.innerRadius * 2f);
        postion.y = 0f;
        postion.z = z * (HecMetrics.outerRadius * 1.5f);

        HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
        cell.transform.localPosition = postion;
        cell.coordinates = HexCoordinates.FromOffesetCoordinates(x, z);
        cell.Color = defaultColor;

        if (x > 0) {
            cell.SetNeighbor(HexDirection.W, cells[i - 1]);
        }
        if (z > 0) {
            if ((z & 1) == 0)
            {
                cell.SetNeighbor(HexDirection.SE, cells[i - cellCountX]);
                if (x > 0)
                {
                    cell.SetNeighbor(HexDirection.SW, cells[i - cellCountX - 1]);
                }
            }
            else {
                cell.SetNeighbor(HexDirection.SW, cells[i - cellCountX]);
                if (x < cellCountX - 1) {
                    cell.SetNeighbor(HexDirection.SE, cells[i - cellCountX + 1]);
                }
            }
        }

        Text label = Instantiate<Text>(cellLabelPrefab);
        label.rectTransform.anchoredPosition = new Vector2(postion.x, postion.z);
        label.text = cell.coordinates.ToStingOnSperateLines() ;
        cell.uiRect = label.rectTransform;
       
        cell.Elevation = 0;
        makeRegion(cell,x,z);
        
        AddCellToChunk(x, z, cell);
    }

    void makeRegion(HexCell cell,int xPos, int yPos) {
        if (xPos <= (chunkCountX*5)/2 - yPos)
        {
            HecMetrics.elevationStep = 4;
            cell.Color = mountainColors[Random.Range(0,3)];
            cell.Elevation = Random.Range(0, 6);
        }
        else if (xPos >= (chunkCountX * 5) / 2 + yPos)
        {
            HecMetrics.elevationStep = 3;
            cell.Color = hillColors[Random.Range(0, 3)];
            cell.Elevation = Random.Range(0, 4);
        }
        else if(xPos <= Mathf.Abs((chunkCountX * 5) / 2 - yPos))
        {
            HecMetrics.elevationStep = 1.5f;
            cell.Color = forestColors[Random.Range(0, 3)];
            cell.Elevation = Random.Range(0, 2);
        }
        else if (xPos >= chunkCountX * 5 - (Mathf.Abs((chunkCountX * 5 - ((chunkCountX * 5) / 2 + yPos)))))
        {
            HecMetrics.elevationStep = 2.5f;
            cell.Color = desertColors[Random.Range(0, 3)];
            cell.Elevation = Random.Range(0, 4);
        }
        else {
            HecMetrics.elevationStep = 1.5f;
            cell.Color = plainColors[Random.Range(0, 3)];
            cell.Elevation = Random.Range(0, 2);
        }

    }

    void AddCellToChunk(int x, int z, HexCell cell)
    {
        int chunkX = x / HecMetrics.chunkSizeX;
        int chunkZ = z / HecMetrics.chunkSizeZ;
        HexGridChunk chunk = chunks[chunkX + chunkZ * chunkCountX];

        int localX = x - chunkX * HecMetrics.chunkSizeX;
        int localZ = z - chunkZ * HecMetrics.chunkSizeZ;
        chunk.AddCell(localX + localZ * HecMetrics.chunkSizeX, cell);
    }
    void createFlatSurface() {
        foreach (HexCell cell in chunks[(chunks.Length / 2)+2].cells) {
            cell.Elevation = 0;
        }
        
    }


}
