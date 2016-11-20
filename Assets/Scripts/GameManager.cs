using UnityEngine;
using System.Collections;
using Mechanics.Objects;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public Player playerCharacter;
    private float timer;

    public bool isDarkMode { get; set; }
    public bool isDead { get { return this.playerCharacter.IsDead; } }

    public string EnemyTag = "Enemy";

    public GameObject DeadOverlay;


    void Awake()
    {
        // Enforces our Singleton pattern
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

      //  DontDestroyOnLoad(gameObject);
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

    void Update()
    {
        if (isDead && !DeadOverlay.activeSelf)
        {
            DeadOverlay.SetActive(true);
        }
        if (isDead)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                // TODO - reload scene correctly
                SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
            }
        }
        
    }

}
