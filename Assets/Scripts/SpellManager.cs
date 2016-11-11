using UnityEngine;
using System.Collections;
using Mechanics.Objects;
using Mechanics.Objects.Abilities;

public class SpellManager : MonoBehaviour {

    public GameObject modeManager;
    public GameObject Camera;
    public GameObject BlackHole;
    public GameObject EnergyBomb;
    public GameObject StandardAttack;
    public GameObject DimensionDoor;
    public GameObject Barrier;
    public GameObject AstralPressence;

    public AudioClip BlackHoleSound;

    private Camera DungeonCam;
    private AudioSource source;
    private ParticleAttractor pAttracktor;
    private ModeManager modeChanger;
    private Transform PlayerPosition;
    private DimensionDoorEffect doorManager;
    private BarrierEffect barrierManager;
    private OrbitManager _OrbitManager;


    // Use this for initialization
    void Start ()
    {
        DungeonCam = Camera.GetComponent<Camera>();
        source = GetComponent<AudioSource>();
        pAttracktor = GetComponent<ParticleAttractor>();
        modeChanger = modeManager.GetComponent<ModeManager>();
        PlayerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        doorManager = DimensionDoor.GetComponent<DimensionDoorEffect>();
        barrierManager = Barrier.GetComponent<BarrierEffect>();
        _OrbitManager = AstralPressence.GetComponent<OrbitManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (GameManager.instance.isDead)
            return;

        // ACTION BAR KEYPRESS 1 - ASTRAL PRESENCE
        if (Input.GetKeyDown(KeyCode.Alpha1)) // Check for whether ability can be used has to be in OrbitManager script as this is a channeled ability
        {
            _OrbitManager.ToggleAstralPresence();
        }

        // ACTION BAR KEYPRESS 2 - DIMENSION DOOR
        if (Input.GetKeyDown(KeyCode.Alpha2) && GameManager.instance.playerCharacter.CanUse(MECHANICS.ABILITIES.DIMENSION_DOOR))
        {
            doorManager.CastDimensionDoor();
        }

        // ACTION BAR KEYPRESS 3 - ENERGY BARRIER
        if (Input.GetKeyDown(KeyCode.Alpha3) && GameManager.instance.playerCharacter.CanUse(MECHANICS.ABILITIES.ENERGY_BARRIER))
        {
            barrierManager.ActivateBarrier();
        }

        // ACTION BAR KEYPRESS 4 - BLACK HOLE
        if (Input.GetKeyDown(KeyCode.Alpha4) && GameManager.instance.playerCharacter.CanUse(MECHANICS.ABILITIES.BLACKHOLE))
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

        // ACTION BAR KEYPRESS 5 - ENERGY BOMB
        if (Input.GetKeyDown(KeyCode.Alpha5) && GameManager.instance.playerCharacter.CanUse(MECHANICS.ABILITIES.ENERGY_BOMB)) 
        {
            Ray ray = DungeonCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit vHit = new RaycastHit();
            if (Physics.Raycast(ray, out vHit, 200))
            {               
                Vector3 spawnPos = PlayerPosition.position;
                spawnPos.y += 1.25f;
                GameObject bomb = (GameObject) Instantiate(EnergyBomb, spawnPos, transform.rotation);
                EnergyBombEffect eController = bomb.GetComponent<EnergyBombEffect>();
                eController.ThrowBomb(vHit.point);
            }
        }

        // ACTION BAR KEYPRESS TAB - SHADOW WALK
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            modeChanger.ChangeMode();
        }

        // ACTION BAR KEYPRESS SHIFT + LEFTMOUSE CLICK - STANDARD ATTACK
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButtonDown(0) && GameManager.instance.playerCharacter.CanUse(MECHANICS.ABILITIES.PSYCHO_KINESIS)) 
        {
            Ray ray = DungeonCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit vHit = new RaycastHit();
            if (Physics.Raycast(ray, out vHit, 200))
            {
                Vector3 spawnPos = PlayerPosition.position;
                spawnPos.y += 1.25f;
                GameObject atk = (GameObject)Instantiate(StandardAttack, spawnPos, transform.rotation);
                PsychokinesisEffect eController = atk.GetComponent<PsychokinesisEffect>();
                eController.ThrowBomb(vHit.point);
            }
        }

    }
}
