using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    private Text textbox;
    private float end;
    private float speed;
    private bool running;
    private float variability;

    public void Run(string text, Color color, Vector3 position)
    {
        this.speed = 7f;
        this.variability = 50f;
        this.end = Time.time + 3.0f;
        this.running = true;
        this.textbox = this.GetComponent<Text>();
        this.textbox.text = text;
        this.textbox.color = color;
        float vari1 = (Mechanics.Objects.Utility.GetVariation((int)this.variability) - (this.variability / 200f));
        float vari2 = (Mechanics.Objects.Utility.GetVariation((int)this.variability) - (this.variability / 200f));
        this.textbox.transform.position = new Vector3(position.x * vari1, position.y * vari2, 0f);
    }

    void Update()
    {
        if (!this.running) return;
        if (Time.time < this.end)
        {
            this.textbox.transform.Translate(Vector3.up * this.speed);
        }
        else
        {
            Destroy(this);
        }
    }
}
