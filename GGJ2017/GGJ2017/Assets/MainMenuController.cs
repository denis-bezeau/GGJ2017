using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
	public RectTransform BG;
	public RectTransform Text;
	public RectTransform PressStart;

	public float BG_FADE_IN_TIME;
	public float TEXT_SLIDE_IN_TIME;

	public float PRESS_START_TIME;
	public float PressStartFadeDirection = -1;


	private float TextStartPos = 0;
	private float TextEndPos = 0;


	float fadeTimer;
	float slideTimer;
	float pressStartTimer;

	// Use this for initialization
	void Start () {
		fadeTimer = 0;
		slideTimer = 0;
		TextStartPos = Text.localPosition.x;
		PressStart.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

	}
	
	// Update is called once per frame
	void Update ()
	{
		fadeTimer += Time.deltaTime;
		if (fadeTimer > BG_FADE_IN_TIME) fadeTimer = BG_FADE_IN_TIME;
		BG.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(0.0f, 1.0f, fadeTimer / BG_FADE_IN_TIME));


		if (fadeTimer == BG_FADE_IN_TIME)
		{
			slideTimer += Time.deltaTime;
			if (slideTimer > TEXT_SLIDE_IN_TIME) slideTimer = TEXT_SLIDE_IN_TIME;
			Text.localPosition = new Vector3(Mathf.Lerp(TextStartPos, TextEndPos, slideTimer / TEXT_SLIDE_IN_TIME), 0, 0);
		}


		if (fadeTimer == BG_FADE_IN_TIME && slideTimer == TEXT_SLIDE_IN_TIME)
		{
			PressStart.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			pressStartTimer += Time.deltaTime * PressStartFadeDirection;
			if (pressStartTimer > PRESS_START_TIME)
			{
				pressStartTimer = PRESS_START_TIME;
				PressStartFadeDirection *= -1;
			}
			else if (pressStartTimer < 0)
			{
				pressStartTimer = 0;
				PressStartFadeDirection *= -1;
			}

			PressStart.GetComponent<Image>().transform.localScale = new Vector3(
				Mathf.Lerp(0.7f, 0.9f, pressStartTimer / PRESS_START_TIME), 
				Mathf.Lerp(0.7f, 0.9f, pressStartTimer / PRESS_START_TIME), 
				Mathf.Lerp(0.7f, 0.9f, pressStartTimer / PRESS_START_TIME));


			if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
			{
				SceneManager.LoadScene("TestGame1");
			}
		}


	}
}
