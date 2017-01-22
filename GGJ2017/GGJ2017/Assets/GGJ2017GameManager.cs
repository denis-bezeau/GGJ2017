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

public class JumpEvent : CTEvent
{
	public GGJ2017GameManager.SURFBOARDCOLOR color;
}

public class KillSurferEvent : CTEvent
{
	public CharacterController surfer;
}

public class ResetEvent : CTEvent
{
}

public class UpdateSurfboardScoreEvent: CTEvent
{
    public Dictionary<GameObject, GGJ2017GameManager.SURFBOARDCOLOR> color;
    public bool addScore;
    public int scoreDelta;
}

public class GGJ2017GameManager : MonoBehaviour 
{
	public enum SURFBOARDCOLOR
	{
		BLUE,
		RED,
		GREEN
	}

    static GGJ2017GameManager instance;

    public static List<Color> m_lPossibleColors = new List<Color>();
    public static List<SURFBOARDCOLOR> m_dTotalColors = new List<SURFBOARDCOLOR>();
    private Dictionary<SURFBOARDCOLOR, float> m_dPowerLevels = new Dictionary<SURFBOARDCOLOR, float>();
    private Dictionary<SURFBOARDCOLOR, float> m_dTotalPowerLevels = new Dictionary<SURFBOARDCOLOR, float>();
    public static Dictionary<SURFBOARDCOLOR, Color> m_dSurfboardColorToColor = new Dictionary<SURFBOARDCOLOR, Color>();

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
        m_dSurfboardColorToColor.Add(SURFBOARDCOLOR.BLUE, Color.blue);
        m_dSurfboardColorToColor.Add(SURFBOARDCOLOR.RED, Color.red);
        m_dSurfboardColorToColor.Add(SURFBOARDCOLOR.GREEN, Color.green);

        m_dTotalColors.Add(SURFBOARDCOLOR.BLUE);
        m_dTotalColors.Add(SURFBOARDCOLOR.RED);
        m_dTotalColors.Add(SURFBOARDCOLOR.GREEN);

        CreateColorList();
        ResetPowerLevels();

        CTEventManager.AddListener<SurferUpEvent>(OnSurferUp);
        CTEventManager.AddListener<UpdateSurfboardScoreEvent>(OnSurferScore);
        CTEventManager.AddListener<ResetEvent>(ResetPowerLevels);
    }

	public void OnDestroy()
	{
		CTEventManager.RemoveListener<SurferUpEvent>(OnSurferUp);
        CTEventManager.RemoveListener<UpdateSurfboardScoreEvent>(OnSurferScore);
        CTEventManager.RemoveListener<ResetEvent>(ResetPowerLevels);
    }

    private void CreateColorList()
    {
        m_lPossibleColors.Add(Color.black);

        m_lPossibleColors.Add(Color.red);
        m_lPossibleColors.Add(Color.green);
        m_lPossibleColors.Add(Color.blue);

        m_lPossibleColors.Add(new Color(1, 1, 0));
        m_lPossibleColors.Add(new Color(1, 0, 1));
        m_lPossibleColors.Add(new Color(0, 1, 1));

        m_lPossibleColors.Add(new Color(1, 1, 1));
    }

    public void ResetPowerLevels(ResetEvent eventData = null)
    {
        //Debug.Log("Reset");
        m_dPowerLevels = new Dictionary<SURFBOARDCOLOR, float>();
        m_dTotalPowerLevels = new Dictionary<SURFBOARDCOLOR, float>();

        

        int idx;
        SURFBOARDCOLOR curColor;
        for (idx = 0; idx < m_dTotalColors.Count; ++idx)
        {
            curColor = m_dTotalColors[idx];

            m_dPowerLevels.Add(curColor, 0f);
            m_dTotalPowerLevels.Add(curColor, 0f);
        }

        CTEventManager.FireEvent(new SetPowerEvent() { m_scColor = SURFBOARDCOLOR.BLUE, value = 0 });
        CTEventManager.FireEvent(new SetPowerEvent() { m_scColor = SURFBOARDCOLOR.RED, value = 0 });
        CTEventManager.FireEvent(new SetPowerEvent() { m_scColor = SURFBOARDCOLOR.GREEN, value = 0 });
    }

	public float GetGlobalScrollSpeed()
	{
		return m_fGlobalScrollSpeed;
	}

    public void OnSurferScore(UpdateSurfboardScoreEvent eventData)
    {
        int idx;
        SURFBOARDCOLOR colorIdx;
        float powerLevel;
        float totalPowerLevel;
        foreach (KeyValuePair<GameObject, SURFBOARDCOLOR> go in eventData.color)
        {
            colorIdx = eventData.color[go.Key];
            powerLevel = m_dPowerLevels[colorIdx] + (eventData.scoreDelta / 100f);
            totalPowerLevel = m_dTotalPowerLevels[colorIdx] + (eventData.scoreDelta / 100f);

            m_dTotalPowerLevels[colorIdx] = (totalPowerLevel > 1) ? 1 : (totalPowerLevel < 0 ? 0 : totalPowerLevel); //Always add to total
            Debug.Log("GGJ: m_dTotalPowerLevels[colorIdx]: " + m_dTotalPowerLevels[colorIdx]);

            if (eventData.addScore)
            {
                m_dPowerLevels[colorIdx] = (powerLevel > 1) ? 1 : (powerLevel < 0 ? 0 : powerLevel);
                CTEventManager.FireEvent(new SetPowerEvent() { m_scColor = colorIdx, value = m_dPowerLevels[colorIdx] });
            }
        }

        if (!eventData.addScore)
        {
            //Kill Combo
        }
    }

    private void OnFullTotalScore()
    {
        //Launch Boss Fight
    }


    public void OnSurferUp(SurferUpEvent eventData)
	{
		switch (eventData.color)
		{
			case SURFBOARDCOLOR.BLUE: Debug.Log("spawn BLUE surfer"); break;
			case SURFBOARDCOLOR.GREEN: Debug.Log("spawn GREEN surfer"); break;
			case SURFBOARDCOLOR.RED: Debug.Log("spawn RED surfer"); break;
            default: break;
		}

		CTEventManager.FireEvent(new SpawnNewSurferEvent() { color = eventData.color });

	}
}
