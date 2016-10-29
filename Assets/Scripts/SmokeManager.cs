using UnityEngine;
using System.Collections;

public class SmokeManager : MonoBehaviour {

    public GameObject Player;
    public float offset = 0.5f;
    private ParticleSystem p;

    // Use this for initialization
    void Start()
    {
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        p = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        int count = p.particleCount;
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[count];
        p.GetParticles(particles);
        Vector3 pos = Player.transform.position;
        pos.y += 1;
        for (int i = 0; i < count; i++)
        {
            particles[i].position = Vector3.Lerp(particles[i].position, pos, ((i+1) / 2) * offset * Time.deltaTime);
        }

        p.SetParticles(particles, count);
    }
}
