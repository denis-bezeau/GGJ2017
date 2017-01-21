using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingWorldItem : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = transform.localPosition += Vector3.left * Time.deltaTime * GGJ2017GameManager.GetInstance().GetGlobalScrollSpeed();
	}
}
