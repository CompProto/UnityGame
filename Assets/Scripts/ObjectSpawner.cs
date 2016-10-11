using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour {
 
    public GameObject Player;
    public GameObject Enemy;
    public int EnemiesNumber = 10;

    private MapGenerator mapGen;
    private int width, height;

	// Use this for initialization
	void Start () {
        mapGen = GetComponent<MapGenerator>();
        mapGen.SetPlayer(Player);
        mapGen.SetEnemy(Enemy);
        mapGen.SetEnemiesNumber(EnemiesNumber);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
