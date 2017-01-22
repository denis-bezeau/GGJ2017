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
    public Color m_cWaveColorFullAlpha;
    private GameObject m_goRenderer;

    private float m_fTime = 0f;

    public GameObject P1;
    public GameObject P2;

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

        DetermineColor(); //Dirty but f**k it amirite?

        m_cWaveColorFullAlpha = m_cWaveColor;
        m_cWaveColorFullAlpha.a = 1;

        if (m_cWaveColorFullAlpha == Color.white)
        {
            ParticleMotion(P1, true);
            ParticleMotion(P2, false);
        }
        else
        {
            if(P1.activeInHierarchy)
            {
                P1.SetActive(false);
            }
            if (P2.activeInHierarchy)
            {
                P2.SetActive(false);
            }
        }
    }

    private void ParticleMotion(GameObject go, bool mod)
    {
        if (!go.activeInHierarchy)
        {
            go.SetActive(true);
        }
        m_fTime += Time.deltaTime;
        if(mod)
        {
            go.transform.position = new Vector3(transform.position.x, transform.position.y - 5 * Mathf.Sin((m_fTime % 1) * (1 * 2) * Mathf.PI), transform.position.z);
        }
        else
        {
            go.transform.position = new Vector3(transform.position.x, transform.position.y + 5 * Mathf.Sin((m_fTime % 1) * (1 * 2) * Mathf.PI), transform.position.z);
        }
    }

    public void DetermineColor()
    {
        //Debug.Log("MainWave: DetermineColor:");

        m_cWaveColor = Color.black;
        //Debug.Log("MainWave: DetermineColor: m_cWaveColor: r: " + m_cWaveColor.r + ", g: " + m_cWaveColor.g + ", b: " + m_cWaveColor.b);
        foreach (KeyValuePair<GameObject,GGJ2017GameManager.SURFBOARDCOLOR> go in GGJ2017GameManager.m_lPlayerColors)
        {
            m_cWaveColor += GGJ2017GameManager.m_dSurfboardColorToColor[go.Value];
            //Debug.Log("MainWave: DetermineColor: m_cWaveColor: r: " + m_cWaveColor.r + ", g: " + m_cWaveColor.g + ", b: " + m_cWaveColor.b);
        }
        m_cWaveColor.a = m_fColorAlpha;
        m_cWaveColor.r = (m_cWaveColor.r > 1) ? 1 : ((m_cWaveColor.r < 0) ? 0 : m_cWaveColor.r);
        m_cWaveColor.g = (m_cWaveColor.g > 1) ? 1 : ((m_cWaveColor.g < 0) ? 0 : m_cWaveColor.g);
        m_cWaveColor.b = (m_cWaveColor.b > 1) ? 1 : ((m_cWaveColor.b < 0) ? 0 : m_cWaveColor.b);


        m_goRenderer.GetComponent<SpriteRenderer>().color = m_cWaveColor;
    }

    public Color GetColor()
    {
        Color waveColor = m_cWaveColor;
        waveColor.a = 1;

        //Debug.Log("MainWave: Color: " + waveColor);
        return waveColor;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("MainWave: OnTriggerEnter2D: TRIGGERED");
        if (col.gameObject.tag == PLAYER_TAG)
        {
            //Debug.Log("GO Color: " + col.GetComponent<CharacterController>().GetColor());
            //Debug.Log("MainWave: trigger Enter: " + col.gameObject);
            CTEventManager.FireEvent(new ReEvaluateSurferEvent() { surfer = col.gameObject, add = true });
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
            //Debug.Log("MainWave: trigger Exit");
            CTEventManager.FireEvent(new ReEvaluateSurferEvent() { surfer = col.gameObject, add = false});
            DetermineColor();
        }
    }
}
