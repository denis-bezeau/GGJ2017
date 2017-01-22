using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private string PLAYER_ATTACK_TAG = "pAttack";

    private const int MAX_HITS_V1 = 3;
    private const int MAX_HITS_V2 = 3;
    private const int BOSS_MODE_V1 = 0;
    private const int BOSS_MODE_V2 = 1;

    private static int ATTACK_RAM = 0;
    private static int ATTACK_PROJECTILE = 1;
    private static int ATTACK_LAZER = 2;

    public GameObject m_goMode1;
    public GameObject m_goMode2;
    private int m_iMode = BOSS_MODE_V1;
    private int m_iCurrentLife = MAX_HITS_V1;
    private int m_iAttackMode;

	public bool m_bAttackPlayer;
	public bool m_bTakeAHit;

	private bool m_bAttacking = false;

	private Animator m_AnimatorWhale1;
	private Animator m_AnimatorWhale2;

	private float m_fInvinvibleTimer = 0.0f;
	private float INVINCIBLE_TIME = 1.0f;

	private float m_fAttackTimer = 0.0f;

    // Use this for initialization
    void Start ()
    {
		m_AnimatorWhale1 = m_goMode1.GetComponent<Animator>();
		m_AnimatorWhale2 = m_goMode2.GetComponent<Animator>();

		if (m_iMode == BOSS_MODE_V1)
		{
			m_goMode1.SetActive(true);
			m_goMode2.SetActive(false);
		}
		else if (m_iMode == BOSS_MODE_V2)
		{
			m_goMode1.SetActive(false);
			m_goMode2.SetActive(true);
		}
	}
	
	// Update is called once per frame
	void Update ()
    {
		m_fInvinvibleTimer -= Time.deltaTime;
		if (m_fInvinvibleTimer < 0.0f)
		{
			m_fInvinvibleTimer = 0.0f;

			if (m_iMode == BOSS_MODE_V1)
			{
				m_goMode1.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			}
			else if (m_iMode == BOSS_MODE_V2)
			{
				m_goMode2.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			}
		}
		else
		{
			if (m_iMode == BOSS_MODE_V1)
			{
				m_goMode1.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
			}
			else if (m_iMode == BOSS_MODE_V2)
			{
				m_goMode2.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
			}
		}

		if (m_fInvinvibleTimer == 0.0f)
		{
			m_fAttackTimer -= Time.deltaTime;
			if (m_fAttackTimer < 0.0f)
			{
				m_fAttackTimer = 0.0f;
				AttackPlayer();
			}
		}

		if (m_bAttackPlayer)
		{
			AttackPlayer();
			m_bAttackPlayer = false;
		}

		if (m_bTakeAHit)
		{
			TakeAHit();
			m_bTakeAHit = false;
		}
	}


    private void TakeAHit()
    {
		if (m_fInvinvibleTimer > 0 || m_iCurrentLife < 0)
		{
			Debug.Log("Boss::TakeAHit() early return");
			return;
		}
		--m_iCurrentLife;
		Debug.Log("Boss::TakeAHit() new life total == " + m_iCurrentLife);
		m_fInvinvibleTimer = INVINCIBLE_TIME;
		//set invincibility timer

        if(m_iCurrentLife < 1)
        {
            switch (m_iMode)
            {
                default:
					Debug.Log("Boss::TakeAHit() default do nothing");
                    break;
                case BOSS_MODE_V1:
					Debug.Log("Boss::TakeAHit() play transition out anim");
					m_AnimatorWhale1.SetTrigger("TransitionOut");
                    break;
                case BOSS_MODE_V2:
					m_AnimatorWhale2.SetTrigger("Die");
                    break;
            }
        }
    }

	public void OnWhale1TransitionOutComplete()
	{
		Debug.Log("Boss::OnWhale1TransitionOutComplete()"); 
		m_goMode1.SetActive(false);
		m_goMode2.SetActive(true);
		m_iMode = BOSS_MODE_V2;
		m_iCurrentLife = MAX_HITS_V2;
		m_AnimatorWhale2.SetTrigger("TransformIn");
	}

    private void AttackPlayer()
    {
		m_bAttacking = true;
		
        switch (m_iMode)
        {
            default:
                break;
            case BOSS_MODE_V1:
                m_iAttackMode = ATTACK_RAM;
				m_AnimatorWhale1.SetBool("IsRamming", true);
				m_fInvinvibleTimer = INVINCIBLE_TIME;
                break;
            case BOSS_MODE_V2:
                m_iAttackMode = Random.Range(ATTACK_RAM, ATTACK_LAZER);
				if (m_iAttackMode == ATTACK_RAM)
				{
					m_AnimatorWhale2.SetBool("IsCasting", true);
				}
				else
				{
					m_AnimatorWhale2.SetBool("IsChargingUp", true);
				}
                break;
        }
    }

	public void OnAttackFinished()
	{
		m_fAttackTimer = Random.Range(1.0f, 3.0f);
		m_bAttacking = false;
	}



    void OnTriggerEnter2D(Collider2D col)
    {
		CharacterController character = col.gameObject.GetComponentInParent<CharacterController>();
		if (character)
		{
			if (character.IsAttacking() && m_fInvinvibleTimer <= 0 && m_iCurrentLife > 0)
			{
				Debug.Log("OnTriggerEnter2D() take a hit");
				TakeAHit();
			}
		}
    }

    void OnTriggerStay2D(Collider2D col)
    {
		CharacterController character = col.gameObject.GetComponentInParent<CharacterController>();
		if (character)
        {
			if (character.IsAttacking() && m_fInvinvibleTimer <= 0 && m_iCurrentLife >0)
			{
				Debug.Log("Boss::OnTriggerStay2D() take a hit");
				TakeAHit();
			}
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == PLAYER_ATTACK_TAG)
        {
			Debug.Log("Boss::TRIGGERED O_O Exit");
        }
    }
}
