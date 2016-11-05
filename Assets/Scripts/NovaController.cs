using UnityEngine;
using System.Collections;

public class NovaController : MonoBehaviour {

    public float duration = 2.0f;
    public ParticleSystem particles;

    private float timer;
    private bool marked;

    public NovaController()
    {
        timer = -duration;
    }

	// Use this for initialization
	void Start () {
        particles.Play();
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= 0.0f || transform.localScale.magnitude >= 10)
        {
            particles.Stop();
            if (!marked)
            {
                Destroy(gameObject);
                marked = true;
            }
        }
        else
        {
            transform.localScale *= 1 - timer/7.5f;
        }
	}



}
