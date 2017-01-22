using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPowerEvent : CTEvent
{
	public GGJ2017GameManager.SURFBOARDCOLOR m_scColor;
    public Color m_cColor;
    public float value;
}

public class SurfboardUIManager : MonoBehaviour
{
    private static int MAX_VALUE = 1;
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
			if (sbcm.eColor == eventData.m_scColor)
			{
				sbcm.SetHeight(eventData.value);
			}

		}
	}

    public void UpdateColor()
    {
        foreach (SurfboardColorMasker sbcm in m_dSurfboardDictionary)
        {
            if (sbcm.m_fCurrentHeight < MAX_VALUE)
            {

            }
            else
            {

            }
        }
    }
}
