using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private static float LIFE_TIME = 10f;

    private static string LEFT_CHILD = "leftCP";
    private static string RIGHT_CHILD = "rightCP";
    private GameObject m_goLeftChild;
    private GameObject m_goRightChild;

    private GGJ2017GameManager.SURFBOARDCOLOR m_scCPColor;
    private Color m_cCPColor = new Color();

    private bool m_bInited = false;
    private float m_fLifeTime = 0;
    private bool m_bUsed = false;

    // Use this for initialization
    void Start()
    {
        Init();
    }

    private void Init()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject Go = gameObject.transform.GetChild(i).gameObject;
            if (Go.name == LEFT_CHILD) { m_goLeftChild = Go; }
            if (Go.name == RIGHT_CHILD) { m_goRightChild = Go; }
        }

        m_bInited = true;

        ChangeColor();
	}
	
	// Update is called once per frame
	void Update () {
        m_fLifeTime += Time.deltaTime;
        if (m_fLifeTime > LIFE_TIME) { Destroy(gameObject); }
    }

    public void ChangeColor()
    {
        //change color value stored
        List<Color> colorList = GGJ2017GameManager.m_lPossibleColors;
        m_cCPColor = colorList[Random.Range(0, (colorList.Count - 1))];

        //change ring color
        redrawColor();
    }

    public void ChangeColor(GGJ2017GameManager.SURFBOARDCOLOR colorIn)
    {
        //change color value stored
        m_scCPColor = colorIn;
        m_cCPColor = GGJ2017GameManager.m_dSurfboardColorToColor[m_scCPColor];

        //change ring color
        redrawColor();
    }

    private void redrawColor()
    {
        if(!m_bInited)
        {
            Init();
            return;
        }

        m_goLeftChild.GetComponent<SpriteRenderer>().color = m_cCPColor;
        m_goRightChild.GetComponent<SpriteRenderer>().color = m_cCPColor;
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("CheckPoint: TriggerEnter: " + col.gameObject.tag);
        if (col.gameObject.tag == Game.WAVE_TAG)
        {
            bool correctColor = (col.gameObject.GetComponent<MainWave>().GetColor() == m_cCPColor);//check with full alpha
            //Debug.Log("CheckPoint: TriggerEnter: Run event: " + correctColor);

            if(!m_bUsed)
            {
                m_bUsed = true;

                CTEventManager.FireEvent(new UpdateSurfboardScoreEvent() { color = m_cCPColor, addScore = correctColor }); //make per life
                Destroy(gameObject);
            }
        }
    }
}
