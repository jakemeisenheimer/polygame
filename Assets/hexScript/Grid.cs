using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Grid : MonoBehaviour
{
    public bool OnlyDrawPath;
    public bool desertOrPlain;
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    Node[,] grid;
    public GameObject[] mountain;
    public GameObject[] plains;
    public GameObject[] desert;
    public GameObject[] forest;
    public GameObject[] hills;
    public GameObject[] largeObjects;
    private int[] mountainPercents = {15,15,15,100 };
    private int[] plainsPercents = {1, 24, 24, 24, 160,160,90,90};
    private int[] desertPercents = {6, 10, 30, 30, 300,300};
    private int[] forestPercents = { 1, 12, 12, 40, 40, 40, 40,200 };
    private int[] hillsPercents = { 1, 30, 100, 100, 100};
    float nodeDiameter;
    int gridSizeX, gridSizeY;
    public int fillAmount;
    HexGridChunk[] chunks;
    public HexGrid hexgrid;
    void Start()
    {
        gridWorldSize.x = hexgrid.chunkCountX * 85;
        gridWorldSize.y = hexgrid.chunkCountX * 74;
        transform.position = new Vector3(hexgrid.chunkCountX * 42.5f, 30, hexgrid.chunkCountX * 37f);
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        chunks = hexgrid.chunks;
        CreateGrid();
        FindFlatNodes();
        CreateEnvoriments();
    }
    public int MaxSize
    {
        get
        {
            return gridSizeX * gridSizeY;
        }
    }
    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - new Vector3(gridWorldSize.x / 2, 0, gridWorldSize.y / 2);
        bool walkable = true;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                walkable = true;
                RaycastHit hit;
                float yPos = 0;
                if (Physics.Raycast(worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius), Vector3.down, out hit, 50))
                {
                    yPos = hit.point.y;
                }
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                worldPoint = new Vector3(worldPoint.x, yPos, worldPoint.z);
                grid[x, y] = new Node(walkable, worldPoint, x, y, Color.white);
            }
        }
    }




    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }

    public List<Node> path;
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
        if (OnlyDrawPath == true)
        {

        }
        else
        {

            if (grid != null)
            {
                foreach (Node n in grid)
                {
                    Gizmos.color = n.nodeColor;
                    if (path != null)
                        if (path.Contains(n))
                            Gizmos.color = Color.black;
                    Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
                }
            }
        }
    }



    bool checkNodeAround(int width, int lenght, int xNodePostion, int yNodePostion)
    {
        if (xNodePostion - width < 0 || yNodePostion - lenght < 0 || xNodePostion + width > gridSizeX || yNodePostion + lenght > gridSizeY)
        {
            return false;
        }
        for (int x = -width / 2; x < width / 2; x++)
        {
            for (int y = -lenght / 2; y < lenght / 2; y++)
            {

                if (!grid[x + xNodePostion, y + yNodePostion].walkable)
                {
                    return false;
                }
            }
        }

        for (int x = -width / 2; x < width / 2; x++)
        {
            for (int y = -lenght / 2; y < lenght / 2; y++)
            {
                grid[x + xNodePostion, y + yNodePostion].setWalkable(false);
                grid[x + xNodePostion, y + yNodePostion].setNodeColor(Color.black);
            }
        }
        return true;
    }

    void FindFlatNodes()
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                if (x <= gridSizeX / 2 - y)
                {
                    checkAroundFlatNodes(x,y);
                }
               if (x >= gridSizeX / 2 + y)
                {
                      
                    checkAroundFlatNodes(x, y);
                }
                 if (x >= gridSizeX - (Mathf.Abs((gridSizeX - (gridSizeX / 2 + y)))))
                {
                    checkAroundFlatNodes(x, y);
                }
            }
        } 
    }

    void checkAroundFlatNodes(int x, int y) {
        if (x + 3 > gridSizeX || y + 3 > gridSizeY|| x - 3 < 0 || y - 3 < 0) {
            return;
        }
        int numberOfNotFlatNodeAroundTheNodeThatIsBeingCheck = 0;
        for (int xPos = 0; xPos < 3; xPos++) {
            for (int yPos = 0; yPos < 3; yPos++)
            {
                if (yPos != 0 && xPos != 0) {
                    if (Mathf.Abs(grid[x + xPos, y + yPos].worldPosition.y - grid[x, y].worldPosition.y) < .2) {
                        numberOfNotFlatNodeAroundTheNodeThatIsBeingCheck++;
                    }
                }
            }
        }

        if (numberOfNotFlatNodeAroundTheNodeThatIsBeingCheck > 3) {
            grid[x, y].setNodeColor(Color.red);
            grid[x, y].setNodeFlat(true);
        }
    }

    void CreateEnvoriments()
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                if (x <= gridSizeX / 2 - y)
                {
                }
                else if (x >= gridSizeX / 2 + y)
                {
                }
                else if (x <= Mathf.Abs(gridSizeX / 2 - y))
                {
                }
                else if (x >= gridSizeX - (Mathf.Abs((gridSizeX - (gridSizeX / 2 + y)))))
                {
                }
                else
                {
                    FillEnvoirmnetLargeObjects(largeObjects[0], grid[x, y].worldPosition, x, y, 5000, true, 6, 6);
                }
            }
        }
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                if (x <= gridSizeX/2 - y)
                {
                    FillEnvoirmnet(mountain,grid[x, y].worldPosition,x,y,mountainPercents, grid[x, y].flat);
                }
                else if (x >= gridSizeX / 2 + y)
                {
                    FillEnvoirmnet(hills, grid[x, y].worldPosition, x, y, hillsPercents, grid[x, y].flat);
                }
                else if (x <= Mathf.Abs(gridSizeX / 2 - y))
                {
                    FillEnvoirmnet(forest, grid[x, y].worldPosition, x, y, forestPercents);
                }
                else if (x >= gridSizeX - (Mathf.Abs((gridSizeX - (gridSizeX / 2 + y)))))
                {
                    FillEnvoirmnet(desert, grid[x, y].worldPosition, x, y, desertPercents, grid[x, y].flat);
                }
                else
                {
                    FillEnvoirmnet(plains, grid[x, y].worldPosition, x, y, plainsPercents);
                }
            }
        }
      }

    void FillEnvoirmnet(GameObject[] placeableOjects, Vector3 PlacePostion, int x, int y, int[] Precentages) {
        int round = 0;
        foreach (GameObject objectToPlace in placeableOjects) {
            if (Random.Range(0, fillAmount * Precentages[round]) == 2 && grid[x,y].walkable)
            {
                int p = Mathf.RoundToInt(((x / (gridSizeX / hexgrid.chunkCountX))));
                int q = ((Mathf.RoundToInt(y / (((gridSizeY / hexgrid.chunkCountX))))) * hexgrid.chunkCountX);
                if (q == 36)
                {
                    q = 30;
                }
                Instantiate(objectToPlace, PlacePostion, Quaternion.identity, chunks[p + q].loadTrigger.transform);
                grid[x, y].setWalkable(false);
            }
            round++;
        }
    }

    void FillEnvoirmnet(GameObject[] placeableOjects, Vector3 PlacePostion, int x, int y, int[] Precentages,bool isFlat)
    {
        int round = 0;
        foreach (GameObject objectToPlace in placeableOjects)
        {
            if (Random.Range(0, fillAmount * Precentages[round]) == 2 && isFlat && grid[x, y].walkable)
            {
                int p = Mathf.RoundToInt(((x / (gridSizeX / hexgrid.chunkCountX))));
                int q = ((Mathf.RoundToInt(y / (((gridSizeY / hexgrid.chunkCountX))))) * hexgrid.chunkCountX);
                if (q == 36)
                {
                    q = 30;
                }
                Instantiate(objectToPlace, PlacePostion, Quaternion.identity, chunks[p + q].loadTrigger.transform);
                grid[x, y].setWalkable(false);
            }
            round++;
        }
    }

    void FillEnvoirmnetLargeObjects(GameObject placeableOjects, Vector3 PlacePostion, int x, int y, int Precentages, bool isFlat,int XRange,int yRange)
    {

            if (Random.Range(0, fillAmount * Precentages) == 2 && isFlat && checkNodeAround(XRange,yRange,x,y))
            {
                int p = Mathf.RoundToInt(((x / (gridSizeX / hexgrid.chunkCountX))));
                int q = ((Mathf.RoundToInt(y / (((gridSizeY / hexgrid.chunkCountX))))) * hexgrid.chunkCountX);
                if (q == 36)
                {
                    q = 30;
                }
                Instantiate(placeableOjects, PlacePostion, Quaternion.identity, chunks[p + q].loadTrigger.transform);
                grid[x, y].setWalkable(false);
            }
        }
    }



