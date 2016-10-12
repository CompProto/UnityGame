using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{

    public AudioClip DayMusic, ShadowMusic;

    private AudioSource source;

    // Use this for initialization
    void Start()
    {
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
        if (source.isPlaying)
        {
            source.Stop();
        }
        source.clip = sound;
        source.Play();
    }


}
