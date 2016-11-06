using UnityEngine;
using System.Collections;

public class BarrierManager : MonoBehaviour {

    [Range(1.0f,20.0f)]
    public float duration = 5.0f;

    public AudioClip BarrierSound;

    private ParticleSystem barrierParticles;
    private float timer;
    private bool isActive;
    private AudioSource source;

	// Use this for initialization
	void Start () {
        barrierParticles = gameObject.GetComponent<ParticleSystem>();
        source = gameObject.GetComponent<AudioSource>();
        source.clip = BarrierSound;
	}
	
	void FixedUpdate () {
        timer += Time.fixedDeltaTime;
        if(timer >= 0)
        {
            isActive = false;
            barrierParticles.Stop();

            if (source.volume > 0)
                source.volume -= Time.fixedDeltaTime;
            else if (source.isPlaying)
                source.Stop();
        }
	}

    public void ActivateBarrier()
    {
        isActive = true;
        barrierParticles.Play();
        timer = -duration;
        source.volume = 1.0f;
        source.Play();
    }
}
