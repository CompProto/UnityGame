using UnityEngine;
using System.Collections;

public class DimensionDoorManager : MonoBehaviour {

    public GameObject Player;
    public GameObject Door; // Prefab with particlesystem that can be spawned where the door should be. Also holds the position
    public GameObject Implosion; // Must have same position as Player. Non-looping particle system, play on awake
    public GameObject Explosion; // Must have same position as Door. Non-looping particle system, play on awake
    [Range(0.0f, 3.0f)]
    public float TimeToTeleport;

    private bool hasDoor; // true if a door has been placed
    private ParticleSystem doorParticles; // Looping particlesystem that illustrates the door position
    private GameObject _door; // The instantiated prefab
    private GameObject _implosion;
    private GameObject _explosion;
    private float timer;
    private bool teleporting;
    private AudioSource source;

    // Use this for initialization
    void Start () {
        timer = 0;
        source = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {    
	    if(teleporting)
        {
            timer += Time.deltaTime;
            if(timer > TimeToTeleport)
            {
                Player.transform.position = _door.transform.position; // "Teleport" the player
                teleporting = false;
                timer = 0;
            }
        }
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
        teleporting = true;
        source.Play();
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
