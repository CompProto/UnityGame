using UnityEngine;
using System.Collections;
using Mechanics.Objects;
using Mechanics.Objects.Abilities;
using UnityEngine.UI;

public class SpellManager : MonoBehaviour
{

    public GameObject modeManager;
    public GameObject Camera;
    [Space(10)]
    [Header("Ability Prefabs")]
    public GameObject BlackHole;
    public GameObject EnergyBomb;
    public GameObject StandardAttack;
    public GameObject DimensionDoor;
    public GameObject Barrier;
    public GameObject AstralPressence;

    [Header("Ability UI")]
    public GameObject BlackHoleUI;
    public GameObject EnergyBombUI;
    public GameObject StandardAttackUI;
    public GameObject DimensionDoorUI;
    public GameObject BarrierUI;
    public GameObject AstralPressenceUI;
    public GameObject ChargeUI;

    public AudioClip BlackHoleSound;

    private Camera DungeonCam;
    private AudioSource source;
    private ParticleAttractor pAttracktor;
    private ModeManager modeChanger;
    private Transform PlayerPosition;
    private DimensionDoorEffect doorManager;
    private BarrierEffect barrierManager;
    private OrbitManager _OrbitManager;

    private Image BlackHoleImage;
    private Image EnergyBombImage;
    private Image StandardAttackImage;
    private Image DimensionDoorImage;
    private Image BarrierImage;
    private Image AstralPressenceImage;
    private Image ChargeImage;

    private Color canUseColor, canNotUseColor, activeColor;

    // Use this for initialization
    void Start()
    {
        DungeonCam = Camera.GetComponent<Camera>();
        source = GetComponent<AudioSource>();
        pAttracktor = GetComponent<ParticleAttractor>();
        modeChanger = modeManager.GetComponent<ModeManager>();
        PlayerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        doorManager = DimensionDoor.GetComponent<DimensionDoorEffect>();
        barrierManager = Barrier.GetComponent<BarrierEffect>();
        _OrbitManager = AstralPressence.GetComponent<OrbitManager>();

        BlackHoleImage = BlackHoleUI.GetComponent<Image>();
        EnergyBombImage = EnergyBombUI.GetComponent<Image>();
        StandardAttackImage = StandardAttackUI.GetComponent<Image>();
        DimensionDoorImage = DimensionDoorUI.GetComponent<Image>();
        BarrierImage = BarrierUI.GetComponent<Image>();
        AstralPressenceImage = AstralPressenceUI.GetComponent<Image>();
        ChargeImage = ChargeUI.GetComponent<Image>();
        canUseColor = BlackHoleImage.color;
        canNotUseColor = canUseColor;
        canNotUseColor.a = 0.3f;
        activeColor = Color.green;
        activeColor.a = 0.7f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isDead)
            return;

        // Update UI Images
        BlackHoleImage.color = GameManager.instance.playerCharacter.CanUse(MECHANICS.ABILITIES.BLACKHOLE) ? canUseColor : canNotUseColor;
        EnergyBombImage.color = GameManager.instance.playerCharacter.CanUse(MECHANICS.ABILITIES.ENERGY_BOMB) ? canUseColor : canNotUseColor;
        StandardAttackImage.color = GameManager.instance.playerCharacter.CanUse(MECHANICS.ABILITIES.PSYCHO_KINESIS) ? canUseColor : canNotUseColor;
        ChargeImage.color = GameManager.instance.playerCharacter.CanUse(MECHANICS.ABILITIES.CHARGE) ? canUseColor : canNotUseColor;

        if (GameManager.instance.playerCharacter.Absorb > 0)
            BarrierImage.color = activeColor;
        else
            BarrierImage.color = GameManager.instance.playerCharacter.CanUse(MECHANICS.ABILITIES.ENERGY_BARRIER) ? canUseColor : canNotUseColor;
        if (doorManager.hasDoor)
            DimensionDoorImage.color = activeColor;
        else
            DimensionDoorImage.color = GameManager.instance.playerCharacter.CanUse(MECHANICS.ABILITIES.DIMENSION_DOOR) ? canUseColor : canNotUseColor;
        if (_OrbitManager.isEnabled)
            AstralPressenceImage.color = activeColor;
        else
            AstralPressenceImage.color = GameManager.instance.playerCharacter.CanUse(MECHANICS.ABILITIES.ASTRAL_PRESENCE) ? canUseColor : canNotUseColor;

        // ACTION BAR KEYPRESS 1 - ASTRAL PRESENCE
        if (GameManager.instance.playerCharacter.CanUse(MECHANICS.ABILITIES.ASTRAL_PRESENCE))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) // Check for whether ability can be used has to be in OrbitManager script as this is a channeled ability
            {
                _OrbitManager.ToggleAstralPresence();
            }
        }

        // ACTION BAR KEYPRESS 2 - DIMENSION DOOR
        if (GameManager.instance.playerCharacter.CanUse(MECHANICS.ABILITIES.DIMENSION_DOOR))
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                doorManager.CastDimensionDoor();
            }
        }

        // ACTION BAR KEYPRESS 3 - ENERGY BARRIER
        if (GameManager.instance.playerCharacter.CanUse(MECHANICS.ABILITIES.ENERGY_BARRIER))
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                barrierManager.ActivateBarrier();
            }
        }

        // ACTION BAR KEYPRESS 4 - BLACK HOLE
        if (GameManager.instance.playerCharacter.CanUse(MECHANICS.ABILITIES.BLACKHOLE))
        {
            if (Input.GetKeyDown(KeyCode.Alpha4))
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
        }

        // ACTION BAR KEYPRESS 5 - ENERGY BOMB
        if (GameManager.instance.playerCharacter.CanUse(MECHANICS.ABILITIES.ENERGY_BOMB))
        {
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                Ray ray = DungeonCam.ScreenPointToRay(Input.mousePosition);
                RaycastHit vHit = new RaycastHit();
                if (Physics.Raycast(ray, out vHit, 200))
                {
                    Vector3 spawnPos = PlayerPosition.position;
                    spawnPos.y += 1.25f;
                    GameObject bomb = (GameObject)Instantiate(EnergyBomb, spawnPos, transform.rotation);
                    EnergyBombEffect eController = bomb.GetComponent<EnergyBombEffect>();
                    eController.ThrowBomb(vHit.point);
                }
            }
        }

        // ACTION BAR KEYPRESS TAB - SHADOW WALK
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            modeChanger.ChangeMode();
        }

        // ACTION BAR KEYPRESS SHIFT + LEFTMOUSE CLICK - STANDARD ATTACK
        if (GameManager.instance.playerCharacter.CanUse(MECHANICS.ABILITIES.PSYCHO_KINESIS))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = DungeonCam.ScreenPointToRay(Input.mousePosition);
                RaycastHit vHit = new RaycastHit();
                if (Physics.Raycast(ray, out vHit, 200) && (vHit.collider.gameObject.tag == "Enemy" || Input.GetKey(KeyCode.LeftShift)))
                {
                   // Debug.Log(vHit.collider.gameObject.tag);       
                    Vector3 spawnPos = PlayerPosition.position;
                    spawnPos.y += 1.25f;
                    GameObject atk = (GameObject)Instantiate(StandardAttack, spawnPos, transform.rotation);
                    PsychokinesisEffect eController = atk.GetComponent<PsychokinesisEffect>();
                    eController.ThrowBomb(vHit.point);
                }
            }
        }

    }
}
