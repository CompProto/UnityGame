using UnityEngine;
using System.Collections;

public class NovaEffect : MonoBehaviour {

    public float duration = 2.0f;
    public ParticleSystem particles;

    private float timer;
    private bool marked;
 //   private Color startColor;

    public NovaEffect()
    {
        timer = -duration;
    }

	// Use this for initialization
	void Start () {
        particles.Play();
    //    startColor = gameObject.GetComponent<Renderer>().material.color;
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= 0.0f || transform.localScale.magnitude >= 15)
        {
            particles.Stop();
            if (!marked)
            {
                Destroy(gameObject, 0.05f);
                marked = true;
            }
        }
        else
        {
            transform.localScale *= 1 - (timer/duration) / 5;
        }
      //  Color currentColor = startColor;
     //   currentColor.a = -timer/duration;
      //  gameObject.GetComponent<Renderer>().material.color = currentColor;

    }



}
