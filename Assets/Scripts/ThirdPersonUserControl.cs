using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;



[RequireComponent(typeof(ThirdPersonCharacter))]
public class ThirdPersonUserControl : MonoBehaviour
{

    public ParticleSystem LevelUp;

    private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
    private Transform m_Cam;                  // A reference to the main camera in the scenes transform
    private Vector3 m_CamForward;             // The current forward direction of the camera
    private Vector3 m_Move;
    private bool m_Jump, charge;                      // the world-relative desired move direction, calculated from the camForward and user input.

    private SpellManager spellManager;
    private MapGenerator mapGenerator;

    private Vector3 output;
    private Ray ray;
    private bool go = false;
    private RaycastHit hit;

    private void Start()
    {
        // get the transform of the main camera
        if (Camera.main != null)
        {
            m_Cam = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning(
                "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
            // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
        }

        // get the third person character ( this should never be null due to require component )
        m_Character = GetComponent<ThirdPersonCharacter>();

        mapGenerator = GameObject.FindGameObjectWithTag("MapGenerator").GetComponent<MapGenerator>();
    }

    private void Update()
    {
        charge = Input.GetMouseButtonDown(1); // Right mouseclick to charge
        
        if (Input.GetMouseButton(0) && !Input.GetKey(KeyCode.LeftShift) || (charge))
        {
            hit = new RaycastHit();
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 200))
            {
                if (mapGenerator.map[(int)hit.point.x+(mapGenerator.width/2), (int)hit.point.z+(mapGenerator.height/2)] == 0)
                {
                output = hit.point - gameObject.transform.position;
                output.Normalize();
                go = true;
                }
            }
        }

        if (!m_Jump)
        {
           // m_Jump = CrossPlatformInputManager.GetButtonDown("Jump"); // Should we be able to jump?
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            LevelUp.Play();
        }
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        if (GameManager.instance.isDead)
        {
            m_Character.Move(new Vector3(0,0,0), false, false);
            return;
        }

        if (go)
        {
            if ((0.75).CompareTo(Vector3.Distance(gameObject.transform.position, hit.point)) == 1)
            {
                go = false;
            }
        }

        if (go)
        {
            m_Move = output;
        }
        else
        {
            m_Move = 0 * Vector3.one;
            output = 0 * Vector3.one;
        }

#if !MOBILE_INPUT
        // walk speed multiplier
        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif
        if (charge)
        {
            charge = GameManager.instance.playerCharacter.CanUse(Mechanics.Objects.MECHANICS.ABILITIES.CHARGE);
        }
        // pass all parameters to the character control script
        m_Character.Move(m_Move, charge, m_Jump);
        m_Jump = false;
    }
}

