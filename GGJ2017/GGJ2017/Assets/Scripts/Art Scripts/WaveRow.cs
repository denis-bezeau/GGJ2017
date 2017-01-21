using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveRow : MonoBehaviour
{
	
	private float m_fWaveRowWidth = 0;
	
	// Use this for initialization
	void Start ()
	{

		Align();
	}
	
	// Update is called once per frame
	public void Move(Vector3 m_v3MoveDirection, float m_fMoveSpeed)
	{
		transform.position = transform.position + m_v3MoveDirection * m_fMoveSpeed * Time.deltaTime;
	}

	void Align()
	{
		float fWaveWidth = 0;
		foreach (WaveRoller wave in GetComponentsInChildren<WaveRoller>())
		{
			wave.m_v3DefaultPosition = new Vector3(fWaveWidth, 0,0);
			fWaveWidth += wave.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
		}
		m_fWaveRowWidth = fWaveWidth;
	}

	public float GetWidth()
	{
		return m_fWaveRowWidth;
	}
}
