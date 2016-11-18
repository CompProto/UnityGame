using UnityEngine;
using System.Collections;
using Mechanics.Objects;
using RPG.Assets.Scripts.Mechanics.Enumerations;

public class EnemyManager : MonoBehaviour
{
    public Enemy enemy;
   
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
