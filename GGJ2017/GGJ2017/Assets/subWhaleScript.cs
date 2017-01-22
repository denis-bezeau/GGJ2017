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
	}


	public void OnCastingFinish()
	{
		Debug.Log("OnCastingFinish");
		m_AnimatorWhale1.SetBool("IsCasting", false);
	}

	public void OnBeamingFinish()
	{
		Debug.Log("OnBeamingFinish");
		m_AnimatorWhale1.SetBool("IsChargingUp", false);
	}

	public void OnCastSpells1()
	{
		Debug.Log("OnCastSpells1");
		GameObject.Instantiate(m_oProjectilePrefab, m_oProjectileStartLocation.transform);
	}

	public void OnCastSpells2()
	{
		Debug.Log("OnCastSpells2");
		GameObject.Instantiate(m_oProjectilePrefab, m_oProjectileStartLocation.transform);
	}

	public void OnCastSpells3()
	{
		Debug.Log("OnCastSpells3");
		GameObject.Instantiate(m_oProjectilePrefab, m_oProjectileStartLocation.transform);
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
		GameObject.Instantiate(m_oBeamPrefab, m_oProjectileStartLocation.transform);
	}
}
