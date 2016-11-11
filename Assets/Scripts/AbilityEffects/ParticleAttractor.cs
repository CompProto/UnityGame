using UnityEngine;
using System.Collections;

public class ParticleAttractor : MonoBehaviour
{

    public GameObject source;
    [Range(1.0f, 10.0f)]
    public float duration = 3.0f;

    private GameObject target;
    private OrbitManager orbitManager;
    private ParticleSystem p;
    private float timer;

    // Use this for initialization
    void Start()
    {
        if (source == null)
        {
            source = gameObject;
            Debug.Log("ParticleAttractor.cs : Source is null.");
        }
        p = source.GetComponent<ParticleSystem>();
        orbitManager = source.GetComponent<OrbitManager>();
        timer = 0;
        target = source;
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        if (timer >= 0.0f)
        {
            target = source;
            p.Stop();
            orbitManager.ToggleAstralPresence();
        }
        else
        {
            int count = p.particleCount;
           // Debug.Log("Particle Attractor running on particles: " + count);
            ParticleSystem.Particle[] particles = new ParticleSystem.Particle[count];
            p.GetParticles(particles);
            for (int i = 0; i < count; i++)
            {
                float distance = Vector3.Distance(particles[i].position, target.transform.position);
                particles[i].position = Vector3.Lerp(particles[i].position, target.transform.position, 15 * Time.deltaTime / (distance * distance));
            }

            p.SetParticles(particles, count);
        }
    }

    public void AttackTarget(GameObject _target)
    {
        this.target = _target;
        timer = -1 * duration;
        p.Play();
        orbitManager.ToggleAstralPresence();
    }
}
