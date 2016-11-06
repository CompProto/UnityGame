using UnityEngine;
using System.Collections;

public class ModeManager : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float MusicVolume = 0.5f;
    public AudioClip DayMusic, ShadowMusic;
    public GameObject DarkSmoke, WhiteSmoke;
    public GameObject[] gameObjects;
    public Material[] materials; // There must be 2 materials for each gameobject. First material is the white version, second material is dark version
    
    public bool isDarkMode = false; 

    public float duration = 2.0f; // How long to swap between the materials

    private AudioSource source;
    private ParticleSystem _DarkSmoke, _WhiteSmoke;
    private float timer = 1.0f;
    private bool soundChanged;

    // Use this for initialization
    void Start()
    {
        if (gameObjects.Length * 2 != materials.Length)
        {
            Debug.Log("ERROR! There should be exactly 2 materials pr. gameobject in the ModeManager");
        }
        source = GetComponent<AudioSource>();
        _DarkSmoke = DarkSmoke.GetComponent<ParticleSystem>();
        _WhiteSmoke = WhiteSmoke.GetComponent<ParticleSystem>();
        Play(DayMusic);
        _WhiteSmoke.Play();
    }

    public void PlayDayMusic()
    {
        _DarkSmoke.Stop();
        _WhiteSmoke.Play();
        Play(DayMusic);
    }

    public void PlayShadowMusic()
    {
        _WhiteSmoke.Stop();
        _DarkSmoke.Play();
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
        if (timer < 0.0f)
            return;

        isDarkMode = !isDarkMode;
        Debug.Log("Changing materials and music.");
        soundChanged = false;
        timer = -duration;
        GameManager.instance.isDarkMode = isDarkMode;
    }

    void Update()
    {
        if (timer < 0.0f)
        {
            float lerp = Mathf.PingPong(Time.time, 2.0f) / 2.0f;
            for (int i = 0; i < gameObjects.Length; i++)
            {
                if (isDarkMode)
                    gameObjects[i].GetComponent<Renderer>().material.Lerp(materials[2 * i], materials[2 * i + 1], timer + 2);
                else
                    gameObjects[i].GetComponent<Renderer>().material.Lerp(materials[2 * i + 1], materials[2 * i], timer + 2);
            }
            ManageMusic();
        }
        timer += Time.deltaTime;
    }

    private void ManageMusic()
    {
        if (!soundChanged)
        {
            if (source.volume > 0)
                source.volume -= Time.deltaTime;
            else
            {
                if (isDarkMode)
                    PlayShadowMusic();
                else
                    PlayDayMusic();
                soundChanged = true;
            }
        }
        else if (source.volume < MusicVolume)
        {
            source.volume += Time.deltaTime;
        }
    }
}
