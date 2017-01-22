using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class subWhaleScript : MonoBehaviour {

	private Animator m_AnimatorWhale1;

	public GameObject m_oProjectilePrefab;
	public GameObject m_oBeamPrefab;
	public GameObject m_oProjectileStartLocation;

	public Boss m_oBossScript;

	// Use this for initialization
	void Start()
	{
		m_AnimatorWhale1 = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnRamFinish()
	{
		Debug.Log("OnRamFinish");
		m_AnimatorWhale1.SetBool("IsRamming", false);
		m_oBossScript.OnAttackFinished();
	}


	public void OnCastingFinish()
	{
		Debug.Log("OnCastingFinish");
		m_AnimatorWhale1.SetBool("IsCasting", false);
		m_oBossScript.OnAttackFinished();
	}

	public void OnBeamingFinish()
	{
		Debug.Log("OnBeamingFinish");
		m_AnimatorWhale1.SetBool("IsChargingUp", false);
		m_oBossScript.OnAttackFinished();
	}

	public void OnCastSpells1()
	{
		Debug.Log("OnCastSpells1");
		GameObject newGO = GameObject.Instantiate(m_oProjectilePrefab, m_oProjectileStartLocation.transform);
		Meteor meteor = newGO.GetComponent<Meteor>();
		if (meteor)
		{
			meteor.color = (GGJ2017GameManager.SURFBOARDCOLOR)Random.Range(0, 3);
			meteor.direction = (1 + Random.Range(0, 5)) * Vector3.left + Vector3.down * Random.Range(0, 4);
		}
	}

	public void OnCastSpells2()
	{
		Debug.Log("OnCastSpells2");
		GameObject newGO = GameObject.Instantiate(m_oProjectilePrefab, m_oProjectileStartLocation.transform);
		Meteor meteor = newGO.GetComponent<Meteor>();
		if (meteor)
		{
			meteor.color = (GGJ2017GameManager.SURFBOARDCOLOR)Random.Range(0, 3);
			meteor.direction = (1 + Random.Range(0, 6))*Vector3.left + Vector3.down * Random.Range(0, 4);
		}
	}

	public void OnCastSpells3()
	{
		Debug.Log("OnCastSpells3");
		GameObject newGO = GameObject.Instantiate(m_oProjectilePrefab, m_oProjectileStartLocation.transform);
		Meteor meteor = newGO.GetComponent<Meteor>();
		if (meteor)
		{
			meteor.color = (GGJ2017GameManager.SURFBOARDCOLOR)Random.Range(0, 3);
			meteor.direction = (1 + Random.Range(0, 6)) * Vector3.left + Vector3.down * Random.Range(0, 4);
		}
	}


	public void OnWhale1TransitionOutComplete()
	{
		Debug.Log("OnWhale1TransitionOutComplete");
		m_oBossScript.OnWhale1TransitionOutComplete();
	}

	public void OnWhat2TransitionInCommplete()
	{
		Debug.Log("OnWhat2TransitionInCommplete");
	}

	public void OnBeamAttack()
	{
		Debug.Log("OnCastSpells3");
		GameObject newGO = GameObject.Instantiate(m_oProjectilePrefab, m_oProjectileStartLocation.transform);
		Meteor meteor = newGO.GetComponent<Meteor>();
		if (meteor)
		{
			meteor.color = (GGJ2017GameManager.SURFBOARDCOLOR)Random.Range(0, 3);
			meteor.direction = (Random.Range(1, 7)) * Vector3.left + Vector3.down * (-2 + Random.Range(0, 6));
		}
	}


	void OnTriggerEnter2D(Collider2D collider)
	{
		Debug.Log("SurferUp::you triggered with " + collider.gameObject.name);
		CharacterController character = collider.gameObject.GetComponent<CharacterController>();
		if (character != null)
		{
			if (character.IsAttacking() == false && character.m_iPlayerIndexForYourColor == 0)
			{
				Debug.Log("DeadlyTerrain::you triggered with character color");
				CTEventManager.FireEvent(new KillSurferEvent() { surfer = character });
			}
		}
	}
}
