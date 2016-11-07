using UnityEngine;
using System.Collections;

public class SpellManager : MonoBehaviour {

    public GameObject modeManager;
    public GameObject Camera;
    public GameObject BlackHole;
    public GameObject EnergyBomb;
    public GameObject DimensionDoor;
    public GameObject Barrier;

    public AudioClip BlackHoleSound;

    private Camera DungeonCam;
    private AudioSource source;
    private ParticleAttractor pAttracktor;
    private ModeManager modeChanger;
    private Transform PlayerPosition;
    private DimensionDoorManager doorManager;
    private BarrierManager barrierManager;
    private PlayerHealth _PlayerHealth;


    // Use this for initialization
    void Start () {
        DungeonCam = Camera.GetComponent<Camera>();
        source = GetComponent<AudioSource>();
        pAttracktor = GetComponent<ParticleAttractor>();
        modeChanger = modeManager.GetComponent<ModeManager>();
        PlayerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        doorManager = DimensionDoor.GetComponent<DimensionDoorManager>();
        barrierManager = Barrier.GetComponent<BarrierManager>();
        _PlayerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
	
	// Update is called once per frame
	void Update () {

        // ACTION BAR KEYPRESS 1 - BLACK HOLE
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Ray ray = DungeonCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit vHit = new RaycastHit();
            if (Physics.Raycast(ray, out vHit, 200))
            {
                Vector3 spawnPos = vHit.point;
                spawnPos.y += 1; // Spawn slightly above the ground
                //Debug.Log("Hit: " + vHit.point);
                Instantiate(BlackHole, spawnPos, transform.rotation);
                source.clip = BlackHoleSound;
                source.Play();
            }
        }

        // ACTION BAR KEYPRESS 2 - ASTRAL PRESENCE
        if (Input.GetKeyDown(KeyCode.Alpha2) && _PlayerHealth.SpendEnergy(25.0f)) // TODO - update with actual energy cost
        {
            Ray ray = DungeonCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit vHit = new RaycastHit();
            if (Physics.Raycast(ray, out vHit, 200))
            {
                Vector3 spawnPos = vHit.point;
                //spawnPos.y += 1; // Spawn slightly above the ground
                //Debug.Log("Hit: " + vHit.point);
                Collider[] candidates = Physics.OverlapSphere(spawnPos, 1.5f);
                foreach (Collider c in candidates)
                {
                    if (c.gameObject.tag == "PowerUp")
                    {
                        pAttracktor.AttackTarget(c.gameObject);
                        Debug.Log("Enemy found at: " + c.gameObject.transform.position); // DEBUG
                        break;
                    }
                }
            }
        }

        // ACTION BAR KEYPRESS 3 - ENERGY BOMB
        if (Input.GetKeyDown(KeyCode.Alpha3) && _PlayerHealth.SpendEnergy(10.0f)) // TODO - update with actual energy cost
        {
            Ray ray = DungeonCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit vHit = new RaycastHit();
            if (Physics.Raycast(ray, out vHit, 200))
            {               
                Vector3 spawnPos = PlayerPosition.position;
                spawnPos.y += 1.25f;
                GameObject bomb = (GameObject) Instantiate(EnergyBomb, spawnPos, transform.rotation);
                EnergyBombController eController = bomb.GetComponent<EnergyBombController>();
                eController.ThrowBomb(vHit.point);
            }
        }

        // ACTION BAR KEYPRESS 4 - DIMENSION DOOR
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            doorManager.CastDimensionDoor();
        }

        // ACTION BAR KEYPRESS 5
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            barrierManager.ActivateBarrier();
        }

        // ACTION BAR KEYPRESS R
        if (Input.GetKeyDown(KeyCode.R))
        {
            modeChanger.ChangeMode();
        }

     }
}
