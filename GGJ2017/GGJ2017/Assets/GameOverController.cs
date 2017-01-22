using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
	public RectTransform BG;
	public float BG_FADE_IN_TIME;
	private float TextStartPos = 0;
	private float TextEndPos = 0;


	float fadeTimer;


	// Use this for initialization
	void Start () {
		fadeTimer = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		fadeTimer += Time.deltaTime;
		if (fadeTimer > BG_FADE_IN_TIME) fadeTimer = BG_FADE_IN_TIME;
		BG.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(0.0f, 1.0f, fadeTimer / BG_FADE_IN_TIME));

		if (fadeTimer == BG_FADE_IN_TIME)
		{
			if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
			{
				SceneManager.LoadScene("MainMenu");
			}
		}
	}
}
