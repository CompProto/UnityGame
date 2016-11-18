using UnityEngine;
using System.Collections;

public class RangedAttackImpactEffect : MonoBehaviour
{

    public float duration = 0.5f;
    public ParticleSystem particles;

    private float timer;
    private bool marked;
    //   private Color startColor;

    public RangedAttackImpactEffect()
    {
        timer = -duration;
    }

    // Use this for initialization
    void Start()
    {
        if (particles == null)
            particles = GetComponent<ParticleSystem>();

        particles.Play();
        //    startColor = gameObject.GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.0f)
        {
            particles.Stop();
            if (!marked)
            {
                Destroy(gameObject, 0.05f);
                marked = true;
            }
        }
        //  Color currentColor = startColor;
        //   currentColor.a = -timer/duration;
        //  gameObject.GetComponent<Renderer>().material.color = currentColor;

    }



}
