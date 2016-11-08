using UnityEngine;
using System.Collections;


public class EnemyController : MonoBehaviour
{

    public float health = 1000;
    public float speed = 3;
    private int[,] movementPath;
    private PathFinding path;
    Vector3 playerPosition;
    Vector3 nextTile;
    Vector3 previousLocation;
    float startTime;
    LayerMask lMask;
    Vector3 lastTile;
    float debugTimer;
    PathFinding.Tile[] stuckTiles;
    PathFinding.Tile positionTile;
    int frameCount = 0;
    int indexCounter = 0;
    float distToPlayer = 0;
    bool isStuck = false;



    // Use this for initialization
    void Start()
    {
        stuckTiles = new PathFinding.Tile[10];


        debugTimer = Time.time;

        startTime = Time.time;
        path = (PathFinding)GameObject.FindGameObjectWithTag("Pathfinder").GetComponent<PathFinding>();



    }
    void StuckDectection()
    {

        frameCount++;
        if (distToPlayer <= 35 && distToPlayer > 5)
        {

            if (frameCount % 10 == 0)
            {
                positionTile = new PathFinding.Tile((int)transform.position.x, (int)transform.position.z, 0);
                stuckTiles[indexCounter] = positionTile;

                indexCounter++;

                int similarCount = 0;
                if (indexCounter == 10)
                {
                    for (int i = 0; i < indexCounter; i++)
                    {
                        for (int x = i; x < indexCounter; x++)
                        {
                            if (stuckTiles[i].Equals(stuckTiles[x]))
                            {
                                similarCount++;
                                if (similarCount > 40)
                                {
                                    
                                    isStuck = true;

                                    similarCount = 0;
                                }

                            }
                        }
                    }
                    indexCounter = 0;
                }

            }
        }
    }

    Vector3 GetOutOfStuck()
    {
        Vector3 AB_normalized = (playerPosition - transform.position).normalized;
        

        for (float i = 20; i > 0; i--)
        {

            Vector3 temp = playerPosition - AB_normalized * i;
            temp.y = -0.9f;
           
            if (!Physics.Linecast(playerPosition, temp))
            {

                Debug.Log("Raycast er false " + temp.ToString());
                return temp;

            }
        }
        return nextTile;
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        nextTile = transform.position;
        distToPlayer = Vector3.Distance(transform.position, playerPosition);


        StuckDectection();
        if (isStuck)
        {
            transform.position = GetOutOfStuck();
            isStuck = false;
        }

        if (distToPlayer < 35 && distToPlayer > 5)
        {
            if (MoveToNextTile() <= 50)
            {
                transform.position = Vector3.MoveTowards(transform.position, nextTile, speed * Time.deltaTime);
            }
        }


    }



    float MoveToNextTile()
    {
        try
        {
            PathFinding.Tile tempTile = path.GetNextTile(transform.position);
            nextTile = new Vector3(tempTile.x - 100 + 0.5f, -0.9f, tempTile.y - 60 + 0.5f);
            Debug.DrawLine(transform.position, playerPosition);
            return tempTile.count;
        }
        catch (System.NullReferenceException)
        {
            nextTile = transform.position;
            return 100000000;
        }
       

    }
    Vector3 CurrentTile()
    {
        Vector3 currentTile;

        int x = (int)transform.position.x;
        int y = (int)transform.position.z;

        currentTile = new Vector3(x, 0.5f, y);
        return currentTile;

    }
}
