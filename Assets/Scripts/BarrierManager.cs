using UnityEngine;
using System.Collections;
using Mechanics.Objects;
using Mechanics.Objects.Abilities;

public class BarrierManager : MonoBehaviour
{

    [Range(1.0f, 20.0f)]
    public float duration = BarrierEffect.Duration;

    public AudioClip BarrierSound;

    private ParticleSystem barrierParticles;
    private float timer;
    private bool isActive;
    private AudioSource source;

    // Use this for initialization
    void Start()
    {
        barrierParticles = gameObject.GetComponent<ParticleSystem>();
        source = gameObject.GetComponent<AudioSource>();
        source.clip = BarrierSound;
    }

    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= 0 || GameManager.instance.playerCharacter.Absorb <= 0f)
        {
            isActive = false;
            barrierParticles.Stop();

            if (source.volume > 0)
                source.volume -= Time.fixedDeltaTime;
            else if (source.isPlaying)
            {
                source.Stop();
            }
            else if (!source.isPlaying)
            {
                GameManager.instance.playerCharacter.EndAbility(MECHANICS.ABILITIES.ENERGY_BARRIER);
            }
        }

    }

    public void ActivateBarrier()
    {
        isActive = true;
        barrierParticles.Play();
        timer = -duration;
        source.volume = 1.0f;
        source.Play();
        GameManager.instance.playerCharacter.UseAbility(MECHANICS.ABILITIES.ENERGY_BARRIER, null, 1f);
    }
}
