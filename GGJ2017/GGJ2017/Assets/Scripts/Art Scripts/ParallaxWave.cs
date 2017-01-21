using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxWave : MonoBehaviour
{
	public List<WaveRow> waveRows = new List<WaveRow>();
	public int ScreenMinX = -500;
	public Vector3 m_v3MoveDirection = Vector3.left;
	public float m_fMoveSpeed = 1.0f;
	public SortingLayer m_iSortingLayer;
	public int m_iSortOrder = 0;
	public float m_fStartingAngle = 0.0f;
	public float m_fRotationSpeed = 1.0f;
	public float m_fRotationRadius = 10.0f;
	public Color m_cSpriteTint;

	// Use this for initialization
	void Start () {
		foreach (SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>())
		{
			sprite.sortingOrder = m_iSortingLayer.id;
			sprite.sortingOrder = m_iSortOrder;
			sprite.color = m_cSpriteTint;
		}

		foreach (WaveRoller wave in GetComponentsInChildren<WaveRoller>())
		{
			wave.m_fRadius = m_fRotationRadius;
			wave.m_fSpeed = m_fRotationSpeed;
			wave.m_fAngle = m_fStartingAngle;
		}

	}
	
	// Update is called once per frame
	void Update () {

		foreach (WaveRow waveRow in waveRows)
		{
			waveRow.Move(m_v3MoveDirection, m_fMoveSpeed);
		}
		CheckForOffScreen();
	}

	void Align()
	{
		float fWaveWidth = 0;
		foreach (WaveRow waveRow in waveRows)
		{
			waveRow.transform.localPosition = new Vector3(fWaveWidth, 0, 0);
			fWaveWidth += waveRow.GetWidth() - 0.1f;//magic number because bunching them a little bit is better than having a gap
		}
	}

	void CheckForOffScreen()
	{
		foreach (WaveRow waveRow in waveRows)
		{
			if (waveRow.transform.localPosition.x + waveRow.GetWidth() < ScreenMinX)
			{
				float fFarRight = 0;
				foreach (WaveRow variablename in waveRows)
				{
					if (variablename.transform.localPosition.x + variablename.GetWidth() > fFarRight)
					{
						fFarRight = variablename.transform.localPosition.x + variablename.GetWidth();
					}
				}
				waveRow.transform.localPosition = new Vector3(fFarRight, waveRow.transform.localPosition.y, waveRow.transform.localPosition.z);
			}
		}
	}
}
