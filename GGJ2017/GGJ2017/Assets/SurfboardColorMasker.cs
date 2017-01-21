using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurfboardColorMasker : MonoBehaviour
{
	public GGJ2017GameManager.SURFBOARDCOLOR eColor = GGJ2017GameManager.SURFBOARDCOLOR.BLUE;

	private float m_fCurrentHeight = 0.0f;
	
	private float m_fTimer = 0.0f;
	public float m_fAnimateTime = 1.0f;
	public float m_fStartingFill = 0.0f;
	private bool m_bLerping = false;
	private float m_fWidth;
	private float m_fMaxHeight;
	private float m_fDestinationHeight = 0.0f;

	private RectTransform m_oCanvasMask;

	public Image m_oImage;
	public Color m_oColor;

	void Start()
	{
		m_oImage.color = m_oColor;
		m_oCanvasMask = GetComponent<RectTransform>();
		m_fMaxHeight = m_oCanvasMask.rect.height;
		m_fWidth = m_oCanvasMask.rect.width;
		SetHeight(m_fStartingFill);
	}

	public void SetHeight(float fHeight)
	{
		if (m_fCurrentHeight != m_fMaxHeight*fHeight)
		{
			m_fDestinationHeight = m_fMaxHeight * fHeight;
			m_fTimer = 0.0f;
			m_bLerping = true;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(m_bLerping)
		{
			m_fTimer += Time.deltaTime;
			m_oCanvasMask.sizeDelta = new Vector2(m_fWidth, Mathf.Lerp(m_fCurrentHeight, m_fDestinationHeight, m_fTimer));
			m_fCurrentHeight = m_oCanvasMask.rect.height;
			if (m_fTimer > m_fAnimateTime)
			{
				m_bLerping = false;
				m_fCurrentHeight = m_fDestinationHeight;
			}
		}

		if(Input.GetKeyDown(KeyCode.Space))
		{
			CTEventManager.FireEvent(new SetPowerEvent() { color = eColor, value = Random.Range(0.0f, 1.0f) });
		}
	}
}
