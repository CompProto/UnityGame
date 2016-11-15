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
    private Player player;


    void Start()
    {
        this.characterSheet.SetActive(false);
        this.mouseHover.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            this.Show();
        }
        string[] statNames = Enum.GetNames(typeof(Stats));
        foreach (Text c in characterSheet.GetComponentsInChildren<Text>())
        {
            if ( this.IsWithin(c))
            {
                this.mouseHover.SetActive(true);
            }
        }
        
    }

    // Update is called once per frame
    public void Show()
    {
        this.visibility = !this.visibility;
        this.characterSheet.SetActive(this.visibility);
        this.player = GameManager.instance.playerCharacter;
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
    }

    private bool IsWithin(Text text)
    {
        Rect r = text.GetComponent<RectTransform>().rect;
        return r.Contains(Input.mousePosition);
    }


}
