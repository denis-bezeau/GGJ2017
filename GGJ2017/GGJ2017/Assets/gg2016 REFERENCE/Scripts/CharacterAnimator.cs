using UnityEngine;
using System.Collections;

public class CharacterAnimator : MonoBehaviour 
{
    private Animator animator;
    private Rigidbody2D rigidBody;
	private CharacterControllerScript characterController;

    // Use this for initialization
    void Start ()
    {
		characterController = GetComponent<CharacterControllerScript>();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		Debug.Log("CHARACTERANIMATOR::UPDATING");
		animator.SetBool("crouching", characterController.IsCrouching());
        animator.SetFloat("MomentumX", rigidBody.velocity.x);
        animator.SetFloat("MomentumY", rigidBody.velocity.y);
        animator.SetBool("Jumping", characterController.IsJumping());
    }
}
