using UnityEngine;
using System.Collections;

public class Parralax : MonoBehaviour {

    public float WiggleRoom = 20;
    public GameObject LeftmostNode;
    public GameObject RightmostNode;
    private GameObject Player;
    private Vector3 OriginalPosition;

    float totalLevelWidth;

    // Use this for initialization
    void Awake () {
        OriginalPosition = transform.localPosition; ;
        Player = FindObjectOfType<CharacterControllerScript>().gameObject;
        totalLevelWidth = (LeftmostNode.transform.position - RightmostNode.transform.position).magnitude;

    }
	
	// Update is called once per frame
	void Update () {
	
        float distanceFromLeftMost = (LeftmostNode.transform.position - Player.transform.position).magnitude;
        float percentageThroughLevel = distanceFromLeftMost / totalLevelWidth;

        transform.localPosition = OriginalPosition - new Vector3(percentageThroughLevel,0,0) * WiggleRoom;
    }
}
