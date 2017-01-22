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

	public List<CharacterController> Characters;
    public int iPlayerMode = SINGLE_PLAYER_GAME;

	public List<CharacterController> BlueCharacters = new List<CharacterController>();
	public List<CharacterController> RedCharacters = new List<CharacterController>();
	public List<CharacterController> GreenCharacters = new List<CharacterController>();
	public List<CharacterController> YellowCharacters = new List<CharacterController>();

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
		CTEventManager.AddListener<JumpEvent>(OnJump);
    }

	public void OnDestroy()
	{
		CTEventManager.RemoveListener<SpawnNewSurferEvent>(OnSpawnNewSurfer);
		CTEventManager.RemoveListener<JumpEvent>(OnJump);
	}

	// Use this for initialization
	void Start ()
    {
        Debug.Log("Game: Start:");

        int charactersToCreate = (iPlayerMode < MULTI_PLAYER_GAME) ? SP_STARTING_WAVES : MP_STARTING_WAVES;
        Debug.Log("Game: Start: charactersToCreate: " + charactersToCreate);

		Characters = new List<CharacterController>();
        GameObject goCharacter;
        CharacterController cCharacterController;
        
        for(int i = 0; i < charactersToCreate; ++i)
        {
            Debug.Log("Game: Start: Loop: " + i);
            goCharacter = Instantiate(Resources.Load("Player")) as GameObject;
            cCharacterController = goCharacter.AddComponent<CharacterController>();
            cCharacterController.CreateCharacterController((KeyCode)GetType().GetField("COLOR_" + (i + 1)).GetValue(this), i);
			cCharacterController.m_eColor = (GGJ2017GameManager.SURFBOARDCOLOR)i;
			Characters.Add(cCharacterController);
            cCharacterController.PositionCharacter(charactersToCreate);

			switch (cCharacterController.m_eColor)
			{
				case GGJ2017GameManager.SURFBOARDCOLOR.RED: RedCharacters.Add(cCharacterController); break;
				case GGJ2017GameManager.SURFBOARDCOLOR.GREEN: GreenCharacters.Add(cCharacterController); break;
				case GGJ2017GameManager.SURFBOARDCOLOR.BLUE: BlueCharacters.Add(cCharacterController); break;
				case GGJ2017GameManager.SURFBOARDCOLOR.YELLOW: YellowCharacters.Add(cCharacterController); break;
			}
        }
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

	public void OnSpawnNewSurfer(SpawnNewSurferEvent eventData)
	{
		GameObject goCharacter = Instantiate(Resources.Load("Player")) as GameObject;
		CharacterController cCharacterController = goCharacter.AddComponent<CharacterController>();
		cCharacterController.m_eColor = eventData.color;
		cCharacterController.PositionCharacter(1);
		Characters.Add(cCharacterController);

		switch (eventData.color)
		{
			case GGJ2017GameManager.SURFBOARDCOLOR.RED:
				if (RedCharacters.Count  > 0)
				{
					cCharacterController.SetFollowTarget(RedCharacters[RedCharacters.Count-1]);
				}
				
				cCharacterController.m_iPlayerIndexForYourColor = (RedCharacters.Count);
				RedCharacters.Add(cCharacterController);
				cCharacterController.CreateCharacterController(KeyCode.A, RedCharacters.Count);
				break;
			case GGJ2017GameManager.SURFBOARDCOLOR.GREEN:  
				if (GreenCharacters.Count > 0)
				{
					cCharacterController.SetFollowTarget(GreenCharacters[GreenCharacters.Count - 1]);
				}

				cCharacterController.m_iPlayerIndexForYourColor = (GreenCharacters.Count);
				GreenCharacters.Add(cCharacterController);
				cCharacterController.CreateCharacterController(KeyCode.A, GreenCharacters.Count);
				break;
			case GGJ2017GameManager.SURFBOARDCOLOR.BLUE:  
				if (BlueCharacters.Count > 0)
				{
					cCharacterController.SetFollowTarget(BlueCharacters[BlueCharacters.Count - 1]);
				}

				cCharacterController.m_iPlayerIndexForYourColor = (BlueCharacters.Count);
				BlueCharacters.Add(cCharacterController);
				cCharacterController.CreateCharacterController(KeyCode.A, BlueCharacters.Count);
				break;
			case GGJ2017GameManager.SURFBOARDCOLOR.YELLOW:  
				if (YellowCharacters.Count > 0)
				{
					cCharacterController.SetFollowTarget(YellowCharacters[YellowCharacters.Count - 1]);
				}

				cCharacterController.m_iPlayerIndexForYourColor = (YellowCharacters.Count);
				YellowCharacters.Add(cCharacterController);
				cCharacterController.CreateCharacterController(KeyCode.A, YellowCharacters.Count);
				break;
		}
	}

	public void OnJump(JumpEvent eventData)
	{
		float fJumpDelay = 0;
		foreach (CharacterController character in Characters)
		{
			if (character.m_bYielding)
			{
				continue;
			}

			if (eventData.color == character.m_eColor)
			{
				if (character.m_bJumping == false)
				{
					character.m_bYielding = true;
				}
				
				StartCoroutine(JumpInXSeconds(character, fJumpDelay));
				fJumpDelay += 0.1f;
			}
		}
	}

	private IEnumerator JumpInXSeconds(CharacterController character, float time)
	{
		yield return new WaitForSeconds(time);
		character.OnJump();
	}
}
