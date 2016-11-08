using UnityEngine;
using System.Collections;
using Mechanics.Objects;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public Player playerCharacter;
    private float timer;

    public bool isDarkMode { get; set; }
    public bool isDead { get { return this.playerCharacter.IsDead; } }

    public string EnemyTag = "PowerUp"; // TODO - change to correct tag

    void Awake()
    {
        // Enforces our Singleton pattern
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        this.playerCharacter = new Player();
        this.timer = 0f;
    }

    void FixedUpdate()
    {
        this.timer += Time.fixedDeltaTime;
        if (this.timer >= 1.0f)
        {
            this.timer = 0f;
            this.playerCharacter.Update();
        }
    }

}
