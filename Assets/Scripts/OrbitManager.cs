﻿using UnityEngine;
using System.Collections;
using Mechanics.Objects;
using Particle = UnityEngine.ParticleSystem.Particle;

public class OrbitManager : MonoBehaviour
{

    public GameObject target; // Usually the player

    private ParticleSystem p;
    private float timer;

    // Use this for initialization
    void Start()
    {
        p = GetComponent<ParticleSystem>();
        timer = 0;
    }

    void Update()
    {
        if (p.isPlaying && !GameManager.instance.playerCharacter.CanUse(MECHANICS.ABILITIES.ASTRAL_PRESENCE))
            ToggleAstralPresence();

        if (!p.isPlaying)
            return;

        if (GameManager.instance.isDarkMode)
        {
            Collider[] candidates = Physics.OverlapSphere(target.transform.position, Mechanics.Objects.Abilities.AstralPressenceMechanic.HitRange);
            foreach (Collider c in candidates)
            {
                if (c.gameObject.tag == GameManager.instance.EnemyTag)
                {
                    // TODO apply damage to target. Damage should probably be: damage = (TotalAbilityDamageForEntireDuration / Time.deltaTime)
                }
            }
        }

        int count = p.particleCount;
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[count];
        p.GetParticles(particles);

        float tarX = target.transform.position.x;
        float tarZ = target.transform.position.z;
        float tarY = target.transform.position.y + 0.3f;

        timer += Time.deltaTime;

        float radius = 1.0f;
        for (int i = 0; i < count; i++)
        {
            float radians = timer + (i + 1) / (2 * Mathf.PI);
            float noise = Random.Range(0.1f, 1.0f);
            float x = tarX + noise * radius * Mathf.Cos(radians);
            float z = tarZ + noise * radius * Mathf.Sin(radians);
            Vector3 pos = new Vector3(x, tarY, z);
            float distance = Vector3.Distance(particles[i].position, target.transform.position);
            particles[i].position = Vector3.Lerp(particles[i].position, pos, 8 * Time.deltaTime / (distance * distance));
        }

        p.SetParticles(particles, count);
    }

    public void ToggleAstralPresence()
    {
        if (!p.isPlaying && GameManager.instance.playerCharacter.CanUse(MECHANICS.ABILITIES.ASTRAL_PRESENCE))
        {
            timer = 0;
            p.Play();
            // TODO start mechanics ability here. This should start draining energy.
        }
        else
        {
            timer = 0;
            p.Stop();
            // TODO stop mechanics ability here. Energy will no longer be drained.
        }
    }
}
