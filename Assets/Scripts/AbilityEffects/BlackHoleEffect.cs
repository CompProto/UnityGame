using UnityEngine;
using System.Collections;
using Mechanics.Objects;
using Mechanics.Objects.Abilities;

public class BlackHoleEffect : MonoBehaviour
{
    public float force = 50;
    private ParticleSystem orb;
    private float timer;
    private float damageFactor;

    // Use this for initialization
    void Start()
    {
        orb = GetComponent<ParticleSystem>();
        timer = BlackHoleMechanic.Duration;
        this.damageFactor = 1f / (30.0f * BlackHoleMechanic.Duration);
        GameManager.instance.playerCharacter.UseAbility(MECHANICS.ABILITIES.BLACKHOLE, null, this.damageFactor);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer -= Time.fixedDeltaTime;
        if (timer <= 0.0f && orb.isPlaying)
        {
            orb.Stop();                      
        }
        else if (!orb.isPlaying)
        {
            GameManager.instance.playerCharacter.EndAbility(MECHANICS.ABILITIES.BLACKHOLE);
            Destroy(gameObject);
        }
        else
        {
            force = (GameManager.instance.isDarkMode) ? -Mathf.Abs(force) : Mathf.Abs(force);
            Collider[] hits = Physics.OverlapSphere(gameObject.transform.position, BlackHoleMechanic.HitRange);
            foreach (Collider candidate in hits)
            {
                if (candidate.gameObject.tag == GameManager.instance.EnemyTag)
                {
                    Vector3 forceDirection = candidate.transform.position - gameObject.transform.position;
                    candidate.gameObject.GetComponent<Rigidbody>().AddForce(-forceDirection.normalized * force, ForceMode.Force);
                    Enemy enemyMechanics = candidate.gameObject.GetComponent<EnemyManager>().enemy;
                    GameManager.instance.playerCharacter.UseAbility(MECHANICS.ABILITIES.BLACKHOLE, enemyMechanics, this.damageFactor);
                }
            }
        }
    }
}
