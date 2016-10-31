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


    // Use this for initialization
    void Start()
    {

        startTime = Time.time;
        path = (PathFinding)GameObject.FindGameObjectWithTag("Pathfinder").GetComponent<PathFinding>();



    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        nextTile = transform.position;
        float distToPlayer = Vector3.Distance(transform.position, playerPosition);

        if (distToPlayer < 35 && distToPlayer > 5)
        {
            if (MoveToNextTile() <= 50)
            {
                transform.position = Vector3.MoveTowards(transform.position, nextTile, speed * Time.deltaTime);
            }
        }
       

    }


    void FixedUpdate()
    {

    }
    float MoveToNextTile()
    {



        PathFinding.Tile tempTile = path.GetNextTile(transform.position);
        nextTile = new Vector3(tempTile.x - 100 + 0.5f, -0.9f, tempTile.y - 60 + 0.5f);




        return tempTile.count;
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
