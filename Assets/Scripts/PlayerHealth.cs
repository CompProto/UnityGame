using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    public Slider HealthBar;
    public Slider EnergyBar;
    public float HealthRegen = 5.0f;
    public float EnergyRegen = 5.0f;

    private bool isDead;
    private ThirdPersonUserControl UserControl;

    // Use this for initialization
    void Start ()
    {
        EnergyBar.maxValue = GameManager.instance.playerCharacter.SpellPoints; 
        HealthBar.maxValue = GameManager.instance.playerCharacter.Life;        
        UserControl = gameObject.GetComponent<ThirdPersonUserControl>();
        GameManager.instance.playerCharacter.ConsumedSpellPoints = GameManager.instance.playerCharacter.SpellPoints;
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
