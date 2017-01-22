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
    private int m_iMode = BOSS_MODE_V1;
    private int m_iCurrentLife = MAX_HITS_V1;
    private int m_iAttackMode;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void TakeAHit()
    {
        --m_iCurrentLife;

        if(m_iCurrentLife < 1)
        {
            m_goMode1.SetActive(false);
            m_goMode2.SetActive(false);
            m_goMode3.SetActive(false);

            switch (m_iMode)
            {
                default:
                    break;
                case BOSS_MODE_V1:
                    m_iMode = BOSS_MODE_V2;
                    m_goMode2.SetActive(true);
                    break;
                case BOSS_MODE_V2:
                    m_iMode = BOSS_MODE_V3; //Dead mode
                    m_goMode3.SetActive(true);
                    break;
            }
        }
    }

    private void AttackPlayer()
    {
        switch (m_iMode)
        {
            default:
                break;
            case BOSS_MODE_V1:
                m_iAttackMode = ATTACK_RAM;
                break;
            case BOSS_MODE_V2:
                m_iAttackMode = Random.Range(ATTACK_RAM, ATTACK_LAZER);
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
