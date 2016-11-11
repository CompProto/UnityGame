using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    public Slider HealthBar;
    public Slider EnergyBar;

    // Use this for initialization
    void Start ()
    {
        EnergyBar.maxValue = GameManager.instance.playerCharacter.SpellPoints; 
        HealthBar.maxValue = GameManager.instance.playerCharacter.Life;        
        GameManager.instance.playerCharacter.Wounds = GameManager.instance.playerCharacter.Life-1f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        EnergyBar.maxValue = GameManager.instance.playerCharacter.SpellPoints;
        HealthBar.maxValue = GameManager.instance.playerCharacter.Life;
        EnergyBar.value = GameManager.instance.playerCharacter.SpellPoints - GameManager.instance.playerCharacter.ConsumedSpellPoints;
        HealthBar.value = GameManager.instance.playerCharacter.Life - GameManager.instance.playerCharacter.Wounds;
    }
    
}
