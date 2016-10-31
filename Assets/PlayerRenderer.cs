using UnityEngine;
using System.Collections;

public class PlayerRenderer : MonoBehaviour {

    private Renderer rend;
    public Material white, dark;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
    }
	
    public void ChangePlayerShader(bool darkMode)
    {
        if (darkMode)
            rend.material = dark;
        else
            rend.material = white;
    }
}
