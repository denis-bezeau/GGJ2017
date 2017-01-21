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
		animator.SetBool("crouching", characterController.IsCrouching());
		animator.SetFloat("MomentumX", characterController.GetVelocity().x);
		animator.SetFloat("MomentumY", characterController.GetVelocity().y);
        animator.SetBool("Jumping", characterController.IsJumping());
    }
}
