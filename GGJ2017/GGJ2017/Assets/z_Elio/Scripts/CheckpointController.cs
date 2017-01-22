using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public List<GameObject> m_lCheckpoints;
    public GameObject m_goCurrentCP;
    public Checkpoint m_cCurrentCP;

    public int m_iIdx;

    public void CreateRings(int cpQuantity)
    {
        m_iIdx = 0;

        //Instantiate Ring amount
        GameObject checkpoint;
        Vector2 initialPosition = new Vector2(-100, 0);
        for(int i = 0; i < cpQuantity; ++i)
        {
            checkpoint = Instantiate(Resources.Load("Checkpoint")) as GameObject;
            checkpoint.transform.position = initialPosition;
            m_lCheckpoints.Add(checkpoint);
        }

        m_goCurrentCP = m_lCheckpoints[m_iIdx];
    }

    public void SendNext()
    {
        m_goCurrentCP = m_lCheckpoints[++m_iIdx];
        m_cCurrentCP = m_goCurrentCP.GetComponent<Checkpoint>();

        m_cCurrentCP.ChangeColor(GGJ2017GameManager.SURFBOARDCOLOR.GREEN);
    }


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
