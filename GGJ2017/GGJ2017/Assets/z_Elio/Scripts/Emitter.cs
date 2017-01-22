using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour
{
    public GameObject m_goEmittedObject;
    public GameObject m_goAltEmittedObject;
    public float m_fRepeatTime = 15f;

    private GameObject m_goEmittedObjectInstance;
    private float m_fTime = 0;

	// Use this for initialization
	void Start ()
    {
        m_fTime = 0;
        CreateObject();
    }

    private void CreateObject()
    {
        GameObject createdObject = m_goEmittedObject;
        if (m_goAltEmittedObject != null)
        {
            if(Random.RandomRange(0,10) % 5 == 0)
            {
                createdObject = m_goAltEmittedObject;
            }
        }
        m_goEmittedObjectInstance = Instantiate(createdObject);
        m_goEmittedObjectInstance.AddComponent<ScrollingWorldItem>();

        m_goEmittedObjectInstance.transform.position = transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        m_fTime += Time.deltaTime;
        //Debug.Log("EMitter: m_fTime : " + m_fTime + ", m_fRepeatTime: " + m_fRepeatTime);

        if (m_fTime > m_fRepeatTime)
        {
            CreateObject();

            m_fTime = 0;
        }
	}
}
