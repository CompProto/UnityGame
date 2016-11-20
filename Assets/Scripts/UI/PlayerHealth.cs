using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{

    public Slider HealthBar;
    public Slider EnergyBar;
    public Slider ExpBar;
    public Text ExpText, LifeText, EnergyText;

    public GameObject LevelUpEffects;
    public GameObject GodModeText;

    private int level;
    private LevelUpController levelController;

    private bool GodModeEnabled;

    // Use this for initialization
    void Start()
    {
        EnergyBar.maxValue = GameManager.instance.playerCharacter.SpellPoints;
        HealthBar.maxValue = GameManager.instance.playerCharacter.Life;
        ExpBar.maxValue = GameManager.instance.playerCharacter.ExpNextLevel(1);
        ExpBar.minValue = GameManager.instance.playerCharacter.ExpNextLevel(0);
        level = GameManager.instance.playerCharacter.Level;
        levelController = LevelUpEffects.GetComponent<LevelUpController>();
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
            level = GameManager.instance.playerCharacter.Level;
            // TODO - play leveling animation & sound
            levelController.PlayLevelUpEffects();
            ExpBar.maxValue = GameManager.instance.playerCharacter.ExpNextLevel(1);
            ExpBar.minValue = GameManager.instance.playerCharacter.ExpNextLevel(0);
        }
        ExpBar.value = GameManager.instance.playerCharacter.CurrentExp;

        // Update texts
        ExpText.text = (GameManager.instance.playerCharacter.CurrentExp - ExpBar.minValue) + " / " + (ExpBar.maxValue - ExpBar.minValue);
        LifeText.text = (int) HealthBar.value + " / " + (int) HealthBar.maxValue;
        EnergyText.text = (int) EnergyBar.value + " / " + (int) EnergyBar.maxValue;

        Cheater();
    }

    private void Cheater()
    {
        if (Input.GetKey(KeyCode.H) && Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.T)) // Enables GODMODE. CHEATER!
            GodModeEnabled = true;
        if (Input.GetKeyDown(KeyCode.Escape))
            GodModeEnabled = false;
        GodModeText.SetActive(GodModeEnabled);
        if (GodModeEnabled)
        {
            GameManager.instance.playerCharacter.ConsumedSpellPoints = 0;
            GameManager.instance.playerCharacter.Wounds = 0;
        }
    }

}
