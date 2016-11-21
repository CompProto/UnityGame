using UnityEngine;
using System.Collections;

public class CreepySoundsController : MonoBehaviour {

    public AudioClip[] CreepySounds;

    private AudioSource source;
    private float timer;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        timer = - 6; // First effect played after this time
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer >= 0)
        {
            PlayClip(Random.Range(0, CreepySounds.Length - 1)); // Play a random effect
            timer = -Random.Range(25, 40); // Time untill next soundeffect starts
        }
	}

    void PlayClip(int i)
    {
        if (source.isPlaying)
            return;
        source.clip = CreepySounds[i];
        source.Play();
    }
}
