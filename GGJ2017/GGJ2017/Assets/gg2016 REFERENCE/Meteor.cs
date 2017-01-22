using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour {

    public float speed;
    public Vector3 direction;

	public GGJ2017GameManager.SURFBOARDCOLOR color;
	
	// Use this for initialization
	void Start ()
	{
		GetComponent<SpriteRenderer>().color = GGJ2017GameManager.m_dSurfboardColorToColor[color];
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = transform.position + direction * speed * Time.deltaTime;
	}



	void OnTriggerEnter2D(Collider2D collider)
	{
		CharacterController character = collider.gameObject.GetComponent<CharacterController>();
		if (character != null)
		{
			if (color == character.m_scPlayerColor)
			{
				CTEventManager.FireEvent(new KillSurferEvent() { surfer = character });
				DestroyObject(gameObject);
			}
		}
	}
}
