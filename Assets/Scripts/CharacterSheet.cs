using UnityEngine;
using System.Collections;
using Mechanics.Objects;
using UnityEngine.UI;
using Mechanics.Enumerations;
using System;
using UnityEngine.EventSystems;

public class CharacterSheet : MonoBehaviour
{
    private bool visibility = false;
    public GameObject characterSheet;
    public GameObject mouseHover;
    public GameObject incPower;
    public GameObject incEssence;
    public GameObject incPerception;
    public GameObject incLuck;
    public Text level;
    private Player player;


    void Start()
    {
        this.characterSheet.SetActive(false);
        this.mouseHover.SetActive(false);
        this.incPower.SetActive(false);
        this.incEssence.SetActive(false);
        this.incPerception.SetActive(false);
        this.incLuck.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            this.Show();
        }
    }

    // Update is called once per frame
    public void Show()
    {
        this.visibility = !this.visibility;
        this.characterSheet.SetActive(this.visibility);
        this.player = GameManager.instance.playerCharacter;
        this.UpdateButtons();
        this.UpdateStats();
    }

    private void UpdateStats()
    {
        string[] statNames = Enum.GetNames(typeof(Stats));
        foreach (Text c in characterSheet.GetComponentsInChildren<Text>())
        {
            bool hasStat = false;
            foreach (string statname in statNames) { hasStat |= statname == c.name; }
            if (hasStat)
            {
                Stats stat = (Stats)Enum.Parse(typeof(Stats), c.name);
                c.text = c.tag == "RawStat" ? this.player.GetStat(stat).Value.ToString() : this.player[stat].ToString();
            }
        }
        this.level.text = this.player.Level.ToString();
    }

    private void UpdateButtons()
    {
        bool show = this.player.AvailableStatPoints > 0;
        this.incPower.SetActive(show);
        this.incEssence.SetActive(show);
        this.incPerception.SetActive(show);
        this.incLuck.SetActive(show);
    }

    public void IncreaseStat(string statName)
    {
        Stats stat = (Stats)Enum.Parse(typeof(Stats), statName);
        this.player.AwardStat(stat, 1f);
        this.UpdateButtons();
        this.UpdateStats();
    }
}
