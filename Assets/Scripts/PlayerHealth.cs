using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    public Slider HealthBar;
    public Slider EnergyBar;

    private bool isDead;

	// Use this for initialization
	void Start () {
        EnergyBar.value = 100.0f; // TODO - set to max energy
     //   HealthBar.value = 100.0f; // TODO - set to max health
	}
	
	// Update is called once per frame
	void Update () {

        // TODO - remove!
        if (Random.Range(0.0f, 1.0f) > 0.5f)
            TakeDamage(0.5f);
        else
            Heal(0.5f);

    }

    void FixedUpdate()
    {
        Heal(5.0f * Time.fixedDeltaTime); // TODO, how much health regen pr second?
        GainEnergy(5.0f * Time.fixedDeltaTime); // TODO, how much energy regen pr second?
    }

    public bool SpendEnergy(float amount)
    {
        if(EnergyBar.value >= amount)
        {
            EnergyBar.value -= amount;
            return true;
        }
        return false;
    } 

    public void GainEnergy(float amount)
    {
        EnergyBar.value += amount;
    }

    public void Heal(float amount)
    {
        HealthBar.value += amount;
    }

    public void TakeDamage(float amount)
    {
        HealthBar.value -= amount;
        if(HealthBar.value <= 0 && !isDead)
        {
            Death();
        }
    }

    private void Death()
    {
        isDead = true;
        // TODO, stop player movement etc
    }

    
}
