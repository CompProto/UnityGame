using UnityEngine;
using System.Collections;
using Mechanics.Objects;
using Mechanics.Enumerations;

public class DropController : MonoBehaviour {

    private Vector3 rotation;
    private Item item;

    public void Initialize(int ilvl)
    {
        item = GameManager.instance.itemGen.MakeRandom(ilvl, GameManager.instance.playerCharacter[Stats.FORTUITY]);
    }

	// Use this for initialization
	void Start () {
        rotation = new Vector3(45,0, 45);

    }
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.Rotate(rotation * Time.deltaTime);
	}

    
    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            GameManager.instance.playerCharacter.EquipRune(item);
            transform.parent.gameObject.GetComponentInChildren<ParticleSystem>().Stop();
            Destroy(transform.parent.gameObject, 0.5f);
            Destroy(gameObject);
        }
    }
}
