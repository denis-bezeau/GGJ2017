using UnityEngine;
using System.Collections;

public class BoatStateAnimator : MonoBehaviour 
{
    private Animator animator;

	public SpriteRenderer BoatColor;

	public GGJ2017GameManager.SURFBOARDCOLOR m_eColor;

	public bool m_bIsDead;
    
    void Start ()
    {
	    animator = GetComponent<Animator>();
		animator.applyRootMotion = true;

		switch (m_eColor)
		{
			case GGJ2017GameManager.SURFBOARDCOLOR.RED: BoatColor.color = Color.red; break;
			case GGJ2017GameManager.SURFBOARDCOLOR.GREEN: BoatColor.color = Color.green; break;
			case GGJ2017GameManager.SURFBOARDCOLOR.BLUE: BoatColor.color = Color.blue; break;
		}
    }

	public void Update()
	{
		if (m_bIsDead)
		{
			gameObject.transform.Rotate(Vector3.forward, 13);
			gameObject.transform.localPosition = gameObject.transform.localPosition + Vector3.up * 13 * Time.deltaTime;
		}
	}

	void Kill()
	{
		m_bIsDead = true;
	}


	void OnTriggerEnter2D(Collider2D collider)
	{
		Debug.Log("boat collision 1");
		CharacterController character = collider.gameObject.GetComponent<CharacterController>();
		if (character != null)
		{
			Debug.Log("boat collision 2");
			if (character.m_scPlayerColor == m_eColor && character.IsWaving())
			{
				Debug.Log("boat collision 3");
				Kill();
			}
		}
	}
	
}
