using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Mechanics.Objects;

public class DimensionDoorEffect : MonoBehaviour {

    public GameObject Player;
    public GameObject Door; // Prefab with particlesystem that can be spawned where the door should be. Also holds the position
    public GameObject Implosion; // Must have same position as Player. Non-looping particle system, play on awake
    public GameObject Explosion; // Must have same position as Door. Non-looping particle system, play on awake
    public Image flash; // Image to be flashed when teleporting
    [Range(0.0f, 3.0f)]
    public float TimeToTeleport;
    [Range(0.5f, 10.0f)]
    public float TeleportRadius;
    public float flashSpeed = 5f;

    public bool hasDoor; // true if a door has been placed
    private ParticleSystem doorParticles; // Looping particlesystem that illustrates the door position
    private GameObject _door; // The instantiated prefab
    private GameObject _implosion;
    private GameObject _explosion;
    private float timer;
    private bool teleporting, enemyTeleporting;
    private AudioSource source;
    private Color flashColor;

    // Use this for initialization
    void Start () {
        timer = 0;
        source = gameObject.GetComponent<AudioSource>();
        flashColor = new Color(0.0f, 0.0f, 0.0f, 0.9f);
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
                flash.color = flashColor;
            }
        }
        else if (enemyTeleporting)
        {
            timer += Time.deltaTime;
            if(timer > TimeToTeleport * 4)
            {
                enemyTeleporting = false;
                timer = 0;
            }
            Collider[] hits = Physics.OverlapSphere(Player.transform.position, TeleportRadius);
            foreach (Collider candidate in hits)
            {
                if (candidate.gameObject.tag == GameManager.instance.EnemyTag)
                {
                    // Pull enemies towards the player
                    Vector3 forceDirection = candidate.transform.position - Player.transform.position;
                    candidate.gameObject.GetComponent<Rigidbody>().AddForce(-forceDirection.normalized * 50, ForceMode.Force);
                    if ((candidate.gameObject.transform.position - Player.transform.position).magnitude < 1.5f)
                    {
                        // Teleport the enemy when it's close to the player
                        candidate.gameObject.transform.position = _door.transform.position;
                    }
                }
            }
        }
        else
        {
            flash.color = Color.Lerp(flash.color, Color.clear, Time.deltaTime * flashSpeed);
        }
	}

    public void CastDimensionDoor()
    {
        if (this.hasDoor)
        {
            if (GameManager.instance.isDarkMode) // Dark mode
            {
                TeleportEnemies();
            }
            else
            {
                UseDimensionDoor();
            }
            GameManager.instance.playerCharacter.EndAbility(MECHANICS.ABILITIES.DIMENSION_DOOR);
        }
        else
        {
            GameManager.instance.playerCharacter.UseAbility(MECHANICS.ABILITIES.DIMENSION_DOOR, null, 0f);
            PlaceDimensionDoor();
        }
    }

    private void TeleportEnemies()
    {
        StartEffects();
        enemyTeleporting = true;
        hasDoor = false;
    }

    private void StartEffects()
    {
        _implosion = (GameObject)Instantiate(Implosion, Player.transform.position, Player.transform.rotation);
        _explosion = (GameObject)Instantiate(Explosion, _door.transform.position, Player.transform.rotation);
        source.Play();
        doorParticles.Stop();
        Destroy(_door, 2.0f);
        Destroy(_implosion, 2.5f);
        Destroy(_explosion, 2.5f);
    }

    private void UseDimensionDoor()
    {
        StartEffects();
        teleporting = true;
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
