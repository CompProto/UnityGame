using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Mechanics.Objects;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public Player playerCharacter;

    public bool isDarkMode { get; set; }
    public bool isDead { get { return this.playerCharacter.IsDead; } }

    public bool isPaused { get { return PauseScreen.activeSelf; } }

    public string EnemyTag = "Enemy";

    public GameObject DeadOverlay;
    public GameObject PauseScreen;
    public GameObject ShadowManager;

    
    private NarrativeBox narrativeBox;
    private ModeManager _modeManager;
    private MapGenerator mapGenerator;

    public int level = 0;

    public ItemGenerator itemGen { get; private set; }


    void Awake()
    {
        // Enforces our Singleton pattern
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        
        _modeManager = ShadowManager.GetComponent<ModeManager>();
        mapGenerator = GameObject.FindGameObjectWithTag("MapGenerator").GetComponent<MapGenerator>();
        narrativeBox = GameObject.FindGameObjectWithTag("NarrativeBox").GetComponent<NarrativeBox>();
        //playerCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //Debug.Log("Player object: "+GameObject.FindGameObjectWithTag("Player").GetComponent<Player>());
        //hudOverlay = GameObject.FindGameObjectWithTag("HUDOverlay").GetComponent<HUDOverlay>();
        //Debug.Log("Pause object: " + transform.Find("PauseMenu").name);
        DontDestroyOnLoad(instance);
        DontDestroyOnLoad(narrativeBox);
        //DontDestroyOnLoad(hudOverlay);
        //DontDestroyOnLoad(PauseScreen);
        itemGen = new ItemGenerator();
        itemGen.Initialize();
    }

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelChanged;
        SceneManager.activeSceneChanged += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelChanged;
        SceneManager.activeSceneChanged -= OnLevelFinishedLoading;
    }

    
    public void FindObjects()
    {
        Transform[] trs = Resources.FindObjectsOfTypeAll<Transform>();
        
        foreach (Transform t in trs)
        {
            //Debug.Log(t.name);
            if (t.name.Equals("Player"))
            {
                //Debug.Log(t.name);                
                //playerCharacter = t;
            }
            if (t.name.Equals("PauseMenu"))
            {
                Debug.Log(t.name);
                PauseScreen = t.gameObject;
            }
            if (t.name.Equals("DeadOverlay"))
            {
                Debug.Log(t.name);
                DeadOverlay = t.gameObject;
                //DeadOverlay.SetActive(false);
            }
        }
    }

    void OnLevelFinishedLoading(Scene previousScene, Scene newScene)
    {
        FindObjects();
        //this.playerCharacter = new Player();

        //Debug.Log("Pause object: " + transform.Find("PauseMenu").name);
        //PauseScreen = GameObject.Find("");
        //Debug.Log("PM objekt: " + GameObject.Find("PauseMenu").name);
        //Debug.Log("PM name: " + PauseScreen.name);
        //if (PauseScreen==null)
        //{
        //    PauseScreen = GameObject.Find("PauseMenu");
        //}
        //Debug.Log("Finished loading level");
    }

    void OnLevelChanged(Scene scene, LoadSceneMode mode)
    {
        //Add one to our level number.
        level++;
        narrativeBox.setText(level);
        //Debug.Log("Level Changed");

    }

    public void loadNewScene()
    {
        if (level == 1)
        {
            //narrativeBox.setText(int.Parse(string.Concat(level.ToString(), "2")));
            narrativeBox.setText(12);
        }
        else if (level == 2)
        {
            narrativeBox.setText(22);
        }
        else if (level == 3)
        {
            narrativeBox.setText(32);
        }
    }

    public void ChangeScene()
    {
        if (level == 3)
        {
            level = 0;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    // Use this for initialization
    void Start()
    {
        if (playerCharacter == null)
        {
            this.playerCharacter = new Player();
        }
    }

    void FixedUpdate()
    {
        this.playerCharacter.Update();
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
                //isDeadLocal = false;
                //DeadOverlay.SetActive(false);
                //this.playerCharacter.Wounds = 0;
                //this.playerCharacter.Update();
                this.playerCharacter = new Player();
                Time.timeScale = 1;
                level = 0; 
                ChangeScene();
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
