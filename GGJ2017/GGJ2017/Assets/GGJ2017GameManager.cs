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

public class ReEvaluateSurferEvent : CTEvent
{
    public GameObject surfer;
    public bool add;
}

public class LaunchBossFightEvent : CTEvent
{
}

public class ResetEvent : CTEvent
{
}

public class UpdateSurfboardScoreEvent: CTEvent
{
    public bool addScore;
    public int scoreDelta;
    public Color color;
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
    public static Dictionary<Color, SURFBOARDCOLOR> m_dColorToSurfboardColor = new Dictionary<Color, SURFBOARDCOLOR>();

    public static Dictionary<GameObject, GGJ2017GameManager.SURFBOARDCOLOR> m_lPlayerColors = new Dictionary<GameObject, GGJ2017GameManager.SURFBOARDCOLOR>();

    public Emitter CPEmitter;
    public Emitter PEmitter;
    public Boss boss;
    private float totalPointsSoFar = 0f;

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
        m_dSurfboardColorToColor.Clear();
        m_dSurfboardColorToColor.Add(SURFBOARDCOLOR.BLUE, Color.blue);
        m_dSurfboardColorToColor.Add(SURFBOARDCOLOR.RED, Color.red);
        m_dSurfboardColorToColor.Add(SURFBOARDCOLOR.GREEN, Color.green);

        m_dColorToSurfboardColor.Clear();
        m_dColorToSurfboardColor.Add(Color.blue, SURFBOARDCOLOR.BLUE);
        m_dColorToSurfboardColor.Add(Color.red, SURFBOARDCOLOR.RED);
        m_dColorToSurfboardColor.Add(Color.green, SURFBOARDCOLOR.GREEN);

        m_dTotalColors.Clear();
        m_dTotalColors.Add(SURFBOARDCOLOR.BLUE);
        m_dTotalColors.Add(SURFBOARDCOLOR.RED);
        m_dTotalColors.Add(SURFBOARDCOLOR.GREEN);

        CreateColorList();
        ResetPowerLevels();

        CTEventManager.AddListener<SurferUpEvent>(OnSurferUp);
        CTEventManager.AddListener<UpdateSurfboardScoreEvent>(OnSurferScore);
        CTEventManager.AddListener<ReEvaluateSurferEvent>(RebuildDictionary);
        CTEventManager.AddListener<ResetEvent>(ResetPowerLevels);
        CTEventManager.AddListener<LaunchBossFightEvent>(LaunchBossBattle);
    }

	public void OnDestroy()
	{
		CTEventManager.RemoveListener<SurferUpEvent>(OnSurferUp);
        CTEventManager.RemoveListener<UpdateSurfboardScoreEvent>(OnSurferScore);
        CTEventManager.RemoveListener<ReEvaluateSurferEvent>(RebuildDictionary);
        CTEventManager.RemoveListener<ResetEvent>(ResetPowerLevels);
    }

    private void CreateColorList()
    {
        m_lPossibleColors.Clear();
        m_lPossibleColors.Add(Color.black);

        m_lPossibleColors.Add(Color.red);
        m_lPossibleColors.Add(Color.green);
        m_lPossibleColors.Add(Color.blue);

        m_lPossibleColors.Add(new Color(1, 1, 0));
        m_lPossibleColors.Add(new Color(1, 0, 1));
        m_lPossibleColors.Add(new Color(0, 1, 1));

        m_lPossibleColors.Add(new Color(1, 1, 1));
    }

    public void LaunchBossBattle(LaunchBossFightEvent eventData)
    {
        CPEmitter.gameObject.SetActive(false);
        PEmitter.gameObject.SetActive(false);

        boss.gameObject.SetActive(true);
    }

    public static void RebuildDictionary(ReEvaluateSurferEvent eventData)
    {
        foreach (KeyValuePair<GameObject, SURFBOARDCOLOR> go in m_lPlayerColors)
        {
            //Debug.Log("GO.value: pre" + go.Value);
        }

        //Debug.Log("GO.value: POST");

        if (eventData.add)
        {
            if (!m_lPlayerColors.ContainsKey(eventData.surfer))
            {
                m_lPlayerColors.Add(eventData.surfer, eventData.surfer.GetComponent<CharacterController>().m_scPlayerColor);
            }
        }
        else
        {
            if (m_lPlayerColors.ContainsKey(eventData.surfer))
            {
                m_lPlayerColors.Remove(eventData.surfer);
            }
        }

        foreach(KeyValuePair<GameObject, SURFBOARDCOLOR> go in m_lPlayerColors)
        {
            //Debug.Log("GO.value: post" + go.Value);
        }
    }

    public void ResetPowerLevels(ResetEvent eventData = null)
    {
        CPEmitter.gameObject.SetActive(true);
        PEmitter.gameObject.SetActive(true);

        boss.gameObject.SetActive(false);

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

        Color currentColor;

        Debug.Log("GGJ Color: ");

        List<Color> rgb = new List<Color>();
        rgb.Add(new Color(eventData.color.r,0,0));
        rgb.Add(new Color(0, eventData.color.g, 0));
        rgb.Add(new Color(0,0,eventData.color.b));

        for (idx = 0; idx < 3; ++idx)
        {
            currentColor = rgb[idx];
            Debug.Log("GGJ Color: i: " + idx + ", part: " + currentColor + ", color: " + eventData.color);

            if (m_dColorToSurfboardColor.ContainsKey(currentColor))
            {
                colorIdx = m_dColorToSurfboardColor[currentColor];

                totalPowerLevel = m_dTotalPowerLevels[colorIdx] + .1f;
                powerLevel = m_dTotalPowerLevels[colorIdx] + .1f;

                m_dTotalPowerLevels[colorIdx] = (totalPowerLevel > 1) ? 1 : (totalPowerLevel < 0 ? 0 : totalPowerLevel); //Always add to total

                if (eventData.addScore)
                {
                    m_dPowerLevels[colorIdx] = (powerLevel > 1) ? 1 : (powerLevel < 0 ? 0 : powerLevel);
                    CTEventManager.FireEvent(new SetPowerEvent() { m_scColor = colorIdx, value = m_dPowerLevels[colorIdx] });
                }
            }
        }

        /*
        foreach (KeyValuePair<GameObject, SURFBOARDCOLOR> go in m_lPlayerColors)
        {
            colorIdx = m_lPlayerColors[go.Key];
            powerLevel = m_dPowerLevels[colorIdx] + (eventData.scoreDelta / 100f);
            totalPowerLevel = m_dTotalPowerLevels[colorIdx] + (eventData.scoreDelta / 100f);

            m_dTotalPowerLevels[colorIdx] = (totalPowerLevel > 1) ? 1 : (totalPowerLevel < 0 ? 0 : totalPowerLevel); //Always add to total
            Debug.Log("GGJ: m_dTotalPowerLevels[colorIdx]: " + m_dTotalPowerLevels[colorIdx]);

            if (eventData.addScore)
            {
                m_dPowerLevels[colorIdx] = (powerLevel > 1) ? 1 : (powerLevel < 0 ? 0 : powerLevel);
                CTEventManager.FireEvent(new SetPowerEvent() { m_scColor = colorIdx, value = m_dPowerLevels[colorIdx] });
            }

            totalPointsSoFar = 0f;
            foreach (KeyValuePair<SURFBOARDCOLOR, float>sf in m_dTotalPowerLevels)
            {
                totalPointsSoFar += (m_dTotalPowerLevels[sf.Key] / 3);
            }
            Debug.Log("GGJ totalpts: " + totalPointsSoFar);

            if (totalPointsSoFar >= 1f)
            {
                CTEventManager.FireEvent(new LaunchBossFightEvent() { });
            }
        }//*/

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
