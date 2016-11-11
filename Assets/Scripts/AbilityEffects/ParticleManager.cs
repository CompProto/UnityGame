using UnityEngine;
using System.Collections;
using Particle = UnityEngine.ParticleSystem.Particle;

public class ParticleManager : MonoBehaviour {

    public GameObject target;
    public GameObject source;
    private ParticleSystem p;

	// Use this for initialization
	void Start () {
        if(source == null)
        {
            source = gameObject;
        }
        p = source.GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        int count = p.particleCount;
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[count];
        p.GetParticles(particles);
	    for(int i=0; i < count; i++)
        {
            particles[i].position = Vector3.Lerp(particles[i].position, target.transform.position,Time.deltaTime / 2.0f);
        }

        p.SetParticles(particles, count);
	}
}
