using UnityEngine;
using System.Collections;
using Mechanics.Objects;
using Mechanics.Objects.Abilities;

public class EnemyRangedAttack : MonoBehaviour
{
    public GameObject Impact;
    public AudioClip ImpactSound;
    private Vector3 tar;
    private ParticleSystem particles;
    private AudioSource source;
    private bool ImpactSpawned = false;
    private EnemyManager enemyManager;
    // Use this for initialization
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        //    source.clip = ImpactSound;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < tar.y && !ImpactSpawned)
        {
            ImpactSpawned = true;
            // Start & spawn Nova
          //  source.Play();
            GameObject _impact = (GameObject)Instantiate(Impact, tar + new Vector3(0, 0.5f, 0), transform.rotation * Quaternion.Euler(90, 0, 0));

            Collider[] hits = Physics.OverlapSphere(gameObject.transform.position, 2);
            foreach (Collider candidate in hits)
            {
                if (candidate.gameObject.tag == "Player")
                {
                    enemyManager.enemy.UseAbility(MECHANICS.ABILITIES.ENEMY_RANGED_ATTACK, GameManager.instance.playerCharacter, 1f); // DU SKAL KALDE DENNE HER
                }
            }
            Destroy(gameObject, 2.5f);
        }
    }

    public void ThrowBomb(Vector3 target, EnemyManager enemyManager)
    {
        this.enemyManager = enemyManager;
        tar = target;
        particles = gameObject.GetComponent<ParticleSystem>();
        particles.Play();
        float dist = (target - transform.position).magnitude;
        float airtime = (12.5f / 100.0f) * dist; // Projectile travel time
        Vector3 ThrowSpeed = calculateBestThrowSpeed(transform.position, target, airtime);
        gameObject.GetComponent<Rigidbody>().AddForce(ThrowSpeed, ForceMode.VelocityChange);        
        enemyManager.enemy.UseAbility(MECHANICS.ABILITIES.ENEMY_RANGED_ATTACK, GameManager.instance.playerCharacter, 1f);
    }

    private Vector3 calculateBestThrowSpeed(Vector3 origin, Vector3 target, float timeToTarget)
    {
        // calculate vectors
        Vector3 toTarget = target - origin;
        Vector3 toTargetXZ = toTarget;
        toTargetXZ.y = 0;

        // calculate xz and y
        float y = toTarget.y;
        float xz = toTargetXZ.magnitude;

        // calculate starting speeds for xz and y. Physics forumulase deltaX = v0 * t + 1/2 * a * t * t
        // where a is "-gravity" but only on the y plane, and a is 0 in xz plane.
        // so xz = v0xz * t => v0xz = xz / t
        // and y = v0y * t - 1/2 * gravity * t * t => v0y * t = y + 1/2 * gravity * t * t => v0y = y / t + 1/2 * gravity * t
        float t = timeToTarget;
        float v0y = y / t + 0.5f * Physics.gravity.magnitude * t;
        float v0xz = xz / t;

        // create result vector for calculated starting speeds
        Vector3 result = toTargetXZ.normalized;        // get direction of xz but with magnitude 1
        result *= v0xz;                                // set magnitude of xz to v0xz (starting speed in xz plane)
        result.y = v0y;                                // set y to v0y (starting speed of y plane)

        return result;
    }
}
