using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	//The controller for the player
	public CharacterController2D controller;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;

	Animator animator;

	private void Start()
	{
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
    {
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		if(Input.GetButtonDown("Jump"))
		{
			jump = true;
			animator.Play("Jump");
		}

		if(Input.GetButtonDown("Crouch"))
		{
			Debug.Log("Crouch");
			animator.Play("2To4");
			crouch = true;
		}
		else if(Input.GetButtonUp("Crouch"))
		{
			Debug.Log("UnCrouch");
			animator.Play("4To2");
			crouch = false;
		}
	}

	//Use for Physics
	private void FixedUpdate()
	{
		//Move character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}
}
