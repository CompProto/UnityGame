using UnityEngine;
using System.Collections;


public class EnemyController : MonoBehaviour
{

    public float speed = 3;

    //Pathfinding 
    private int[,] movementPath;
    private PathFinding path;
    Vector3 playerPosition;
    Vector3 nextTile;

    //Movement
    private Rigidbody rb;


    //StuckCalculation
    PathFinding.Tile[] stuckTiles;
    PathFinding.Tile positionTile;
    int frameCount = 0;
    int indexCounter = 0;
    float distToPlayer = 0;
    bool isStuck = false;

    // Animation
    Animator anim;
    bool walking = false;
    bool attacking = false;


    float time = 0;
    float startTime;
    float attackTimer = 10;

    private Transform playerTransform;

    void Awake()
    {
        anim = GetComponent<Animator>();


    }
    // Use this for initialization
    void Start()
    {
        anim.enabled = true;
        rb = GetComponent<Rigidbody>();
        stuckTiles = new PathFinding.Tile[10];

        startTime = Time.time;
        path = (PathFinding)GameObject.FindGameObjectWithTag("Pathfinder").GetComponent<PathFinding>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerPosition = playerTransform.position;
    }

    /*
    * StuckDectection(), function that determine if the enemy is stuck in narrow corridors.
    * Works the way, that it put every position into an Array, and checks if these elements are the same.
    * it counts 1 up everytime it detects a similarCount. 55 similarCounts are maximum.
    * 55 is too rigid, detection is not very likely
    * 1 similarCount makes it too possible to detect if stuck.
    */
    private void StuckDectection()
    {
        frameCount++;

        // Only needs to determine if stuck within a range of 5 and 35 from player
        if (distToPlayer <= 35 && distToPlayer > 5)
        {
            // Every 10 frames it puts the position into an array
            if (frameCount % 10 == 0)
            {
                positionTile = new PathFinding.Tile((int)transform.position.x, (int)transform.position.z, 0);
                stuckTiles[indexCounter] = positionTile;

                indexCounter++;

                int similarCount = 0;
                if (indexCounter == 10)
                {
                    //  Goes through the array checking every possible combination
                    for (int i = 0; i < indexCounter; i++)
                    {
                        for (int x = i; x < indexCounter; x++)
                        {
                            // Every time 2 positions are the same it counts 1 up
                            if (stuckTiles[i].Equals(stuckTiles[x]))
                            {
                                similarCount++;
                                // Onces it gets to 33 counts stuck is detected
                                if (similarCount > 33)
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
    // Once stuck is detected, this function is called.
    // It works that way, that it meassures the line between player and enemy
    // and figures the furthest tile to place the enemy with a linecast
    Vector3 GetOutOfStuck()
    {
        // finds the direction to player
        Vector3 AB_normalized = (playerPosition - transform.position).normalized;


        //Goes through the tiles on the line to player, if linecast is false the tile is empty and enemy is placed on temp, else it just goes to nextTile, meaning that no available tile is found.
        for (float i = 20; i > 0; i--)
        {
            Vector3 temp = playerPosition - AB_normalized * i;
            temp.y = -0.9f;

            if (!Physics.Linecast(playerPosition, temp))
            {
                return temp;
            }
        }
        return nextTile;
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = playerTransform.position;
        nextTile = transform.position;
        distToPlayer = Vector3.Distance(transform.position, playerPosition);



        if (isStuck)
        {
            transform.position = GetOutOfStuck();
            isStuck = false;
        }

    }

   

    void Animation()
    {
        anim.SetBool("IsCrawling", walking);


    }

    void FixedUpdate()
    {

        if (distToPlayer < 35 && distToPlayer >= 2)
        {
            if (MoveToNextTile() <= 50 && Physics.Linecast(playerPosition, transform.position))
            {
                Move(nextTile, distToPlayer);
            }
            else if (MoveToNextTile() <= 50 && !Physics.Linecast(playerPosition, transform.position))
            {
                Move(playerPosition, distToPlayer);
            }
        }
        else if (distToPlayer <= 3)
        {
            walking = false;
            Animation();
        }
    

}

    void Move(Vector3 destination, float distance)
    {

        StuckDectection();
        Vector3 lookPos = destination - rb.position;
        lookPos.Normalize();
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos, Vector3.up);
        rotation *= Quaternion.Euler(0, 90, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10f * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        Vector3 velocity = lookPos * speed;
        velocity.y = 0;
        rb.velocity = velocity;
        walking = true;
        Animation();
    }

    float MoveToNextTile()
    {
        try
        {
            PathFinding.Tile tempTile = path.GetNextTile(transform.position);
            nextTile = new Vector3(tempTile.x - 100 + 0.5f, -0.9f, tempTile.y - 60 + 0.5f);

            return tempTile.count;
        }
        catch (System.NullReferenceException)
        {
            nextTile = transform.position;
            return 100000000;
        }


    }
   
}
