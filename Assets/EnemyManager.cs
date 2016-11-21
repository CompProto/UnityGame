using UnityEngine;
using System.Collections;
using Mechanics.Objects;
using RPG.Assets.Scripts.Mechanics.Enumerations;

public class EnemyManager : MonoBehaviour
{
    public Enemy enemy;

    public ParticleSystem BossDeathEffect;
    public AudioClip BossDeathSound;

    private AudioSource source;
    private ParticleSystem LongRangeEnemyEffects;
    private float timer;
    private bool MarkedForDestroy;

    void Start()
    {
        if (enemy.Type == EnemyType.BOSS)
        {
            source = GetComponent<AudioSource>();
        } else if(enemy.Type == EnemyType.LONGRANGE)
        {
            LongRangeEnemyEffects = GetComponent<ParticleSystem>();
        }
        timer = -2.5f;
    }

	// Update is called once per frame
	void Update ()
    {
	    if(this.enemy.IsDead)
        {         
            if(enemy.Type == EnemyType.BOSS)
            {
                if (!MarkedForDestroy)
                    BossDeath();
                else
                    timer += Time.deltaTime;
                if (timer >= 0)
                    BossDeathEffect.Stop();
            }
            else if (enemy.Type == EnemyType.LONGRANGE)
            {
                if (!MarkedForDestroy)
                    RangeDeath();
            }
            else
            {
                GameManager.instance.playerCharacter.AwardExp(this.enemy.ExpValue);
                Destroy(gameObject);
            }
        }
	}

    void BossDeath()
    {
        if (source == null)
        {
            source = GetComponent<AudioSource>();
        }
        source.Stop();
        source.clip = BossDeathSound;
        source.Play();
        BossDeathEffect.Play();
        Destroy(gameObject, 4.0f);
        MarkedForDestroy = true;
        GameManager.instance.playerCharacter.AwardExp(this.enemy.ExpValue);
    }

    void RangeDeath()
    {
        GameManager.instance.playerCharacter.AwardExp(this.enemy.ExpValue);
        LongRangeEnemyEffects.Stop();
        Destroy(gameObject, 1.5f);
        MarkedForDestroy = true;
    }
}
