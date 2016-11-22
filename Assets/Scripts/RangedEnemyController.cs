using UnityEngine;
using System.Collections;
using Mechanics.Objects;
using RPG.Assets.Scripts.Mechanics.Enumerations;

public class RangedEnemyController : MonoBehaviour {

    public float speed = 2;
    public GameObject RangedAttack;

    private EnemyManager enemyManager;
    private PathFinding path;
    private float distToPlayer;
    private Vector3 nextTile;
    private Transform player;
    private Rigidbody rb;
    private bool hasReset;
//    private float timer;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        path = (PathFinding)GameObject.FindGameObjectWithTag("Pathfinder").GetComponent<PathFinding>();
        distToPlayer = float.MaxValue;
        enemyManager = GetComponent<EnemyManager>();
        enemyManager.enemy =  new Enemy(EnemyType.LONGRANGE, new Interval(1, 5));
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (enemyManager.enemy.IsDead)
            return;
        PathFinding.Tile tempTile = path.GetNextTile(transform.position);
        nextTile = new Vector3(tempTile.x - path.offsetX + 0.5f, -0.9f, tempTile.y - path.offsetY + 0.5f);
        distToPlayer = Vector3.Distance(transform.position, player.position);
    }

    void FixedUpdate()
    {
       // timer += Time.fixedDeltaTime;
        if (distToPlayer > 10 && this.enemyManager.enemy.Wounds == 0) // TODO activation distance
        {
            // Dont activate
            return;
        }
        else
        {
            if(!hasReset)
                ResetEnemyLevel();
            if (distToPlayer > 7) // TODO cast distance
            {
                Move(player.position, distToPlayer);
            }
            else
            {
                if (this.enemyManager.enemy.CanUse(MECHANICS.ABILITIES.ENEMY_RANGED_ATTACK))
                {
                    GameObject bomb = (GameObject)Instantiate(RangedAttack, transform.position, transform.rotation);
                    EnemyRangedAttack eController = bomb.GetComponent<EnemyRangedAttack>();
                    eController.ThrowBomb(player.position, this.enemyManager);
                }
            }
        }
     }

    void ResetEnemyLevel()
    {
        int min = Mathf.Max(GameManager.instance.playerCharacter.Level - 1, 1);
        float wounds = enemyManager.enemy.Wounds;
        enemyManager.enemy = new Enemy(EnemyType.LONGRANGE, new Interval(min, GameManager.instance.playerCharacter.Level + 2));
        enemyManager.enemy.Wounds = wounds;
        Debug.Log(enemyManager.enemy.Life);
        hasReset = true;
    }

    void Move(Vector3 destination, float distance)
    {
        Vector3 lookPos = destination - rb.position;
        lookPos.Normalize();
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos, Vector3.up);
        rotation *= Quaternion.Euler(0, 90, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10f * Time.deltaTime);
        //transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        Vector3 velocity = lookPos * speed;
        velocity.y = 0;
        rb.velocity = velocity;
    }

    float MoveToNextTile()
    {
        try
        {
            PathFinding.Tile tempTile = path.GetNextTile(transform.position);
            nextTile = new Vector3(tempTile.x - path.offsetX + 0.5f, -0.9f, tempTile.y - path.offsetY + 0.5f);

            return tempTile.count;
        }
        catch (System.NullReferenceException)
        {
            nextTile = transform.position;
            return 100000000;
        }
    }
}
