using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour {
 
    /*
     * A simple script to hold variables about the number of enemies, players, etc that needs to be spawned in the game. 
     * The actual spawning of objects will be handled in MapGenerator.cs
     */
    public GameObject Player;
    public GameObject Enemy;
    public int EnemiesNumber = 10;
    [Range(1.0f, 50.0f)]
    public float MinimumDistance = 10.0f; // Minimum distance between enemies

}
