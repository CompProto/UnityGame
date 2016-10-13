using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour {

    [Range(0.5f,10.0f)]
    public float duration = 3.0f;
    [Range(0.5f, 10.0f)]
    public float range = 5.0f;

    public string EnemyTag = "PowerUp";

    public float force = 50;

    private ParticleSystem orb;
    private float timer;

	// Use this for initialization
	void Start () {
        orb = GetComponent<ParticleSystem>();
        timer = duration;
    }
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if(timer <= 0.0f)
        {
            orb.Stop();
        }
        else
        {
            Collider[] hits = Physics.OverlapSphere(gameObject.transform.position, 5.0f);
            foreach (Collider candidate in hits)
            {
                if (candidate.gameObject.tag == EnemyTag)
                {
                    Vector3 forceDirection = candidate.transform.position - gameObject.transform.position;
                    candidate.gameObject.GetComponent<Rigidbody>().AddForce(-forceDirection.normalized * force, ForceMode.Force);
                }
            }
        }
	}
}
