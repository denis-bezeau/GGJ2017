using UnityEngine;
using System.Collections;

public class FishSpawner : MonoBehaviour {

    public GameObject FishPrefab;
    public GameObject[] spawnPoints;
    public float SpawnTime;
    private float spawnTimer;
	
	// Update is called once per frame
	void Update () {
        spawnTimer -= Time.deltaTime;
        if(spawnTimer < 0)
        {
            spawnTimer += SpawnTime;
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                GameObject newFish = GameObject.Instantiate(FishPrefab, spawnPoints[i].transform.position, Quaternion.identity) as GameObject;
                CollisionDetectorHack.GetInstance().AddSpawnedCollider(newFish.GetComponent<BoxCollider2D>());
            }
        }

    }
}
