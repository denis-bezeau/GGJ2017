using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurferUpBG : MonoBehaviour {

	public float m_fRotationSpeed;
	public GGJ2017GameManager.SURFBOARDCOLOR m_eColor;

	public SpriteRenderer BackgroundSprite;

	void Start ()
	{
        List<GGJ2017GameManager.SURFBOARDCOLOR> colors = GGJ2017GameManager.m_dTotalColors;
        m_eColor = colors[Random.Range(0, colors.Count)];
        BackgroundSprite.color = GGJ2017GameManager.m_dSurfboardColorToColor[m_eColor];
        /*
		switch (m_eColor)
		{
			case GGJ2017GameManager.SURFBOARDCOLOR.BLUE: BackgroundSprite.color = Color.blue; break;
			case GGJ2017GameManager.SURFBOARDCOLOR.RED: BackgroundSprite.color = Color.red; break;
			case GGJ2017GameManager.SURFBOARDCOLOR.GREEN: BackgroundSprite.color = Color.green; break;
            default: break;
		}//*/
	}
	
	// Update is called once per frame
	void Update ()
	{
		BackgroundSprite.transform.Rotate(Vector3.forward, m_fRotationSpeed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		CharacterController character = collider.gameObject.GetComponent<CharacterController>();
		if(character != null)
		{
			if (character.m_scPlayerColor == m_eColor)
			{
				CTEventManager.FireEvent(new SurferUpEvent() { color = m_eColor });
				DestroyObject(gameObject);
			}
		}
	}
}
