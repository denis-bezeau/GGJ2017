using UnityEngine;
using System.Collections;

public class CharacterStateAnimator : MonoBehaviour 
{
    private Animator animator;
    
	public CharacterControllerScript characterController;

    // Use this for initialization
    void Start ()
    {
		if (characterController == null)
		characterController = GetComponentInParent<CharacterControllerScript>();
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		Debug.Log("CHARACTERANIMATOR::UPDATING animator=" + animator);

		Debug.Log("CHARACTERANIMATOR::UPDATING characterController=" + characterController); 
		animator.SetBool("crouching", characterController.IsCrouching());
		animator.SetFloat("MomentumX", characterController.GetVelocity().x);
		animator.SetFloat("MomentumY", characterController.GetVelocity().y);
        animator.SetBool("Jumping", characterController.IsJumping());
    }
}
