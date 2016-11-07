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
    void Start () {
        EnergyBar.value = 100.0f; // TODO - set to max energy
                                  //   HealthBar.value = 100.0f; // TODO - set to max health
        UserControl = gameObject.GetComponent<ThirdPersonUserControl>();
	}
	
	// Update is called once per frame
	void Update () {
        // TODO - remove!
        if (isDead)
            return;
        if (Random.Range(0.0f, 1.0f) > 0.5f)
            TakeDamage(0.5f);
        else
            Heal(0.5f);

    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            Heal(HealthRegen * Time.fixedDeltaTime); // TODO, how much health regen pr second?
            GainEnergy(EnergyRegen * Time.fixedDeltaTime); // TODO, how much energy regen pr second?
        }
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
        GameManager.instance.isDead = true; // mark player as dead
        // TODO, stop player movement etc
    }

    
}
