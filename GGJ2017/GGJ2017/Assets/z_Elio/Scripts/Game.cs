using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static KeyCode COLOR_1 = KeyCode.A;
    public static KeyCode COLOR_2 = KeyCode.S;
    public static KeyCode COLOR_3 = KeyCode.D;

    public static int SINGLE_PLAYER_GAME = 0;
    public static int MULTI_PLAYER_GAME = 1;

    public static int SP_STARTING_WAVES = 1;
    public static int MP_STARTING_WAVES = 3;

    public static Game game;

    public List<GameObject> Characters;
    public int iPlayerMode = SINGLE_PLAYER_GAME;

    void Awake()
    {
        Debug.Log("Game: Awake:");
        if(game == null)
        {
            Debug.Log("Game: Awake: game is null");
            DontDestroyOnLoad(this.gameObject);
            game = this;
        }
        else if(game != null)
        {
            Debug.Log("Game: Awake: game is not null destroy this instance");
            Destroy(this.gameObject);
        }

		CTEventManager.AddListener<SpawnNewSurferEvent>(OnSpawnNewSurfer);
    }

	public void OnDestroy()
	{
		CTEventManager.RemoveListener<SpawnNewSurferEvent>(OnSpawnNewSurfer);
	}

	// Use this for initialization
	void Start ()
    {
        Debug.Log("Game: Start:");

        int charactersToCreate = (iPlayerMode < MULTI_PLAYER_GAME) ? SP_STARTING_WAVES : MP_STARTING_WAVES;
        Debug.Log("Game: Start: charactersToCreate: " + charactersToCreate);

        Characters = new List<GameObject>();
        GameObject goCharacter;
        CharacterController cCharacterController;
        
        for(int i = 0; i < charactersToCreate; ++i)
        {
            Debug.Log("Game: Start: Loop: " + i);
            goCharacter = Instantiate(Resources.Load("Player")) as GameObject;
            cCharacterController = goCharacter.AddComponent<CharacterController>();
            cCharacterController.CreateCharacterController((KeyCode)GetType().GetField("COLOR_" + (i + 1)).GetValue(this), i);

            cCharacterController.PositionCharacter(charactersToCreate);
        }
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

	public void OnSpawnNewSurfer(SpawnNewSurferEvent eventData)
	{
		GameObject goCharacter;
		CharacterController cCharacterController;
		goCharacter = Instantiate(Resources.Load("Player")) as GameObject;
		cCharacterController = goCharacter.AddComponent<CharacterController>();
	}
}
