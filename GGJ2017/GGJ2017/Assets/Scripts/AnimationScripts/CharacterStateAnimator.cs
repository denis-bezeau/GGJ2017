using UnityEngine;
using System.Collections;

public class CharacterStateAnimator : MonoBehaviour 
{
    private Animator animator;
    
	private CharacterController characterController;

    // Use this for initialization
    void Start ()
    {
		characterController = GetComponent<CharacterController>();
		if (characterController == null)
		{
			characterController = GetComponentInParent<CharacterController>();
		}

        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        animator.SetBool("Jumping", characterController.IsJumping());
		animator.SetBool("IsWaving", characterController.IsWaving());
		animator.SetBool("IsAttacking", characterController.IsAttacking());
		animator.SetBool("IsDead", characterController.m_bIsDead);
    }

	void OnAttackFinish()
	{
		characterController.StopAttacking();
	}


	void OnTriggerStay2D(Collider2D col)
	{
		//Debug.Log("CharacterStateAnim::OnTriggerStay2D() " + name + " -> " + col.name);

	}
}
