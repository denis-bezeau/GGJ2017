using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWave : MonoBehaviour
{
    private string PLAYER_TAG = "Player";
    private string RENDERER_NAME = "Bgd";

    public Dictionary<GameObject, GGJ2017GameManager.SURFBOARDCOLOR> m_lPlayerColors = new Dictionary<GameObject, GGJ2017GameManager.SURFBOARDCOLOR>();
    public GGJ2017GameManager.SURFBOARDCOLOR m_scWaveColor;
    public Color m_cWaveColor = Color.green;
    private GameObject m_goRenderer;

    private float m_fColorAlpha = 0.5f;

    void Init()
    {
        m_scWaveColor = GGJ2017GameManager.SURFBOARDCOLOR.GREEN;

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject Go = gameObject.transform.GetChild(i).gameObject;
            if (Go.name == RENDERER_NAME) { m_goRenderer = Go; }
        }

        DetermineColor();
    }

    // Use this for initialization
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DetermineColor()
    {
        //Debug.Log("MainWave: DetermineColor:");

        m_cWaveColor = Color.black;
        //Debug.Log("MainWave: DetermineColor: m_cWaveColor: r: " + m_cWaveColor.r + ", g: " + m_cWaveColor.g + ", b: " + m_cWaveColor.b);
        foreach (KeyValuePair<GameObject,GGJ2017GameManager.SURFBOARDCOLOR> go in m_lPlayerColors)
        {
            m_cWaveColor += GGJ2017GameManager.m_dSurfboardColorToColor[go.Value];
            //Debug.Log("MainWave: DetermineColor: m_cWaveColor: r: " + m_cWaveColor.r + ", g: " + m_cWaveColor.g + ", b: " + m_cWaveColor.b);
        }
        m_cWaveColor.a = m_fColorAlpha;

        m_goRenderer.GetComponent<SpriteRenderer>().color = m_cWaveColor;
    }
    
    public Dictionary<GameObject, GGJ2017GameManager.SURFBOARDCOLOR> GetColors()
    {
        return m_lPlayerColors;
    }

    public Color GetColor()
    {
        return m_cWaveColor;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("MainWave: OnTriggerEnter2D: TRIGGERED");
        if (col.gameObject.tag == PLAYER_TAG)
        {
            //Debug.Log("GO Color: " + col.GetComponent<CharacterController>().GetColor());
            m_lPlayerColors.Add(col.gameObject, col.GetComponent<CharacterController>().GetColor());
            DetermineColor();
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        //Debug.Log("MainWave: OnTriggerStay2D: TRIGGERED");
        if (col.gameObject.tag == PLAYER_TAG)
        {
            //Debug.Log("TRIGGERED O_O Stay");
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        //Debug.Log("MainWave: OnTriggerExit2D: TRIGGERED");
        if (col.gameObject.tag == PLAYER_TAG)
        {
            m_lPlayerColors.Remove(col.gameObject);
            DetermineColor();
        }
    }
}
