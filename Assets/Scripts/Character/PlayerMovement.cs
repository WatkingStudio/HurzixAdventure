using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	//The controller for the player
	[SerializeField]
	private CharacterMovement2D m_Movement;
	[SerializeField] 
	private RuntimeAnimatorController m_StandingController;
	[SerializeField] 
	private RuntimeAnimatorController m_CrouchingController;
	[SerializeField] 
	private LayerMask m_WhatIsGround;
	[SerializeField] 
	private Transform m_CeilingCheck;
	[SerializeField]
	private Inventory m_Inventory;
	[SerializeField]
	private Rigidbody2D m_Rigidbody2D;

	[SerializeField] private float m_RunSpeed = 40f;

	private float m_HorizontalMove = 0f;
	private bool m_Jump = false;
	private bool m_Crouch = false;
	private bool m_Sprint = false;
	private bool m_DisableMovement = false;

	[SerializeField] private Animator m_Animator;

	public void DisableMovement()
	{
		m_DisableMovement = true;
		m_Rigidbody2D.velocity = Vector2.one;
	}

	public void EnableMovement()
	{
		m_DisableMovement = false;
	}

	// Update is called once per frame
	void Update()
	{
		//If Movement has been disabled do not allow player to move.
		if (m_DisableMovement)
		{
			m_HorizontalMove = 0;
			return;
		}

		m_HorizontalMove = Input.GetAxisRaw("Horizontal") * m_RunSpeed;
		
		if(Input.GetButtonDown("Sprint"))
		{
			m_Animator.SetBool("IsSprinting", true);
			m_Sprint = true;
		}
		else if(Input.GetButtonUp("Sprint"))
		{
			m_Animator.SetBool("IsSprinting", false);
			m_Sprint = false;
		}

		if(Input.GetButtonDown("Attack"))
		{
			m_Animator.SetTrigger("Attack");
		}

		if(Input.GetButtonDown("Jump"))
		{
			m_Jump = true;
			m_Animator.SetBool("IsJumping", true);
		}

		m_Animator.SetFloat("Speed", Mathf.Abs(m_HorizontalMove));

		if (Input.GetButtonDown("Crouch"))
		{
			//Crouch
			if(!m_Crouch)
			{
				m_Animator.SetBool("IsCrouching", true);
				m_Animator.runtimeAnimatorController = m_CrouchingController;
				m_Animator.SetBool("IsCrouching", true);
				m_Crouch = true;
			}
			//Stand Up
			else
			{
				if(!Physics2D.OverlapCircle(m_CeilingCheck.position, .2f, m_WhatIsGround))
				{
					m_Animator.SetBool("IsCrouching", false);
					m_Animator.runtimeAnimatorController = m_StandingController;
					m_Animator.SetBool("IsCrouching", false);
					m_Crouch = false;
				}			
			}
		}
	}

	//This function is used to apply any code to the player when they land
	public void OnLanding()
	{
		m_Animator.SetBool("IsJumping", false);
	}

	public void StartFalling()
	{
		m_Animator.SetBool("IsFalling", true);
		m_Animator.SetBool("IsJumping", false);
	}

	public void StopFalling()
	{
		m_Animator.SetBool("IsFalling", false);
	}

	//Use for Physics
	private void FixedUpdate()
	{
		//Move character
		m_Movement.Move(m_HorizontalMove * Time.fixedDeltaTime, m_Crouch, m_Jump, m_Sprint);
		m_Jump = false;
	}
}
