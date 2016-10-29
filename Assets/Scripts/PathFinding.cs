using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PathFinding : MonoBehaviour
{
    // Variabler der skal hentes fra eksterne kilder
    private MapGenerator mapGen;
    int height;
    int width;
    int[,] map;
    Queue<Tile> queue = new Queue<Tile>();
    private Vector3 playerPosition;
    private Tile playerTile;
    private Vector3 enemyPosition;
    private Tile enemyTile;
    public int[,] movementPath;
    private bool isInit = false;
    private float timer;
    private float startTime;




    void Start()
    {
        startTime = Time.time;

        // mapGen og map sættes, så man kan søge i det tilgænglige map.
        mapGen = (MapGenerator)FindObjectOfType(typeof(MapGenerator));
        map = mapGen.map;
        // dimensionerne hentes fra MapGenerator, så der kan laves et nyt 2d array med værdier i, der skal repræsentere hvordan npcen skal //
        // flytte sig
        height = mapGen.height;
        width = mapGen.width;
        movementPath = new int[width, height];






    }

    void Update()
    {
        timer = Time.time - startTime;
        playerPosition = GameObject.Find("ThirdPersonController").transform.position;
        playerTile = new Tile((int)playerPosition.x + 100, (int)playerPosition.z + 60, 0);
        if (!isInit || timer >= 10)
        {

            var watch = System.Diagnostics.Stopwatch.StartNew();

            initMap();
            FindPath();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
           // print("Det tog  " + elapsedMs + "ms");

            if (!isInit)
            {
              //  WriteOutMap(); // Husk at indsætte sti til biblioteket. filen vil blive oprettet
                isInit = true;
            }



        }




    }

    void FindPath()
    {
        queue.Enqueue(playerTile);


        Tile current;
        while (queue.Count > 0)
        {
            //System.IO.File.AppendAllText("C:\\Users\\KimdR\\Desktop\\queue.txt", queue.Count + Environment.NewLine);
            current = queue.Dequeue();
            if (current.count <= 50)
            {
                FindNeighbours(current);
            }

        }

    }

    void FindNeighbours(Tile current)
    {

        int x = (int)current.x;
        int y = (int)current.y;
        int count = (int)current.count;

        if (map == null || movementPath == null)
        {
           
            return;
        }

        if (x > 0)
            if (map[x - 1, y] == 0 && movementPath[x - 1, y] == 999998)
            {

                Tile up = new Tile(x - 1, y, count + 1);
                queue.Enqueue(up);
                for (int i = 0; i < queue.Count; i++)

                    movementPath[(int)up.x, (int)up.y] = (int)up.count;
            }
        if (x < height - 1)
            if (map[x + 1, y] == 0 && movementPath[x + 1, y] == 999998)
            {

                Tile down = new Tile(x + 1, y, count + 1);
                queue.Enqueue(down);
                movementPath[(int)down.x, (int)down.y] = (int)down.count;
            }
        if (y > 0)
            if (map[x, y - 1] == 0 && movementPath[x, y - 1] == 999998)
            {

                Tile left = new Tile(x, y - 1, count + 1);
                queue.Enqueue(left);
                movementPath[(int)left.x, (int)left.y] = (int)left.count;
            }
        if (y < width - 1)
            if (map[x, y + 1] == 0 && movementPath[x, y + 1] == 999998)
            {

                Tile right = new Tile(x, y + 1, count + 1);
                queue.Enqueue(right);
                movementPath[(int)right.x, (int)right.y] = (int)right.count;
            }

    }
    void initMap()
    {


        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < width; y++)

            {
                if (map[y, x] == 1)
                    movementPath[y, x] = 1000000;
                else if (map[y, x] == 0)
                    movementPath[y, x] = 999998;

            }
        }
        movementPath[playerTile.x, playerTile.y] = playerTile.count;


    }


    public Tile GetNextTile(Vector3 enemyPosition)
    {
        Tile enemy = new Tile((int)Mathf.Floor(enemyPosition.x) + 100, (int)Mathf.Floor(enemyPosition.z) + 60, 0);


        //Debug.Log("GetNextTile + enemy.x :" + enemy.x + " enemy.y :" + enemy.y);
        int x = enemy.x;
        int y = enemy.y;

        List<Tile> neighbourList = new List<Tile>();
        if (x > 0)
        {
            Tile tm = new Tile(x - 1, y, movementPath[x - 1, y]);       // topmid
            neighbourList.Add(tm);
            if (y > 0)
            {
                Tile tl = new Tile(x - 1, y - 1, movementPath[x - 1, y - 1]); // topleft tile
                neighbourList.Add(tl);
            }
            if (x > width - 1)
            {
                Tile tr = new Tile(x - 1, y + 1, movementPath[x - 1, y + 1]);  // top right
                neighbourList.Add(tr);
            }
        }
        if (y > 0)
        {
            Tile ml = new Tile(x, y - 1, movementPath[x, y - 1]); // middle left
            neighbourList.Add(ml);
        }
        if (y < height - 1)
        {
            Tile mr = new Tile(x, y + 1, movementPath[x, y + 1]); // middle right
            neighbourList.Add(mr);
        }
        if (x < width - 1)
        {
            Tile bm = new Tile(x + 1, y, movementPath[x + 1, y]);  // bottom middle;
            neighbourList.Add(bm);
            if (y > 0) { }
            Tile bl = new Tile(x + 1, y + 1, movementPath[x + 1, y - 1]); // bottom left;
            neighbourList.Add(bl);
        }

        if (y < height - 1)
        {
            Tile br = new Tile(x + 1, y + 1, movementPath[x + 1, y + 1]); // Buttom right;
            neighbourList.Add(br);
        }

        Tile minCountTile = neighbourList[0];
        for (int i = 1; i < neighbourList.Count; i++)
        {
            if (neighbourList[i].count < minCountTile.count)
            {
                minCountTile = neighbourList[i];

            }
        }
        return minCountTile;
    }

    void WriteOutMap()
    {


        for (int y = 0; y < height; y++)
        {
            String path = ""; // Path til biblioteket hvor txt skal ligge.
            System.IO.File.AppendAllText(path, y - 60 + "|| ");
            for (int x = 0; x < width; x++)
            {
                if (movementPath[x, y] == 1000000)
                {
                    System.IO.File.AppendAllText(path, "n ");
                }
                else
                {
                    System.IO.File.AppendAllText(path, movementPath[x, y] + " ");
                }
                if (x == width - 1)
                {
                    System.IO.File.AppendAllText(path, movementPath[x, y] + Environment.NewLine);
                }
            }


        }




    }

    public class Tile
    {
        public int x;
        public int y;
        public int count;

        public Tile(int x, int y, int count)
        {
            this.x = x;
            this.y = y;
            this.count = count;
        }
    }

}
