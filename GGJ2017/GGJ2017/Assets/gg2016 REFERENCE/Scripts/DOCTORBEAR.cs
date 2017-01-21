using UnityEngine;
using System.Collections;

public class DOCTORBEAR : MonoBehaviour {


    public GameObject[] SpeechBubbles;
    public float speechBubbleDisplayTime;
    private float speechBubbleDisplayTimer;
    bool displayingSpeechBubble;

    int speechBubbletoDisplay = 0;

	// Use this for initialization
	void Start () {

        for (int i = 0; i < SpeechBubbles.Length; ++i)
        {
            if(SpeechBubbles[i] != null)
            SpeechBubbles[i].SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(displayingSpeechBubble == true)
        {
            speechBubbleDisplayTimer -= Time.deltaTime;

            if(speechBubbleDisplayTimer < 0)
            {
                displayingSpeechBubble = false;
                if(speechBubbletoDisplay < SpeechBubbles.Length)
                    if(SpeechBubbles[speechBubbletoDisplay] != null)
                SpeechBubbles[speechBubbletoDisplay].SetActive(false);
            }
        }
	}

    public void OnShowSpeechBubble()
    {
        
        speechBubbleDisplayTimer = speechBubbleDisplayTime;
        if(displayingSpeechBubble == false)
        {
            displayingSpeechBubble = true;
            speechBubbletoDisplay = GameManager.GetInstance().GetGameLevel();
            Debug.Log("collision with doc bear. speechBubbletoDisplay=" + speechBubbletoDisplay);
            for (int i = 0; i < SpeechBubbles.Length; ++i)
            {
                if (SpeechBubbles[i] != null)
                SpeechBubbles[i].SetActive(i == speechBubbletoDisplay);
            }
        }
    }
}
