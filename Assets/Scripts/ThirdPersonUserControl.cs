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
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            hit = new RaycastHit();
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 200))
            {
                if (hit.collider.gameObject.tag=="Ground")
                {
                output = hit.point - gameObject.transform.position;
                output.Normalize();
                go = true;
                }
            }
        }

        if (!m_Jump)
        {
            m_Jump = CrossPlatformInputManager.GetButtonDown("Jump"); // Should we be able to jump?
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            LevelUp.Play();
        }
        charge = Input.GetMouseButtonDown(1); // Right mouseclick to charge
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
            if ((0.5).CompareTo(Vector3.Distance(gameObject.transform.position, hit.point)) == 1)
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

        //// read inputs
        //float h = CrossPlatformInputManager.GetAxis("Horizontal");
        //float v = CrossPlatformInputManager.GetAxis("Vertical");
        
        //// calculate move direction to pass to character
        //if (m_Cam != null)
        //{
        //    // calculate camera relative direction to move:
        //    m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
        //    m_Move = v * m_CamForward + h * m_Cam.right;
        //}
        //else
        //{
        //    // we use world-relative directions in the case of no main camera
        //    m_Move = v * Vector3.forward + h * Vector3.right;
        //}
#if !MOBILE_INPUT
        // walk speed multiplier
        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif
        // pass all parameters to the character control script
        m_Character.Move(m_Move, charge, m_Jump);
        m_Jump = false;
    }
}

