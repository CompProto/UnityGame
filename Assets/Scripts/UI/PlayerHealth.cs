using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{

    public Slider HealthBar;
    public Slider EnergyBar;
    public Slider ExpBar;

    private int level;

    // Use this for initialization
    void Start()
    {
        EnergyBar.maxValue = GameManager.instance.playerCharacter.SpellPoints;
        HealthBar.maxValue = GameManager.instance.playerCharacter.Life;
        ExpBar.maxValue = GameManager.instance.playerCharacter.ExpNextLevel(0);
        ExpBar.minValue = GameManager.instance.playerCharacter.ExpNextLevel(-1);
        level = GameManager.instance.playerCharacter.Level;
    }

    // Update is called once per frame
    void Update()
    {
        EnergyBar.maxValue = GameManager.instance.playerCharacter.SpellPoints;
        HealthBar.maxValue = GameManager.instance.playerCharacter.Life;
        EnergyBar.value = GameManager.instance.playerCharacter.SpellPoints - GameManager.instance.playerCharacter.ConsumedSpellPoints;
        HealthBar.value = GameManager.instance.playerCharacter.Life - GameManager.instance.playerCharacter.Wounds;
        if (level != GameManager.instance.playerCharacter.Level)
        {
            ExpBar.maxValue = GameManager.instance.playerCharacter.ExpNextLevel(0);
            ExpBar.minValue = GameManager.instance.playerCharacter.ExpNextLevel(-1);
        }
        ExpBar.value = GameManager.instance.playerCharacter.CurrentExp;
    }

}
