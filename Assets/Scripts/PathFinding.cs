using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinding : MonoBehaviour

{
    // Variabler der hører til GameObjektet


    // Variabler der skal hentes fra eksterne kilder
    private MapGenerator mapGen;
    int[,] map;
    private Vector3 playerPosition;

    // Variabler til at holde styr på pathfinding;
    int count = 0;
    List<Vector3> movementList = new List<Vector3>();
    List<Vector3> searchQueue = new List<Vector3>();
    Vector3 start;


    void Start()
    {
        // mapGen og map sættes, så man kan søge i det tilgænglige map.
        mapGen = (MapGenerator)FindObjectOfType(typeof(MapGenerator));
        map = mapGen.map;
        // Players position hentes
        playerPosition = ((Player)GameObject.FindObjectOfType(typeof(Player))).transform.position;
        //enemy position sættes 
        start = transform.position;

        CalculatePath();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void CalculatePath()
    {
        List<Vector3> currentNeighbours = new List<Vector3>();
       
    }
}
