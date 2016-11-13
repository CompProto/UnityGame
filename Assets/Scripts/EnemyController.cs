using UnityEngine;
using System.Collections;


public class EnemyController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 3;
    private int[,] movementPath;
    private PathFinding path;
    Vector3 playerPosition;
    Vector3 nextTile;
    float startTime;
    PathFinding.Tile[] stuckTiles;
    PathFinding.Tile positionTile;
    int frameCount = 0;
    int indexCounter = 0;
    float distToPlayer = 0;
    bool isStuck = false;
    bool walking = false;
    bool attacking = false;
    Animator anim;
    float time = 0;


    void Awake()
    {
        anim = GetComponent<Animator>();


    }
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stuckTiles = new PathFinding.Tile[10];

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

        if (distToPlayer < 35 && distToPlayer >= 1)
        {
            if (MoveToNextTile() <= 50)
            {
                Vector3 lookPos = nextTile - rb.position;
                lookPos.y = 0;
                Quaternion rotation = Quaternion.LookRotation(lookPos);
                rotation *= Quaternion.Euler(0, 90, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10f * Time.deltaTime);
                transform.position = Vector3.MoveTowards(transform.position, nextTile, speed * Time.deltaTime);
                walking = true;
                attacking = false;
            }

        }
        else if (distToPlayer <= 1)
        {
            attacking = true;
            walking = false;
        }
    }

    void Animation()
    {
        anim.SetBool("IsCrawling", walking);
        anim.SetBool("IsAttacking", attacking);

    }

    void FixedUpdate()
    {
        time = time + Time.deltaTime;
        if (time >= 1)
        {
            Animation();
            time = 0;
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
