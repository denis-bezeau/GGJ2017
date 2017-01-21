using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPowerEvent : CTEvent
{
	public SurfboardColorMasker.SURFBOARDCOLOR color;
	public float value;
}

public class SurfboardUIManager : MonoBehaviour
{
	private SurfboardColorMasker[] m_dSurfboardDictionary;

	public void Awake()
	{
		m_dSurfboardDictionary = GetComponentsInChildren<SurfboardColorMasker>();
		CTEventManager.AddListener<SetPowerEvent>(SetPower);
	}

	public void OnDestroy()
	{
		CTEventManager.RemoveListener<SetPowerEvent>(SetPower);
	}


	public void SetPower(SetPowerEvent eventData)
	{
		foreach (SurfboardColorMasker sbcm in m_dSurfboardDictionary)
		{
			if (sbcm.eColor == eventData.color)
			{
				sbcm.SetHeight(eventData.value);
			}
		}
	}
}
