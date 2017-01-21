using UnityEngine;
using System.Collections;

public class MainCharacterFollowCam : MonoBehaviour {
    public GameObject Target;
    public float m_fScreenEdgeLeft;
    public float m_fScreenEdgeRight;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector3(Target.transform.position.x ,transform.position.y, transform.position.z);

        if(transform.position.x < m_fScreenEdgeLeft)
        {
            transform.position = new Vector3(m_fScreenEdgeLeft, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > m_fScreenEdgeRight)
        {
            transform.position = new Vector3(m_fScreenEdgeRight, transform.position.y, transform.position.z);
        }
    }
}
