using UnityEngine;
using System.Collections;
using Mechanics.Objects;
using Mechanics.Objects.Abilities;

public class BarrierEffect : MonoBehaviour
{
    public AudioClip BarrierSound;
    private ParticleSystem barrierParticles;
    private float timer;
    private AudioSource source;

    // Use this for initialization
    void Start()
    {
        this.barrierParticles = gameObject.GetComponent<ParticleSystem>();
        this.source = gameObject.GetComponent<AudioSource>();
        this.source.clip = BarrierSound;
    }

    void FixedUpdate()
    {
        this.timer += Time.fixedDeltaTime;
        if (this.timer >= 0 || GameManager.instance.playerCharacter.Absorb <= 0f)
        {
            this.barrierParticles.Stop();
            if (this.source.volume > 0)
            {
                this.source.volume -= Time.fixedDeltaTime * 5;
            }
            else if (this.source.isPlaying)
            {
                this.source.Stop();
            }
            else if (!this.source.isPlaying)
            {
                GameManager.instance.playerCharacter.EndAbility(MECHANICS.ABILITIES.ENERGY_BARRIER);
            }
        }
    }

    public void ActivateBarrier()
    {
        this.barrierParticles.Play();
        this.timer = -BarrierMechanic.Duration;
        this.source.volume = 1.0f;
        this.source.Play();
        GameManager.instance.playerCharacter.UseAbility(MECHANICS.ABILITIES.ENERGY_BARRIER, null, 1f);
    }
}
