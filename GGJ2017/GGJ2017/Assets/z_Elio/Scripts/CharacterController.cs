using UnityEngine;

//Input Control
//Collision control
public class CharacterController : MonoBehaviour
{
    private float JUMP_TIME_LIMIT = 1f;

    private string MAIN_LOCATION = "Spawn";
    private string JUMP_LOCATION = "OutOfWay";

    private GameObject particle;
    private KeyCode m_kcJumpKey = KeyCode.Alpha0;
    private int m_iInstance;

    private float m_fTime;
    private float m_fMoveSpeed = 1f;
    private float m_fJumpTimer;
    private float m_fWaveAmp = 2f;
    private float m_fWaveFrequency = 2f; //Speed limit is 15 freq is 5

    private GameObject m_goTargetPosition;

    private bool m_bJumping = false;
    private bool m_bResting = true;

    private Color m_cPlayerColor;

    public void CreateCharacterController(KeyCode jumpIn, int instanceIn)
    {
        particle = Instantiate(Resources.Load("Particle")) as GameObject;
        particle.gameObject.transform.parent = gameObject.transform;

        m_kcJumpKey = jumpIn;
        m_iInstance = instanceIn;

        m_cPlayerColor = Color.green;

        particle.GetComponent<ParticleSystem>().startColor = m_cPlayerColor;
        particle.GetComponent<ParticleSystem>().enableEmission = false;
    }

    public void PositionCharacter(int totalCharacters)
    {
        if(totalCharacters == 1) //Hack
        {
            m_goTargetPosition = GameObject.Find("Spawn");
            this.gameObject.transform.position = m_goTargetPosition.transform.position;
        }
    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
    void FixedUpdate()
    {

    }

	// Update is called once per frame
	void Update ()
    {
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

        if (m_kcJumpKey != KeyCode.Alpha0)
        {
            //pressing button
            bool bJumpInput = Input.GetKey(m_kcJumpKey);

            //At apex of jump
            if (m_bJumping)
            {
                if(m_bResting)
                {
                    m_fJumpTimer += Time.deltaTime;
                    //Debug.Log("CharacterController: Update: m_fJumpTimer: " + m_fJumpTimer);
                }

                //Met limit of time in air
                if (m_fJumpTimer > JUMP_TIME_LIMIT)
                {
                    //Debug.Log("CharacterController: Update: reset timer");
                    bJumpInput = false;
                }
            }

            GoToLane(bJumpInput);
        }

        if (transform.position != m_goTargetPosition.transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_goTargetPosition.transform.position, m_fMoveSpeed);
            m_bResting = false;
        }
        else if(!m_bResting)
        {
            m_bResting = true;
        }

        if (m_bJumping && m_bResting)
        {
            m_fJumpTimer += Time.deltaTime;
            //Debug.Log("CharacterController: Update: m_fJumpTimer: " + m_fJumpTimer);
            if (m_fJumpTimer > JUMP_TIME_LIMIT)
            {
                //Debug.Log("CharacterController: Update: reset timer");
                GoToLane(!m_bJumping);
            }
        }
	}

    private void GoToLane(bool bLane)
    {
        string sLoc = "";
        switch (bLane)
        {
            default:
            case false:
                sLoc = MAIN_LOCATION;
                break;
            case true:
                sLoc = JUMP_LOCATION;
                m_bJumping = true;
                break;
        }
        if (m_bResting)
        {
            if(GameObject.Find(sLoc) != m_goTargetPosition)
            {
                m_goTargetPosition = GameObject.Find(sLoc);
            }

            if(!bLane && GameObject.Find(sLoc).transform.position == gameObject.transform.position)
            {
                m_bJumping = false;
                m_fJumpTimer = 0;
            }
        }
    }

    private void ParticleMotion()
    {
        m_fTime += Time.deltaTime;
        particle.transform.position = new Vector3(transform.position.x, transform.position.y + m_fWaveAmp * Mathf.Sin((m_fTime % 1) * (m_fWaveFrequency * 2) * Mathf.PI), transform.position.z);
    }

	public bool IsCrouching()
	{
		return false;// m_bCrouching;
	}

	public bool IsJumping()
	{
		return m_bJumping;
	}

	public Vector2 GetVelocity()
	{
		//return rigidBody.velocity;
		return Vector2.right * 100;
	}

    public Color GetColor()
    {
        Debug.Log("CharacterController: GetColor: " + m_cPlayerColor);
        return m_cPlayerColor;
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
