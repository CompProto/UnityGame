using UnityEngine;
using System.Collections;

public class SpellManager : MonoBehaviour {

    public GameObject Camera;

    public GameObject BlackHole;

    private Camera DungeonCam;
    private AudioSource source;
    private ParticleAttractor pAttracktor;


    // Use this for initialization
    void Start () {
        DungeonCam = Camera.GetComponent<Camera>();
        source = GetComponent<AudioSource>();
        pAttracktor = GetComponent<ParticleAttractor>();
    }
	
	// Update is called once per frame
	void Update () {

        // ACTION BAR KEYPRESS 1
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
                source.Play();
            }
        }

        // ACTION BAR KEYPRESS 2
        if (Input.GetKeyDown(KeyCode.Alpha2))
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

    }
}
