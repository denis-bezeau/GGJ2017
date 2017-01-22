using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private string LEFT_CHILD = "leftCP";
    private string RIGHT_CHILD = "rightCP";

    private string WAVE_TAG = "Wave";

    private Color m_cCPColor = new Color();
    private GGJ2017GameManager.SURFBOARDCOLOR m_scCPColor;

    private bool m_bInited = false;
    public bool m_bUsable = true;

    private GameObject m_goLeftChild;
    private GameObject m_goRightChild;

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

        ChangeColor(); //REMOVE
	}
	
	// Update is called once per frame
	void Update () {
		if(m_bUsable) //For Testing
        {
            ResetCheckPoint();
        }
	}

    public void ResetCheckPoint()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }

    private void UseCheckPoint()
    {
        //animate separation

        //disable collider
        m_bUsable = false;
        GetComponent<BoxCollider2D>().enabled = m_bUsable;
    }

    public void ChangeColor()
    {
        ResetCheckPoint();

        //change color value stored
        List<Color> colorList = GGJ2017GameManager.m_lPossibleColors;
        m_cCPColor = colorList[Random.Range(0, (colorList.Count - 1))];

        //change ring color
        redrawColor();
    }

    public void ChangeColor(GGJ2017GameManager.SURFBOARDCOLOR colorIn)
    {
        ResetCheckPoint();

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

        Debug.Log("CheckPoint: redraw: color: " + m_cCPColor);
        m_goLeftChild.GetComponent<SpriteRenderer>().color = m_cCPColor;
        m_goRightChild.GetComponent<SpriteRenderer>().color = m_cCPColor;
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("TRIGGERED O_O");
        if (col.gameObject.tag == WAVE_TAG)
        {
            Debug.Log("TRIGGERED Wave");
            bool correctColor = (col.gameObject.GetComponent<MainWave>().GetColor() == m_cCPColor);

            //correctColor = true; //REMOVE
            CTEventManager.FireEvent(new UpdateSurfboardScoreEvent() { color = col.gameObject.GetComponent<MainWave>().GetColors(), scoreDelta = 10, addScore = correctColor }); //make per life
            UseCheckPoint();
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == WAVE_TAG)
        {
            Debug.Log("TRIGGERED O_O Stay");
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == WAVE_TAG)
        {
            Debug.Log("TRIGGERED O_O Exit");
        }
    }
}
