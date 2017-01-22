using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyTerrain : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider)
	{
		Debug.Log("SurferUp::you triggered with " + collider.gameObject.name);
		CharacterController character = collider.gameObject.GetComponent<CharacterController>();
		if (character != null)
		{
			Debug.Log("DeadlyTerrain::you triggered with character color");
			CTEventManager.FireEvent(new KillSurferEvent() { surfer = character });
			DestroyObject(gameObject);
		}
	}
}
