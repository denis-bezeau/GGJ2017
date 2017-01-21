using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	public static GGJ2017GameManager GetInstance()
	{
		return instance;
	}

	// Use this for initialization
	void Start()
	{
		instance = this;
	}
}
