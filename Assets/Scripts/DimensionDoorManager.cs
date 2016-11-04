using UnityEngine;
using System.Collections;

public class DimensionDoorManager : MonoBehaviour {

    public GameObject Player;
    public GameObject Door; // Prefab with particlesystem that can be spawned where the door should be. Also holds the position
    public GameObject Implosion; // Must have same position as Player. Non-looping particle system, play on awake
    public GameObject Explosion; // Must have same position as Door. Non-looping particle system, play on awake

    private bool hasDoor; // true if a door has been placed
    private ParticleSystem doorParticles; // Looping particlesystem that illustrates the door position
    private GameObject _door; // The instantiated prefab
    private GameObject _implosion;
    private GameObject _explosion;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CastDimensionDoor()
    {
        if (hasDoor)
            UseDimensionDoor();
         else
            PlaceDimensionDoor();
    }

    private void UseDimensionDoor()
    {
        _implosion = (GameObject)Instantiate(Implosion, Player.transform.position, Player.transform.rotation);
        _explosion = (GameObject)Instantiate(Explosion, _door.transform.position, Player.transform.rotation);
        Player.transform.position = _door.transform.position; // "Teleport" the player
        doorParticles.Stop();
        Destroy(_door, 2.0f);
        Destroy(_implosion, 2.5f);
        Destroy(_explosion, 2.5f);
        hasDoor = false;
    }

    private void PlaceDimensionDoor()
    {
        Vector3 spawnPos = Player.transform.position;
        spawnPos.y += 0.2f;
        _door = (GameObject) Instantiate(Door, spawnPos, Player.transform.rotation);
        doorParticles = _door.GetComponent<ParticleSystem>();
        doorParticles.Play();
        hasDoor = true;
    }
}
