using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

    public float moveSpeed = 1;
    public GameObject StartingNode;
    public GameObject EndNode;
    private float m_v3Direction = 1.0f;

    // Use this for initialization
    void Start () {
        transform.position = StartingNode.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 directionToTarget = (EndNode.transform.position - StartingNode.transform.position).normalized;

        transform.position = transform.position + directionToTarget * moveSpeed * Time.deltaTime * m_v3Direction;



        if(m_v3Direction < 0)//going left
        {
            float distanceToTaget = (transform.position - StartingNode.transform.position).magnitude;
            if(distanceToTaget < 1)
            {
                m_v3Direction = 1;
            }
        }

        if (m_v3Direction > 0)//going right
        {
            float distanceToTaget = (transform.position - EndNode.transform.position).magnitude;
            if (distanceToTaget < 1)
            {
                m_v3Direction = -1;
            }
        }
    }
}
