using UnityEngine;
using System.Collections;

public class CharacterStateAnimator : MonoBehaviour 
{
    private Animator animator;
    
	public CharacterController characterController;

    // Use this for initialization
    void Start ()
    {
		if (characterController == null)
			characterController = GetComponentInParent<CharacterController>();
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		animator.SetFloat("MomentumX", characterController.GetVelocity().x);
		animator.SetFloat("MomentumY", characterController.GetVelocity().y);
        animator.SetBool("Jumping", characterController.IsJumping());
		animator.SetBool("crouching", characterController.IsWaving());
		//animator.SetBool("IsDead", characterController.m_bIsDead);
    }
}
