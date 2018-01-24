using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCreations : MonoBehaviour {
    public GameObject[] rooms;
    private int direction = 0;
    private Vector3 zero = Vector3.zero;
    private int vericle = 1, horizontal = 0;
    private float rotaion;
    private Vector2 currrentLocation = new Vector2(200,200);
    private bool[,] grid = new bool[400, 400];
    public GameObject hallway;
    private int x;
    public List<Split> splits = new List<Split>();
    // Use this for initialization
    void Start () {
        GenerateDungeon();
        foreach (Split sp in splits) {
            GenerateDungeonFromSplit(sp);
        }
    }

    void GenerateDungeon() {
        Instantiate(rooms[10], new Vector3(7.9f,0,8), Quaternion.Euler(0, 180, 0));
        for (int y = 0; y < 10; y++)
        {
            int hallwaylength = Random.Range(4, 8);
            GameObject CurrenntHallway = Instantiate(hallway, zero, Quaternion.identity);
            for (x = 1; x < hallwaylength; x++)
            {
                // if their is already a block at the postion or the hallway has ended then place the end block
                if (grid[(int)currrentLocation.x + horizontal * 2, (int)currrentLocation.y + vericle * 2] || grid[(int)currrentLocation.x + horizontal, (int)currrentLocation.y + vericle]|| (x == hallwaylength-1 && y == 9))
                {
                    calculatePostion();
                    Instantiate(rooms[10], new Vector3(zero.x + (horizontal * (10 * x)), 0, zero.z + (vericle * (10 * x))), Quaternion.Euler(0, rotaion, 0));
                    return;
                }
                int random = Random.Range(0, 6);
                // if it wants to place the block a length of 2 but their is only one space left then chose a length one block insted
                if (random == 5 && x == 9)
                {
                    random = Random.Range(0, 5);
                }
                if (random == 0)
                {
                    Instantiate(rooms[0], new Vector3(zero.x + (horizontal * (10 * x)), 0, zero.z + (vericle * (10 * x))), Quaternion.Euler(0, rotaion, 0), CurrenntHallway.transform);
                }
                else if (random == 1)
                {
                    Instantiate(rooms[1], new Vector3(zero.x + (horizontal * (10 * x)), 0, zero.z + (vericle * (10 * x))), Quaternion.Euler(0, rotaion, 0), CurrenntHallway.transform);
                }
                else if (random == 2)
                {
                    Instantiate(rooms[2], new Vector3(zero.x + (horizontal * (10 * x)), 0, zero.z + (vericle * (10 * x))), Quaternion.Euler(0, rotaion, 0), CurrenntHallway.transform);
                }
                else if (random == 3)
                {
                    Instantiate(rooms[3], new Vector3(zero.x + (horizontal * (10 * x)), 0, zero.z + (vericle * (10 * x))), Quaternion.Euler(0, rotaion, 0), CurrenntHallway.transform);
                }
                else if (random == 4)
                {
                    Instantiate(rooms[4], new Vector3(zero.x + (horizontal * (10 * x)), 0, zero.z + (vericle * (10 * x))), Quaternion.Euler(0, rotaion, 0), CurrenntHallway.transform);
                }
                else if (random == 5)
                {
                    Instantiate(rooms[5], new Vector3(zero.x + (horizontal * (10 * x)), 0, zero.z + (vericle * (10 * x))), Quaternion.Euler(0, rotaion, 0), CurrenntHallway.transform);
                    // incresses x and calcualte postion now beucase this block is length 2
                    x++;
                    calculatePostion();
                }
                calculatePostion();
            }
            int aandom = Random.Range(0, 3);
            if (aandom == 0)
            {
                // if the next space is taken palce the end block
                if (grid[(int)currrentLocation.x + horizontal * 2, (int)currrentLocation.y + vericle * 2] || grid[(int)currrentLocation.x + horizontal, (int)currrentLocation.y + vericle] || (x == hallwaylength - 1 && y == 9))
                {
                    calculatePostion();
                    Instantiate(rooms[10], new Vector3(zero.x + (horizontal * (x * 10)), 0, zero.z + (vericle * (x * 10))), Quaternion.Euler(0, rotaion, 0), CurrenntHallway.transform);
                    return;
                }
                // create a right turn
                Instantiate(rooms[6], new Vector3(zero.x + (horizontal * (x * 10)), 0, zero.z + (vericle * (x * 10))), Quaternion.Euler(0, rotaion, 0), CurrenntHallway.transform);
                // set the new starting postion of the next hallway from the starting postion of the prevois one plus how long it was and the offset it needs becuase the first block is roated 90 degrees
                zero = new Vector3((zero.x + (horizontal * (x * 10)) + (9.9f * vericle) + (7.93f * horizontal)), 0, (zero.z + (vericle * (x * 10))) - (9.9f * horizontal) + (7.93f * vericle));
                calculatePostion();
                direction++;
                CalculateDirection();
                if (grid[(int)currrentLocation.x + horizontal * 2, (int)currrentLocation.y + vericle * 2] || grid[(int)currrentLocation.x + horizontal, (int)currrentLocation.y + vericle] || (x == hallwaylength - 1 && y == 9))
                {
                    calculatePostion();
                    Instantiate(rooms[10], zero, Quaternion.Euler(0, rotaion, 0), CurrenntHallway.transform);
                    return;
                }
                Instantiate(rooms[Random.Range(0, 5)], zero, Quaternion.Euler(0, rotaion, 0), CurrenntHallway.transform);
                calculatePostion();
            }
            else if (aandom == 1)
            {
                if (grid[(int)currrentLocation.x + horizontal * 2, (int)currrentLocation.y + vericle * 2] || grid[(int)currrentLocation.x + horizontal, (int)currrentLocation.y + vericle] || (x == hallwaylength - 1 && y == 9))
                {
                    calculatePostion();
                    Instantiate(rooms[10], new Vector3(zero.x + (horizontal * (x * 10)), 0, zero.z + (vericle * (x * 10))), Quaternion.Euler(0, rotaion, 0), CurrenntHallway.transform);
                    return;
                }
                Instantiate(rooms[7], new Vector3(zero.x + (horizontal * (x * 10)), 0, zero.z + (vericle * (x * 10))), Quaternion.Euler(0, rotaion, 0), CurrenntHallway.transform);
                zero = new Vector3((zero.x + (horizontal * (x * 10)) + (-2f * vericle)), 0, (zero.z + (vericle * (x * 10))) - (-2f * horizontal));
                calculatePostion();
                direction--;
                CalculateDirection();
                if (grid[(int)currrentLocation.x + horizontal * 2, (int)currrentLocation.y + vericle * 2] || grid[(int)currrentLocation.x + horizontal, (int)currrentLocation.y + vericle])
                {
                    calculatePostion();
                    Instantiate(rooms[10], zero, Quaternion.Euler(0, rotaion, 0), CurrenntHallway.transform);
                    return;
                }
                Instantiate(rooms[Random.Range(0, 5)], zero, Quaternion.Euler(0, rotaion, 0), CurrenntHallway.transform);
                calculatePostion();
            }
            else if (aandom == 2)
            {
                if (grid[(int)currrentLocation.x + horizontal * 2, (int)currrentLocation.y + vericle * 2] || grid[(int)currrentLocation.x + horizontal, (int)currrentLocation.y + vericle] || (x == hallwaylength - 1 && y == 9))
                {
                    calculatePostion();
                    Instantiate(rooms[10], new Vector3(zero.x + (horizontal * (x * 10)), 0, zero.z + (vericle * (x * 10))), Quaternion.Euler(0, rotaion, 0), CurrenntHallway.transform);
                    return;
                }
               
                Instantiate(rooms[8], new Vector3(zero.x + (horizontal * (x * 10)), 0, zero.z + (vericle * (x * 10))), Quaternion.Euler(0, rotaion, 0), CurrenntHallway.transform);
                // create a split that holds the information for the left side of the split becuase it does the right side first
                Split currentSplit = new Split(new Vector2(currrentLocation.x + horizontal, currrentLocation.y + vericle), new Vector3((zero.x + (horizontal * (x * 10)) + (9.9f * vericle) + (7.93f * horizontal)), 0, (zero.z + (vericle * (x * 10))) - (9.9f * horizontal) + (7.93f * vericle)), oppositeDirection(direction-1), horizontal, vericle, rotaion+90);
                splits.Add(currentSplit);
                zero = new Vector3((zero.x + (horizontal * (x * 10)) + (-2f * vericle)), 0, (zero.z + (vericle * (x * 10))) - (-2f * horizontal));
                calculatePostion();
                direction--;
                CalculateDirection();
                
                if (grid[(int)currrentLocation.x + horizontal * 2, (int)currrentLocation.y + vericle * 2] || grid[(int)currrentLocation.x + horizontal, (int)currrentLocation.y + vericle])
                {
                    calculatePostion();
                    Instantiate(rooms[10], zero, Quaternion.Euler(0, rotaion, 0), CurrenntHallway.transform);
                    return;
                }
                Instantiate(rooms[Random.Range(0, 5)], zero, Quaternion.Euler(0, rotaion, 0), CurrenntHallway.transform);
                calculatePostion();
            }
            if (y == 0 || y == 1)
            {
                CurrenntHallway.SetActive(true);
            }
        }
    }
    void GenerateDungeonFromSplit(Split start)
    {
        // same as generate dungeon but starts from the split information
        Debug.Log(start.zeroAtSplit);
        currrentLocation = start.currentPostion;
        zero = start.zeroAtSplit;
        direction = start.directionAtSplit;
        CalculateDirection();
        rotaion = start.rotaionAtSpilt;
        Instantiate(rooms[Random.Range(0, 5)], zero, Quaternion.Euler(0, rotaion, 0));
        calculatePostion();
        for (int y = 0; y < 10; y++)
        {
            int hallwaylength = Random.Range(4, 8);
            GameObject CurrenntHallway = Instantiate(hallway, zero, Quaternion.identity);
            for (x = 1; x < hallwaylength; x++)
            {
                if (grid[(int)currrentLocation.x + horizontal * 2, (int)currrentLocation.y + vericle * 2] || grid[(int)currrentLocation.x + horizontal, (int)currrentLocation.y + vericle] || (x == hallwaylength - 1 && y == 9))
                {
                    calculatePostion();
                    Instantiate(rooms[10], new Vector3(zero.x + (horizontal * (10 * x)), 0, zero.z + (vericle * (10 * x))), Quaternion.Euler(0, rotaion, 0));
                    return;
                }
                int random = Random.Range(0, 6);
                if (random == 5 && x == 9)
                {
                    random = Random.Range(0, 5);
                }
                if (random == 0)
                {
                    Instantiate(rooms[0], new Vector3(zero.x + (horizontal * (10 * x)), 0, zero.z + (vericle * (10 * x))), Quaternion.Euler(0, rotaion, 0), CurrenntHallway.transform);
                }
                else if (random == 1)
                {
                    Instantiate(rooms[1], new Vector3(zero.x + (horizontal * (10 * x)), 0, zero.z + (vericle * (10 * x))), Quaternion.Euler(0, rotaion, 0), CurrenntHallway.transform);
                }
                else if (random == 2)
                {
                    Instantiate(rooms[2], new Vector3(zero.x + (horizontal * (10 * x)), 0, zero.z + (vericle * (10 * x))), Quaternion.Euler(0, rotaion, 0), CurrenntHallway.transform);
                }
                else if (random == 3)
                {
                    Instantiate(rooms[3], new Vector3(zero.x + (horizontal * (10 * x)), 0, zero.z + (vericle * (10 * x))), Quaternion.Euler(0, rotaion, 0), CurrenntHallway.transform);
                }
                else if (random == 4)
                {
                    Instantiate(rooms[4], new Vector3(zero.x + (horizontal * (10 * x)), 0, zero.z + (vericle * (10 * x))), Quaternion.Euler(0, rotaion, 0), CurrenntHallway.transform);
                }
                else if (random == 5)
                {
                    Instantiate(rooms[5], new Vector3(zero.x + (horizontal * (10 * x)), 0, zero.z + (vericle * (10 * x))), Quaternion.Euler(0, rotaion, 0), CurrenntHallway.transform);
                    x++;
                    calculatePostion();
                }
                calculatePostion();
            }
            int aandom = Random.Range(0, 2);
            if (aandom == 0)
            {
                if (grid[(int)currrentLocation.x + horizontal * 2, (int)currrentLocation.y + vericle * 2] || grid[(int)currrentLocation.x + horizontal, (int)currrentLocation.y + vericle] || (x == hallwaylength - 1 && y == 9))
                {
                    calculatePostion();
                    Instantiate(rooms[10], new Vector3(zero.x + (horizontal * (x * 10)), 0, zero.z + (vericle * (x * 10))), Quaternion.Euler(0, rotaion, 0), CurrenntHallway.transform);
                    return;
                }
                Instantiate(rooms[6], new Vector3(zero.x + (horizontal * (x * 10)), 0, zero.z + (vericle * (x * 10))), Quaternion.Euler(0, rotaion, 0), CurrenntHallway.transform);
                zero = new Vector3((zero.x + (horizontal * (x * 10)) + (9.9f * vericle) + (7.93f * horizontal)), 0, (zero.z + (vericle * (x * 10))) - (9.9f * horizontal) + (7.93f * vericle));
                calculatePostion();
                direction++;
                CalculateDirection();
                if (grid[(int)currrentLocation.x + horizontal * 2, (int)currrentLocation.y + vericle * 2] || grid[(int)currrentLocation.x + horizontal, (int)currrentLocation.y + vericle] || (x == hallwaylength - 1 && y == 9))
                {
                    calculatePostion();
                    Instantiate(rooms[10], zero, Quaternion.Euler(0, rotaion, 0), CurrenntHallway.transform);
                    return;
                }
                Instantiate(rooms[Random.Range(0, 5)], zero, Quaternion.Euler(0, rotaion, 0), CurrenntHallway.transform);
                calculatePostion();
            }
            else if (aandom == 1)
            {
                if (grid[(int)currrentLocation.x + horizontal * 2, (int)currrentLocation.y + vericle * 2] || grid[(int)currrentLocation.x + horizontal, (int)currrentLocation.y + vericle] || (x == hallwaylength - 1 && y == 9))
                {
                    calculatePostion();
                    Instantiate(rooms[10], new Vector3(zero.x + (horizontal * (x * 10)), 0, zero.z + (vericle * (x * 10))), Quaternion.Euler(0, rotaion, 0), CurrenntHallway.transform);
                    return;
                }
                Instantiate(rooms[7], new Vector3(zero.x + (horizontal * (x * 10)), 0, zero.z + (vericle * (x * 10))), Quaternion.Euler(0, rotaion, 0), CurrenntHallway.transform);
                zero = new Vector3((zero.x + (horizontal * (x * 10)) + (-2f * vericle)), 0, (zero.z + (vericle * (x * 10))) - (-2f * horizontal));
                calculatePostion();
                direction--;
                CalculateDirection();
                if (grid[(int)currrentLocation.x + horizontal * 2, (int)currrentLocation.y + vericle * 2] || grid[(int)currrentLocation.x + horizontal, (int)currrentLocation.y + vericle] || (x == hallwaylength - 1 && y == 9))
                {
                    calculatePostion();
                    Instantiate(rooms[10], zero, Quaternion.Euler(0, rotaion, 0), CurrenntHallway.transform);
                    return;
                }
                Instantiate(rooms[Random.Range(0, 5)], zero, Quaternion.Euler(0, rotaion, 0), CurrenntHallway.transform);
                calculatePostion();
            }
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
    void CalculateDirection() {
        if (direction == 4 || direction == 0) {
            direction = 0;
            vericle = 1;
            horizontal = 0;
            rotaion = 0;
        } else if (direction == 1)
        {
            vericle = 0;
            rotaion = 90;
            horizontal = 1;
        }
        else if (direction == 2)
        {
            vericle = -1;
            horizontal = 0;
            rotaion =180;
        }
        else if (direction == 3 || direction == -1)
        {
            direction = 3;
            vericle = 0;
            horizontal = -1;
            rotaion = 270;
        }
    }

  
    void OnDrawGizmos()
    {

        for(int x = 0; x < 4000; x += 10) {
            for (int y = 0; y < 4000; y += 10)
            {
                if (grid[x/10,y/10])
                {
                    Gizmos.DrawCube(new Vector3(x, 0, y), new Vector3(10, 10, 10));
                    Gizmos.color = Color.red;
                }
                else {
                    Gizmos.color = Color.white;
                }
            }
        }
    }

    void calculatePostion() {
        currrentLocation.x += horizontal;
        currrentLocation.y += vericle;
        grid[(int)currrentLocation.x, (int)currrentLocation.y] = true;
    }

    bool checkSpaceForFightClub() {
        int jorizon = 0;
        int kertical = 0;
        int RealX = 0;
        int RealY = 0;
        if (horizontal == 0) {
            jorizon = 3;
            kertical = 2;
            RealX = 0;
            RealY = -1;
        } else if (vericle == 0)
        {
            jorizon = 2;
            kertical = 3;
            RealX = -1;
            RealY = 0;
        }
        for (int x = RealX; x < jorizon; x++) {
            for (int y = RealY; y < kertical; y++)
            {
                if(grid[(int)currrentLocation.x + x, (int)currrentLocation.y + y])
                {
                    return false;
                }
            }
        }
        for (int y = 0; y < 3; y++)
        {
            grid[(int)currrentLocation.x + x, (int)currrentLocation.y + y] = true;
        }
        return true;
    }

    int oppositeDirection(int direction) {
        int newdirection = direction + 2;

        if (newdirection > 3)
        {
            newdirection -= 4;
        }
        else if (newdirection < 0) {
            newdirection += 4;
        }
        return newdirection;
    }
    public struct Split
    {
       public Vector2 currentPostion;
        public Vector3 zeroAtSplit;
        public int directionAtSplit;
        public int horizontalAtSplit;
        public int vecerticalAtSplit;
        public float rotaionAtSpilt;
        public Split(Vector2 pos, Vector3 zero, int dir, int hor, int vert, float rot) {
            currentPostion = pos;
            zeroAtSplit = zero;
            directionAtSplit = dir;
            horizontalAtSplit = hor;
            vecerticalAtSplit = vert;
            rotaionAtSpilt = rot;
        }
        
    }
}
