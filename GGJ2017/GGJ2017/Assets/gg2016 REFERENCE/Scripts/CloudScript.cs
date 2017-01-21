using UnityEngine;
using System.Collections;

public class CloudScript : MonoBehaviour {

    public float m_fMoveSpeed;
    public float m_fLHS;
    public float m_fRHS;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = transform.position - new Vector3(1.0f, 0.0f, 0.0f) * m_fMoveSpeed * Time.deltaTime;
        if(transform.position.x < m_fLHS)
        {
            transform.position = new Vector3(m_fRHS, transform.position.y, transform.position.z);
        }
	}
}
