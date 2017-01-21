using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurferUpBG : MonoBehaviour {

	public float m_fRotationSpeed;
	public GGJ2017GameManager.SURFBOARDCOLOR m_eColor;

	public SpriteRenderer BackgroundSprite;

	void Start ()
	{
		switch (m_eColor)
		{
			case GGJ2017GameManager.SURFBOARDCOLOR.BLUE: BackgroundSprite.color = Color.blue; break;
			case GGJ2017GameManager.SURFBOARDCOLOR.RED: BackgroundSprite.color = Color.red; break;
			case GGJ2017GameManager.SURFBOARDCOLOR.GREEN: BackgroundSprite.color = Color.green; break;
			case GGJ2017GameManager.SURFBOARDCOLOR.YELLOW: BackgroundSprite.color = Color.yellow; break;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		BackgroundSprite.transform.Rotate(Vector3.forward, m_fRotationSpeed * Time.deltaTime);
	}

	void OnCollisionEnter2D(Collision2D collider)
	{
		Debug.Log("SurferUp::you collided with " + collider.gameObject.name);
		CharacterController character = collider.gameObject.GetComponent<CharacterController>();
		if (character != null)
		{
			Debug.Log("SurferUp::you collided with character color");
			CTEventManager.FireEvent(new SurferUpEvent() { color = m_eColor });
			DestroyObject(gameObject);
		}

	}


	void OnTriggerEnter2D(Collider2D collider)
	{
		Debug.Log("SurferUp::you triggered with " + collider.gameObject.name);
		CharacterController character = collider.gameObject.GetComponent<CharacterController>();
		if(character != null)
		{
			Debug.Log("SurferUp::you triggered with character color" );
			CTEventManager.FireEvent(new SurferUpEvent() { color = m_eColor });
			DestroyObject(gameObject);
		}
	}
}
