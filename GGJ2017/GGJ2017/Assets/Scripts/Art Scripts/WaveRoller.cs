using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveRoller : MonoBehaviour
{


	public float m_fAngle = 0.0f;
	public float m_fSpeed = 1.0f;
	public float m_fRadius = 10.0f;
	public Vector3 m_v3DefaultPosition;

	// Update is called once per frame
	void Update() {
		m_fAngle += Time.deltaTime * m_fSpeed;

		transform.localPosition = m_v3DefaultPosition + new Vector3(Mathf.Cos(m_fAngle*m_fRadius), Mathf.Sin(m_fAngle*m_fRadius), 0);
	}

}
