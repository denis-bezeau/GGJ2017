using UnityEngine;

//Input Control
//Collision control
public class CharacterController : MonoBehaviour
{
    private GameObject particle;

    private KeyCode jump = KeyCode.Alpha0;
    private int instance;

    private float waveTime;
    private float waveAmp = .25f;
    private float waveFrequency = 1f;
	private bool m_bJumping = false;

    private static Vector2 CHARACTER_ORIGIN_POSITION = new Vector2(-20, 0);

    public void CreateCharacterController(KeyCode jumpIn, int instanceIn)
    {
        particle = Instantiate(Resources.Load("Particle")) as GameObject;
        particle.gameObject.transform.parent = gameObject.transform;

        //CreateParticles();

        jump = jumpIn;
        instance = instanceIn;
    }

    public void PositionCharacter(int totalCharacters)
    {
        if(totalCharacters == 1) //Hack
        {
            this.gameObject.transform.position = CHARACTER_ORIGIN_POSITION;
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
        ParticleMotion();

        if (jump != KeyCode.Alpha0 && Input.GetKeyDown(jump))
        {
            Debug.Log("CharacterController: Update: "+ instance +": jump");
			m_bJumping = true;
        }
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

    private void ParticleMotion()
    {
        waveTime += Time.deltaTime;
        particle.transform.position = new Vector3(particle.transform.position.x, particle.transform.position.y + waveAmp * Mathf.Sin((waveTime % 1) * (waveFrequency * 2) * Mathf.PI), particle.transform.position.z);
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
