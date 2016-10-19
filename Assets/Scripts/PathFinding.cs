using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    private int[,] movementPath;




    void Start()
    {
        // mapGen og map sættes, så man kan søge i det tilgænglige map.
        mapGen = (MapGenerator)FindObjectOfType(typeof(MapGenerator));
        map = mapGen.map;
        // dimensionerne hentes fra MapGenerator, så der kan laves et nyt 2d array med værdier i, der skal repræsentere hvordan npcen skal //
        // flytte sig
        height = mapGen.height;
        width = mapGen.width;
        
        // Players position hentes der rundes ned til nærmeste integer
    }

    int[,] FindPath(Vector3 enemyPosition)
    {
        movementPath = new int[height, width];
        
        playerPosition = ((Player)GameObject.FindObjectOfType(typeof(Player))).transform.position;
        playerTile = new Tile((int)playerPosition.x, (int)playerPosition.y, 0);
        enemyTile = new Tile((int)enemyPosition.x, (int)enemyPosition.z, -1000000);
        initMap();

        queue.Enqueue(playerTile);
        Tile current = new Tile(-1, -1, -1);
        while (!current.Equals(enemyTile))
        {

            current = queue.Dequeue();
            FindNeighbours(current);

            if (queue.Count == 0)
            {
                current = enemyTile;
            }

        }
        return movementPath;
    }

    void FindNeighbours(Tile current)
    {
        int x = (int)current.x;
        int y = (int)current.y;
        int count = (int)current.count;


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


        for (int x = 0; x < 11; x++)
        {
            for (int y = 0; y < 14; y++)

            {
                movementPath[x, y] = 999998;

            }
        }
        movementPath[playerTile.x, playerTile.y] = playerTile.count;
        movementPath[enemyTile.x, enemyTile.y] = enemyTile.count;

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
