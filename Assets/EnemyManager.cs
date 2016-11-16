using UnityEngine;
using System.Collections;
using Mechanics.Objects;
using RPG.Assets.Scripts.Mechanics.Enumerations;

public class EnemyManager : MonoBehaviour
{
    public Enemy enemy;
    // Use this for initialization

    void Start ()
    {
        this.enemy = new Enemy(EnemyType.BOSS, new Interval(1f,5f));
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(this.enemy.IsDead)
        {
            GameManager.instance.playerCharacter.AwardExp(this.enemy.ExpValue);
            Destroy(gameObject);
        }
	}
}
