using UnityEngine;
using System.Collections;
using Mechanics.Objects;
using UnityEngine.UI;
using Mechanics.Enumerations;
using System;


public class CharacterSheet : MonoBehaviour
{
    private bool visibility = false;
    public GameObject characterSheet;
    private Player player;


    void Start()
    {
        this.characterSheet.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
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
        string[] statNames = Enum.GetNames(typeof(Stats));
        string[] summaryStats = new string[] { "LIFE", "SPELLPOINTS" };
        foreach (Text c in characterSheet.GetComponentsInChildren<Text>())
        {
            bool hasStat = false;
            foreach(string statname in statNames) { hasStat |= statname == c.name; }
            if(hasStat)
            {
                Stats stat = (Stats)Enum.Parse(typeof(Stats), c.name);
                c.text = this.player.GetStat(stat).Value.ToString();
            }

            //hasStat = false;
            //foreach (string summary in summaryStats) { hasStat |= summary == c.name; }
            //if (hasStat)
            //{
            //    Stats stat = (Stats)Enum.Parse(typeof(Stats), c.name);
            //    c.text = this.player[stat].ToString();
            //}
        }
    }


}
