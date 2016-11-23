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

    public bool isPaused { get { return PauseScreen.activeSelf; } }

    public string EnemyTag = "Enemy";

    public GameObject DeadOverlay;
    public GameObject PauseScreen;
    public GameObject ShadowManager;

    private ModeManager _modeManager;


    void Awake()
    {
        // Enforces our Singleton pattern
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        _modeManager = ShadowManager.GetComponent<ModeManager>();
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
            Time.timeScale = 0.25f;
            if (Input.GetKeyDown(KeyCode.R))
            {
                // TODO - reload scene correctly ?
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
            }
        }

        if(!isDead && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)))
        {
            // Pause/unpause game
            if (PauseScreen.activeSelf)
            {
                Time.timeScale = 1;
                PauseScreen.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                PauseScreen.SetActive(true);
            }
        }
        if (PauseScreen.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Q))
                Application.Quit(); // EXIT game
        }
        
    }

    public void AddBalanceAmount(float amount)
    {
        _modeManager.AddBalanceAmount(amount);
    }

}
