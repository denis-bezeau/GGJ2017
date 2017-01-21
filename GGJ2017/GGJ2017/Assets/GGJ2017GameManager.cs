using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurferUpEvent : CTEvent
{
	public GGJ2017GameManager.SURFBOARDCOLOR color;
}

public class SpawnNewSurferEvent : CTEvent
{
	public GGJ2017GameManager.SURFBOARDCOLOR color;
}

public class GGJ2017GameManager : MonoBehaviour 
{
	public enum SURFBOARDCOLOR
	{
		BLUE,
		RED,
		GREEN,
		YELLOW
	}

	static GGJ2017GameManager instance;

	private Dictionary<SURFBOARDCOLOR, float> m_dPowerLevels = new Dictionary<SURFBOARDCOLOR, float>();

	public float m_fGlobalScrollSpeed;

	public static GGJ2017GameManager GetInstance()
	{
		return instance;
	}

	// Use this for initialization
	void Start()
	{
		instance = this;
	}

	public void Awake()
	{
		CTEventManager.AddListener<SurferUpEvent>(OnSurferUp);
	}

	public void OnDestroy()
	{
		CTEventManager.RemoveListener<SurferUpEvent>(OnSurferUp);
	}

	public float GetGlobalScrollSpeed()
	{
		return m_fGlobalScrollSpeed;
	}

	public void OnSurferUp(SurferUpEvent eventData)
	{
		switch (eventData.color)
		{
			case SURFBOARDCOLOR.BLUE: Debug.Log("spawn BLUE surger"); break;
			case SURFBOARDCOLOR.GREEN: Debug.Log("spawn GREEN surger"); break;
			case SURFBOARDCOLOR.RED: Debug.Log("spawn RED surger"); break;
			case SURFBOARDCOLOR.YELLOW: Debug.Log("spawn YELLOW surger"); break;
		}

		CTEventManager.FireEvent(new SpawnNewSurferEvent() { color = eventData.color });

	}
}
