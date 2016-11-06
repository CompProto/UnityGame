using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public bool isDarkMode { get; set; }
    
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
	void Start () {
	
	}
	
}
