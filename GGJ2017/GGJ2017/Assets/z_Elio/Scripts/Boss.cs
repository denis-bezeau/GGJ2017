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
    private const int BOSS_MODE_V3 = 2;

    private static int ATTACK_RAM = 0;
    private static int ATTACK_PROJECTILE = 1;
    private static int ATTACK_LAZER = 2;

    public GameObject m_goMode1;
    public GameObject m_goMode2;
    public GameObject m_goMode3;
    public int m_iMode = BOSS_MODE_V1;
    private int m_iCurrentLife = MAX_HITS_V1;
    private int m_iAttackMode;

	public bool m_bUseRamAttack;
	public bool m_bUseSpellAttack;

	private Animator m_AnimatorWhale1;
	private Animator m_AnimatorWhale2;

    // Use this for initialization
    void Start ()
    {
		m_AnimatorWhale1 = m_goMode1.GetComponent<Animator>();
		m_AnimatorWhale2 = m_goMode2.GetComponent<Animator>();

		if (m_iMode == BOSS_MODE_V1)
		{
			m_goMode1.SetActive(true);
			m_goMode2.SetActive(false);
			m_goMode3.SetActive(false);
		}
		else if (m_iMode == BOSS_MODE_V2)
		{
			m_goMode1.SetActive(false);
			m_goMode2.SetActive(true);
			m_goMode3.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (m_bUseRamAttack)
		{
			AttackPlayer();
			m_bUseRamAttack = false;
		}

		if (m_bUseSpellAttack)
		{
			AttackPlayer();
			m_bUseSpellAttack = false;
		}
	}

    private void TakeAHit()
    {
        --m_iCurrentLife;

        if(m_iCurrentLife < 1)
        {
            switch (m_iMode)
            {
                default:
                    break;
                case BOSS_MODE_V1:
					m_AnimatorWhale1.SetTrigger("TransitionOut");
                    break;
                case BOSS_MODE_V2:
                    m_iMode = BOSS_MODE_V3; //Dead mode
                    m_goMode3.SetActive(true);
                    break;
            }
        }
    }

	public void OnWhale1TransitionOutComplete()
	{
		m_goMode1.SetActive(false);
		m_goMode2.SetActive(true);
		m_iMode = BOSS_MODE_V2;
		m_AnimatorWhale2.SetTrigger("TransformIn");
	}

    private void AttackPlayer()
    {
        switch (m_iMode)
        {
            default:
                break;
            case BOSS_MODE_V1:
                m_iAttackMode = ATTACK_RAM;
				m_AnimatorWhale1.SetBool("IsRamming", true);
				
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



    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("TRIGGERED O_O");
        if (col.gameObject.tag == PLAYER_ATTACK_TAG)
        {
            //Take a hit
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == PLAYER_ATTACK_TAG)
        {
            Debug.Log("TRIGGERED O_O Stay");
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == PLAYER_ATTACK_TAG)
        {
            Debug.Log("TRIGGERED O_O Exit");
        }
    }
}
