using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	//The controller for the player
	public CharacterController2D controller;
	public RuntimeAnimatorController standingController;
	public RuntimeAnimatorController crouchingController;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;

	private Animator animator;

	private void Start()
	{
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
    {
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if(Input.GetButtonDown("Jump"))
		{
			jump = true;
			animator.SetBool("IsJumping", true);			
		}

		if(Input.GetButtonDown("Crouch"))
		{
			Debug.Log("Crouch");
			animator.SetBool("IsCrouching", true);
			animator.runtimeAnimatorController = crouchingController;
			animator.SetBool("IsCrouching", true);
			//animator.Play("2To4");
			crouch = true;
		}
		else if(Input.GetButtonUp("Crouch"))
		{
			Debug.Log("UnCrouch");
			animator.SetBool("IsCrouching", false);
			animator.runtimeAnimatorController = standingController;
			animator.SetBool("IsCrouching", false);
			//animator.Play("4To2");
			crouch = false;
		}
	}

	public void OnLanding()
	{
		animator.SetBool("IsJumping", false);
	}

	//Use for Physics
	private void FixedUpdate()
	{
		//Move character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}
}
