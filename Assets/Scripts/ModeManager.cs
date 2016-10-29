using UnityEngine;
using System.Collections;

public class ModeManager : MonoBehaviour {

    public AudioClip DayMusic, ShadowMusic;
    public GameObject[] gameObjects;
    public Material[] materials;

    public bool isDarkMode = false;

    private AudioSource source;

	// Use this for initialization
	void Start () {
	    if(gameObjects.Length * 2 != materials.Length)
        {
            Debug.Log("ERROR! There should be exactly 2 materials pr. gameobject in the ModeManager");
        }
        source = GetComponent<AudioSource>();
        PlayDayMusic();
    }

    public void PlayDayMusic()
    {
        Play(DayMusic);
    }

    public void PlayShadowMusic()
    {
        Play(ShadowMusic);
    }

    private void Play(AudioClip sound)
    {
        if (source.clip == sound && source.isPlaying)
            return;

        if (source.isPlaying)
        {
            source.Stop();
        }

        source.clip = sound;
        source.Play();
    }

    public void ChangeMode()
    {
        isDarkMode = !isDarkMode;
        Debug.Log("Changing materials and music.");
        int offset = 0;
        if (isDarkMode)
        {
            offset = 1;
            PlayShadowMusic();
        }
        else
        {
            PlayDayMusic();
        }
        for(int i=0; i < gameObjects.Length; i++)
        {
            gameObjects[i].GetComponent<Renderer>().material = materials[i * 2 + offset];
        }
    }
}
