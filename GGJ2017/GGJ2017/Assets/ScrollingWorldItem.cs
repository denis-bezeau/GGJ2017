﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingWorldItem : MonoBehaviour
{

    private float m_fScrollSpeed = 0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(GGJ2017GameManager.GetInstance() != null && m_fScrollSpeed != GGJ2017GameManager.GetInstance().GetGlobalScrollSpeed())
        {
            m_fScrollSpeed = GGJ2017GameManager.GetInstance().GetGlobalScrollSpeed();
        }
        transform.localPosition = transform.localPosition += Vector3.left * Time.deltaTime * m_fScrollSpeed;
	}
}
