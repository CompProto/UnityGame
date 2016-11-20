using UnityEngine;
using System.Collections;

public class LevelUpController : MonoBehaviour {


    public AudioClip LevelUpSound;
    private AudioSource source;
    private ParticleSystem particles;

    private float timer;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        source.clip = LevelUpSound;
        timer = 0;
        particles = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= 0 && particles.isPlaying)
        {
            particles.Stop();
        }
    }

    public void PlayLevelUpEffects()
    {
        source.Play();
        particles.Play();
        timer = -2.0f;
    }
}
