using UnityEngine;
using System.Collections;
using Mechanics.Objects;
using RPG.Assets.Scripts.Mechanics.Enumerations;

public class EnemyManager : MonoBehaviour
{
    public Enemy enemy;

    public ParticleSystem BossDeathEffect;
    public AudioClip BossDeathSound;
    public int ModeAmount = 5;
    public GameObject Drop;

    private AudioSource source;
    private ParticleSystem LongRangeEnemyEffects;
    private float timer;
    private bool MarkedForDestroy;

    void Start()
    {
        if (enemy.Type == EnemyType.BOSS)
        {
            source = GetComponent<AudioSource>();
        }
        else if (enemy.Type == EnemyType.LONGRANGE)
        {
            LongRangeEnemyEffects = GetComponent<ParticleSystem>();
        }
        timer = -2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.enemy.IsDead)
        {
            if (enemy.Type == EnemyType.BOSS)
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
                GameManager.instance.AddBalanceAmount(ModeAmount * Random.Range(0.5f, 1.5f));
                SpawnDrop();
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
        GameManager.instance.AddBalanceAmount(ModeAmount * 3);
        SpawnDrop();

    }

    void RangeDeath()
    {
        GameManager.instance.playerCharacter.AwardExp(this.enemy.ExpValue);
        GameManager.instance.AddBalanceAmount(ModeAmount * Random.Range(0.5f, 1.5f));
        LongRangeEnemyEffects.Stop();
        Destroy(gameObject, 1.5f);
        MarkedForDestroy = true;
        SpawnDrop();
    }

    void SpawnDrop()
    {
        if (Random.value <= MECHANICS.TABLES.SPECIALS.DROP_CHANCE)
        {
            DropController dCon = ((GameObject)Instantiate(Drop, transform.position, transform.rotation)).GetComponentInChildren<DropController>();
            dCon.Initialize(enemy.Level);
        }
    }
}
