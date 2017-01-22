using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static string EMITTER_TAG = "Emitter";
    public static string WAVE_TAG = "Wave";
    public static string PLAYER_TAG = "Player";
    public static string EMPTY_TAG = "NONE";

    public static KeyCode RED_BUTTON = KeyCode.D;
    public static KeyCode BLUE_BUTTON = KeyCode.A;
    public static KeyCode GREEN_BUTTON = KeyCode.S;
	public static KeyCode YELLOW_BUTTON = KeyCode.W;

    public static int SINGLE_PLAYER_GAME = 0;
    public static int MULTI_PLAYER_GAME = 1;

    public static int SP_STARTING_WAVES = 3;
    public static int MP_STARTING_WAVES = 3;

    public static Game game;

    public List<CharacterController> Characters;
    public int iPlayerMode = SINGLE_PLAYER_GAME;

	public List<CharacterController> BlueCharacters = new List<CharacterController>();
	public List<CharacterController> RedCharacters = new List<CharacterController>();
	public List<CharacterController> GreenCharacters = new List<CharacterController>();


	public GameObject m_SpawningNodeBlue;
	public GameObject m_SpawningNodeGreen;
	public GameObject m_SpawningNodeRed;
	public GameObject m_oJumpNode;

    void Awake()
    {
        //Debug.Log("Game: Awake:");
        if(game == null)
        {
            //Debug.Log("Game: Awake: game is null");
            DontDestroyOnLoad(this.gameObject);
            game = this;
        }
        else if(game != null)
        {
            //Debug.Log("Game: Awake: game is not null destroy this instance");
            Destroy(this.gameObject);
        }
		CTEventManager.AddListener<SpawnNewSurferEvent>(OnSpawnNewSurfer);
		CTEventManager.AddListener<KillSurferEvent>(OnKillSurfer);
		CTEventManager.AddListener<JumpEvent>(OnJump);
    }

	public void OnDestroy()
	{
		CTEventManager.RemoveListener<SpawnNewSurferEvent>(OnSpawnNewSurfer);
		CTEventManager.RemoveListener<KillSurferEvent>(OnKillSurfer);
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
			cCharacterController.m_scPlayerColor = (GGJ2017GameManager.SURFBOARDCOLOR)i;
            goCharacter.GetComponent<SpriteRenderer>().color = GGJ2017GameManager.m_dSurfboardColorToColor[cCharacterController.m_scPlayerColor];

            Characters.Add(cCharacterController);

			switch (cCharacterController.m_scPlayerColor)
			{
				case GGJ2017GameManager.SURFBOARDCOLOR.RED: 
					RedCharacters.Add(cCharacterController);
					cCharacterController.CreateCharacterController(RED_BUTTON, i);
					cCharacterController.PositionCharacter(m_SpawningNodeRed);
					break;
				case GGJ2017GameManager.SURFBOARDCOLOR.GREEN: 
					GreenCharacters.Add(cCharacterController);
					cCharacterController.CreateCharacterController(GREEN_BUTTON, i);
					cCharacterController.PositionCharacter(m_SpawningNodeGreen);
					break;
				case GGJ2017GameManager.SURFBOARDCOLOR.BLUE: 
					BlueCharacters.Add(cCharacterController);
					cCharacterController.CreateCharacterController(BLUE_BUTTON, i);
					cCharacterController.PositionCharacter(m_SpawningNodeBlue);
					break;
                default:
                    break;
			}

            CTEventManager.FireEvent(new ReEvaluateSurferEvent() { surfer = goCharacter, add = true });
        }

        CTEventManager.FireEvent(new ResetEvent() {});
    }

	public List<CharacterController> GetColorList(GGJ2017GameManager.SURFBOARDCOLOR color)
	{
		switch (color)
		{
			case GGJ2017GameManager.SURFBOARDCOLOR.RED:		return RedCharacters;
			case GGJ2017GameManager.SURFBOARDCOLOR.GREEN:	return GreenCharacters; 
			case GGJ2017GameManager.SURFBOARDCOLOR.BLUE:	return BlueCharacters;
		}

		return null;
	}
	
	public void OnKillSurfer(KillSurferEvent eventData)
	{
		List<CharacterController> colorList = GetColorList(eventData.surfer.m_scPlayerColor);
		
		for (int i = eventData.surfer.m_iPlayerIndexForYourColor; i < colorList.Count; i++)
		{
			colorList[i].m_iPlayerIndexForYourColor--;
		}

		colorList.Remove(eventData.surfer);
		Characters.Remove(eventData.surfer);


		if (Characters.Count == 0)
		{
			SceneManager.LoadScene("GameOver");
		}

        eventData.surfer.Kill();
	}

	public void OnSpawnNewSurfer(SpawnNewSurferEvent eventData)
	{
		GameObject goCharacter = Instantiate(Resources.Load("Player")) as GameObject;
        //Destroy(goCharacter.GetComponent<Collider2D>());
		CharacterController cCharacterController = goCharacter.AddComponent<CharacterController>();
		cCharacterController.m_scPlayerColor = eventData.color;
        goCharacter.GetComponent<SpriteRenderer>().color = GGJ2017GameManager.m_dSurfboardColorToColor[cCharacterController.m_scPlayerColor];
        Characters.Add(cCharacterController);

		switch (eventData.color)
		{
			case GGJ2017GameManager.SURFBOARDCOLOR.RED:
				cCharacterController.PositionCharacter(m_SpawningNodeRed);
				if (RedCharacters.Count  > 0)
				{
					cCharacterController.SetFollowTarget(RedCharacters[RedCharacters.Count-1]);
				}
				
				cCharacterController.m_iPlayerIndexForYourColor = (RedCharacters.Count);
				RedCharacters.Add(cCharacterController);
				cCharacterController.CreateCharacterController(RED_BUTTON, RedCharacters.Count);
				break;
			case GGJ2017GameManager.SURFBOARDCOLOR.GREEN:
				cCharacterController.PositionCharacter(m_SpawningNodeGreen);
				if (GreenCharacters.Count > 0)
				{
					cCharacterController.SetFollowTarget(GreenCharacters[GreenCharacters.Count - 1]);
				}

				cCharacterController.m_iPlayerIndexForYourColor = (GreenCharacters.Count);
				GreenCharacters.Add(cCharacterController);
				cCharacterController.CreateCharacterController(GREEN_BUTTON, GreenCharacters.Count);
				break;
			case GGJ2017GameManager.SURFBOARDCOLOR.BLUE:
				cCharacterController.PositionCharacter(m_SpawningNodeBlue);
				if (BlueCharacters.Count > 0)
				{
					cCharacterController.SetFollowTarget(BlueCharacters[BlueCharacters.Count - 1]);
				}

				cCharacterController.m_iPlayerIndexForYourColor = (BlueCharacters.Count);
				BlueCharacters.Add(cCharacterController);
				cCharacterController.CreateCharacterController(BLUE_BUTTON, BlueCharacters.Count);
				break;
            default:
				break;
		}

        CTEventManager.FireEvent(new ReEvaluateSurferEvent() { surfer = goCharacter, add = true });
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

			if (eventData.color == character.m_scPlayerColor)
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
