using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Input Control
//Collision control
public class CharacterController : MonoBehaviour
{
    private float JUMP_TIME_LIMIT = 1f;
	private float DEATH_TIMER = 2;

	private GameObject MAIN_LOCATION;
	private GameObject JUMP_LOCATION;

    private GameObject particle;
    private KeyCode m_kcJumpKey = KeyCode.Alpha0;
    private int m_iInstance;

    private float m_fTime;
    private float m_fMoveSpeed = 1f;
    private float m_fJumpTimer;
    private float m_fWaveAmp = 2f;
    private float m_fWaveFrequency = 2f; //Speed limit is 15 freq is 5

    private GameObject m_goTargetPosition;

	public int m_iPlayerIndexForYourColor = 0;
    public bool m_bJumping = false;
    private bool m_bResting = true;

	public bool m_bYielding = false;

	public bool m_bIsDead = false;

	public GGJ2017GameManager.SURFBOARDCOLOR m_scPlayerColor;    private Color m_cPlayerColor;
	public CharacterController m_oFollowTarget;

    public void CreateCharacterController(KeyCode jumpIn, int instanceIn)
    {
        particle = Instantiate(Resources.Load("Particle")) as GameObject;
        particle.gameObject.transform.parent = gameObject.transform;

        m_kcJumpKey = jumpIn;
        m_iInstance = instanceIn;

        m_cPlayerColor = GGJ2017GameManager.m_dSurfboardColorToColor[m_scPlayerColor];

        particle.GetComponent<ParticleSystem>().startColor = m_cPlayerColor;
        particle.GetComponent<ParticleSystem>().enableEmission = false;
    }

	public void PositionCharacter(GameObject destinationNode)
    {
		MAIN_LOCATION = destinationNode;
		m_goTargetPosition = MAIN_LOCATION;
    }
	// Use this for initialization
	void Awake ()
    {
		MAIN_LOCATION = Game.game.m_SpawningNodeBlue;
		JUMP_LOCATION = Game.game.m_oJumpNode;
	}
	
    void FixedUpdate()
    {

    }

	public void Kill()
	{
		m_bIsDead = true;
		StartCoroutine(DieInXSeconds(DEATH_TIMER));
	}

	private IEnumerator DieInXSeconds(float time)
	{
		yield return new WaitForSeconds(time);

        CTEventManager.FireEvent(new ReEvaluateSurferEvent() { surfer = gameObject, add = false });
        Destroy(gameObject);
	}

	// Update is called once per frame
	void Update ()
    {
		if (m_bYielding)
			return;
        //Debug.Log("CharacterController: Update:");
        if(m_bJumping)
        {
            if(!particle.GetComponent<ParticleSystem>().emission.enabled)
            {
                particle.GetComponent<ParticleSystem>().enableEmission = true;
            }
            ParticleMotion();
        }
        else if (!m_bJumping && particle.GetComponent<ParticleSystem>().emission.enabled)
        {
            particle.GetComponent<ParticleSystem>().enableEmission = false;
        }


		//only the first dude for each Color Can Send a jump event
		if (m_iPlayerIndexForYourColor == 0 && Input.GetKeyDown(m_kcJumpKey) && m_bJumping == false)
		{
			if(Input.GetKey(KeyCode.LeftShift))
			{
				StartWaving();
			}
			else
			{
				Debug.Log("CharacterController: Update: firing jump event");
			CTEventManager.FireEvent(new JumpEvent() { color = m_scPlayerColor });
			}
		}


		if (m_goTargetPosition != null)
		{
			Vector3 AutoMovePosition = m_goTargetPosition.transform.position;
			if (m_oFollowTarget != null)
			{
				AutoMovePosition = m_goTargetPosition.transform.position + Vector3.left * 5 * m_iPlayerIndexForYourColor; //be X guys behind the jump point
			}

			if (transform.position != AutoMovePosition)
			{
				transform.position = Vector3.MoveTowards(transform.position, AutoMovePosition, m_fMoveSpeed);
			}
			else if (m_goTargetPosition == MAIN_LOCATION)
			{
				m_bJumping = false;
			}
		}

		if (m_bJumping)
		{
			m_fJumpTimer += Time.deltaTime;
            //Debug.Log("CharacterController: Update: m_fJumpTimer: " + m_fJumpTimer);
			if (m_fJumpTimer > JUMP_TIME_LIMIT)
			{
                //Debug.Log("CharacterController: Update: reset timer");
				GoToLane(false);
			}
		}

	}

	public void OnJump()
	{
		m_bYielding = false;
		GoToLane(true);
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        
    }

    void OnTriggerStay2D(Collider2D col)
    {

    }

    void OnTriggerExit2D(Collider2D col)
    {

    }

    private void GoToLane(bool bLane)
    {
        switch (bLane)
        {
            default:
            case false:
				m_fJumpTimer = 0;
				m_goTargetPosition = MAIN_LOCATION;
                break;
            case true:
                m_bJumping = true;
				m_fJumpTimer = 0;
				m_goTargetPosition = JUMP_LOCATION;
                break;
        }
    }

    private void ParticleMotion()
    {
        m_fTime += Time.deltaTime;
        particle.transform.position = new Vector3(transform.position.x, transform.position.y + m_fWaveAmp * Mathf.Sin((m_fTime % 1) * (m_fWaveFrequency * 2) * Mathf.PI), transform.position.z);
    }

	private bool m_bWaving = false;
	public void StartWaving()
	{
		m_bWaving = true;
		StartCoroutine(StopWavingInXSeconds(2));
	}

	private IEnumerator StopWavingInXSeconds(float time)
	{
		yield return new WaitForSeconds(time);
		m_bWaving = false;
	}

	public bool IsWaving()
	{
		return m_bWaving;
	}

	public bool IsJumping()
	{
		return m_bJumping;
	}

	public Vector2 GetVelocity()
	{
		return Vector2.right * 100;
	}

    public GGJ2017GameManager.SURFBOARDCOLOR GetColor()
    {
        //Debug.Log("CharacterController: GetColor: " + m_scPlayerColor);
        return m_scPlayerColor;
    }
	
	public void SetFollowTarget(CharacterController followTarget)
	{
		m_oFollowTarget = followTarget;
		m_goTargetPosition = MAIN_LOCATION;
	}


    /*
    public void CreateParticles()
    {
        int repeat = 1;
        int frequency = 2;//repeatrate
        float amp = .25f;//repeatrate
        float resolution = 10;//the pointamount used to create the wave

        ParticleSystem.VelocityOverLifetimeModule vel = particle.GetComponent<ParticleSystem>().velocityOverLifetime;
        vel.enabled = true;
        vel.space = ParticleSystemSimulationSpace.Local;

        AnimationCurve curveX = new AnimationCurve();

        Vector3 WaveValue = new Vector3(0,0,0);
        int points = 100 * repeat;
        for (int i = 0; i < points; i++)
        {
            float newtime = (i / (resolution - 1));
            WaveValue.x = (amp * Mathf.Sin(newtime * (frequency * 2) * Mathf.PI));
            WaveValue = WaveValue.normalized * amp;

            curveX.AddKey(newtime, WaveValue.x);
        }

        vel.x = new ParticleSystem.MinMaxCurve(10.0f, curveX);
    }//*/
}
