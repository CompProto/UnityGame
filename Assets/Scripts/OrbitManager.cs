using UnityEngine;
using System.Collections;
using Particle = UnityEngine.ParticleSystem.Particle;

public class OrbitManager : MonoBehaviour {

    public GameObject target;

    private ParticleSystem p;

    private bool isRotating = false;

	// Use this for initialization
	void Start () {
        p = GetComponent<ParticleSystem>();
    }

    float time = 0;
	
	// Update is called once per frame
	void Update () {
       // if(!isRotating)
        //    return;

        int count = p.particleCount;
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[count];
        p.GetParticles(particles);

        float tarX = target.transform.position.x;
        float tarZ = target.transform.position.z;
        float tarY = target.transform.position.y + 0.3f;

        time += Time.deltaTime;

        float radius = 1.0f;
        for (int i = 0; i < count; i++)
        {
            float radians = time + (i + 1) / (2 * Mathf.PI);
            float noise = Random.Range(0.1f, 1.0f);
            float x = tarX + noise * radius * Mathf.Cos(radians);
            float z = tarZ + noise * radius * Mathf.Sin(radians);
            Vector3 pos = new Vector3(x, tarY, z);
            float distance = Vector3.Distance(particles[i].position, target.transform.position);
            particles[i].position = Vector3.Lerp(particles[i].position, pos,  8 * Time.deltaTime / (distance * distance));
        }

        p.SetParticles(particles, count);
    }

    public void setActive(bool b)
    {
        this.isRotating = b;
    }
}
