using UnityEngine;
using System.Collections;

public class MeteorSpawner : MonoBehaviour {

    public GameObject MeteorPrefab;

    float timer = 3.0f;
    float timeout = 3.0f;

    
    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
	}

    public void Spawn()
    {
        if(timer < 0)
        {
            GameObject meteor1 = GameObject.Instantiate(MeteorPrefab, transform.position + Vector3.left * 00, Quaternion.identity) as GameObject;
            GameObject meteor2 = GameObject.Instantiate(MeteorPrefab, transform.position + Vector3.left * 5, Quaternion.identity) as GameObject;
            GameObject meteor3 = GameObject.Instantiate(MeteorPrefab, transform.position + Vector3.left * 10, Quaternion.identity) as GameObject;

            CollisionDetectorHack.GetInstance().AddSpawnedCollider(meteor1.GetComponent<BoxCollider2D>());
            CollisionDetectorHack.GetInstance().AddSpawnedCollider(meteor2.GetComponent<BoxCollider2D>());
            CollisionDetectorHack.GetInstance().AddSpawnedCollider(meteor3.GetComponent<BoxCollider2D>());
            timer = timeout;
        }
        
    }
}
