using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject player;
    public GameObject respawn;

    public DynamicBlock[] dyanamicBlocks;

    int GameLevel = 0;

    static GameManager instance;

    

    public static GameManager GetInstance()
    {
        return instance;

    }

	// Use this for initialization
	void Start () {
        dyanamicBlocks = FindObjectsOfType<DynamicBlock>() as DynamicBlock[];

        Respawn();
        instance = this;
	}
	
	
	public void LevelUp () {
        GameLevel++;
	}

    public int GetGameLevel()
    { return GameLevel;
    }

    public void Respawn()
    {
        player.transform.position = respawn.transform.position;
        player.GetComponent<CharacterControllerScript>().m_bJumping = false;
        CollisionDetectorHack.GetInstance().DeleteAllSpawnedObjects();

        
        foreach(DynamicBlock currentBlock in dyanamicBlocks)
        {
            bool isAvailableForThisLevel = false;
            foreach(int currentlevel in currentBlock.ShowForTheseLevels )
            {
                if(currentlevel == GameLevel)
                {
                    isAvailableForThisLevel = true;
                }
            }

            currentBlock.gameObject.SetActive(isAvailableForThisLevel);

        }
    }
}
