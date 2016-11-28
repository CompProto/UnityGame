using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class NarrativeBox : MonoBehaviour {

    private bool visibility = false;
    public GameObject narrativeBox;

    public static NarrativeBox instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        this.narrativeBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            this.ShowNarrative();
            // Pause / Unpause the game
            if (endLevel)
            {
                endLevel = false;
                GameManager.instance.ChangeScene();
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Escape) && visibility)
        {
            ShowNarrative(); // Close char screen, show pause menu
        }
    }

    public void ShowNarrative()
    {
        this.visibility = !this.visibility;
        this.narrativeBox.SetActive(this.visibility);
        //Debug.Log("Visibility: "+ this.visibility);
        //Debug.Log("Active: " + this.narrativeBox.activeSelf);
        if (!GameManager.instance.isPaused)
        {
            if (visibility)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }
    }

    private bool endLevel = false;

    public void setText(int level)
    {
        Debug.Log("setText LEVEL: "+level);
        if (level==1)
        {
            narrativeBox.GetComponentInChildren<Text>().text = level1();
            Invoke("ShowNarrative", 1.3f);
        }
        if (level == 12)
        {
            endLevel = true;
            narrativeBox.GetComponentInChildren<Text>().text = level12();
            //Invoke("ShowNarrative", 1.3f);
            ShowNarrative();
        }
        if (level == 2)
        {
            narrativeBox.GetComponentInChildren<Text>().text = level2();
            Invoke("ShowNarrative", 1.3f);
        }
        if (level == 22)
        {
            endLevel = true;
            narrativeBox.GetComponentInChildren<Text>().text = level22();
            Invoke("ShowNarrative", 1.3f);
        }
        if (level == 3)
        {
            narrativeBox.GetComponentInChildren<Text>().text = level3();
            Invoke("ShowNarrative", 1.3f);
        }
        if (level == 32)
        {
            endLevel = true;
            narrativeBox.GetComponentInChildren<Text>().text = level32();
            Invoke("ShowNarrative", 1.3f);
        }
    }

    private string level1()
    {
        string normal1 = "First Act – A Troubling Task" + Environment.NewLine + Environment.NewLine +
                         "As I entered the high council room, it is clear something is horrible wrong. Dark energy fills the usually bright and peaceful room." +
                         " There are marks on the floor and the walls. Razael, the eldest of the high council speaks;" + Environment.NewLine + Environment.NewLine;

        string normal2 = "I accepted. What else could I do? I wanted a place in The Icarion…" + Environment.NewLine + Environment.NewLine +
                         "Press 'g' to go on your adventure.";


        string italic1 = "“Traveler, your master Malcar has deceived us." + Environment.NewLine + Environment.NewLine +
                         "He has been corrupted and seeks to overturn the balance. We cannot let him do this." +
                         " We have tried to capture him, but he has escaped our grasp." + Environment.NewLine + Environment.NewLine +
                         "You, as his apprentice, knows his way of doing.We ask of you to take his place in the Icarion as a Plane Walker.But first, you must hunt Malcar down." +
                         " Destroy him, or bring him to us! We will imbue you with the powers to do so." + Environment.NewLine + Environment.NewLine +
                         "Do you accept this troubling task?“" + Environment.NewLine + Environment.NewLine;

        string italic2 = string.Format(italic1, FontStyle.Italic);

        return string.Concat(normal1,italic2,normal2);               
    }

    private string level12()
    {
        return "End of the First Act" + Environment.NewLine + Environment.NewLine + 
               "Hello traveler, are you the one sent from the Icarion?" + Environment.NewLine + Environment.NewLine +
               "Malcar roams on the plane beyond this, but be careful, every action you take from now on will tip the balance of light and dark." + Environment.NewLine + Environment.NewLine +
               "If you do not keep this balance, you will find it impossible to continue the fight and the world will be destroyed." + Environment.NewLine + Environment.NewLine +
               "Press 'g' to continue on your adventure.";
    }

    private string level2()
    {
        string normal1 = "Second Act – The Hunt" + Environment.NewLine + Environment.NewLine +
                         "It feels like Chaos is fighting within me. I am fighting to both keep the balance, and find Malcar. I see his steps. His past actions." +
                         " I just cannot shake this feeling. This feeling that something is... not right with the balance despite my efforts. It must be Malcars work." + Environment.NewLine + Environment.NewLine +
                         "He moves faster and more determined than I imagined. I must push myself to the very limit of my powers." + 
                         " He must be stopped… " + Environment.NewLine +
                         "Luckily, Malcar has trained me well." + Environment.NewLine + Environment.NewLine +
                         "Press 'g' to go on your adventure.";

        return normal1;
    }

    private string level22()
    {
        return "End of the Second Act" + Environment.NewLine + Environment.NewLine + 
               "Wait!" + Environment.NewLine + Environment.NewLine +
               "It is you, my dear apprentice!" + Environment.NewLine + Environment.NewLine +
               "I can’t believe they would be so vile as to turn you against me and send you to kill me..." + Environment.NewLine + 
               "Travel with me to the final plane and on the way I will present you with evidence of what is really going on!" + Environment.NewLine + Environment.NewLine +
               "Press 'g' to continue on your adventure.";
    }

    private string level3()
    {
        string normal1 = "Third Act – The High Council" + Environment.NewLine + Environment.NewLine +
                         "At first I thought his story was nothing more than a plea for mercy. A way for him to get inside my head and corrupt me." + 
                         " A part of me still hopes this is the case, as the evidence Malcar has presented for me is worrying. He claims the high council has abandoned our true goal." + 
                         " They have them self been corrupted. Corrupted by the very powers they imbued me with. Will our lust for power be our, and the entire worlds demise?" + 
                         " Not if it is up to me and Malcar." + Environment.NewLine + Environment.NewLine +
                         "We must confront the high council together, to end this, and save the worlds existence." + Environment.NewLine + Environment.NewLine +
                         "Press 'g' to go on your adventure.";

        return normal1;
    }

    private string level32()
    {
        return "End of the Game - Epilogue" + Environment.NewLine + Environment.NewLine +
               "As the last of the high council elders fell to his death, Malcar looked at me. " + 
               "It briefly felt like his eyes gleamed as was his entire body only a shell filled with pure power. " + 
               "Malcar walked to the center throne, sat down and spoke;" + Environment.NewLine + Environment.NewLine +
               "“Traveler, you have done well. You deserve a place in the new high council, at my side. " + 
               "Together, we shall lead the Icarion and protect the balance.”" + Environment.NewLine + Environment.NewLine +
               "I accepted. What else could I do? I had just slaughtered the high council, the Icarion needed someone to follow to keep the balance. " + 
               "And I felt more powerful by his side." + Environment.NewLine + Environment.NewLine +
               "As I write this for archiving in our library, I have a weird feeling. It feels like I missed something. " + 
               "I have trouble recalling some of my doings. I also seem to have forgotten what exactly the evidence Malcar showed me was... " + 
               "It must have been compelling, why else would I have slaughtered the high council together with him?" + Environment.NewLine + Environment.NewLine +
               "Press 'g' to restart your adventure.";
    }
}
