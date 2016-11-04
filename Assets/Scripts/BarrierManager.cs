using UnityEngine;
using System.Collections;

public class BarrierManager : MonoBehaviour {

    [Range(1.0f,20.0f)]
    public float duration = 5.0f;

    private ParticleSystem barrierParticles;
    private float timer;
    private bool isActive;

	// Use this for initialization
	void Start () {
        barrierParticles = gameObject.GetComponent<ParticleSystem>();
	}
	
	void FixedUpdate () {
        timer += Time.fixedDeltaTime;
        if(timer >= 0)
        {
            isActive = false;
            barrierParticles.Stop();
        }
	}

    public void ActivateBarrier()
    {
        isActive = true;
        barrierParticles.Play();
        timer = -duration;
    }
}
