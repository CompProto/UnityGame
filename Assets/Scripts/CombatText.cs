using UnityEngine;
using System.Collections;

public class CombatText : MonoBehaviour
{
    public static CombatText instance;
    public GameObject floatingText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //this.transform.parent = null;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void Show(string text, Color color, int defaultFontSize = 24)
    {
        GameObject ftext = (GameObject)Instantiate(floatingText, gameObject.transform.parent, false);
        ftext.GetComponent<FloatingText>().Run(text, color, new Vector3(Screen.width / 2f, Screen.height / 2f, 0f), defaultFontSize);
    }

}
