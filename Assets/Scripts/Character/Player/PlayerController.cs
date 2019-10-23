using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 
 * \class PlayerController
 * 
 * \brief This class is used to take the input of the player and apply it to the character
 * 
 * \date 2019/15/10
 * 
 */
public class PlayerController : MonoBehaviour
{
	//The controller for the player
	[SerializeField]
	private CharacterMovement2D m_Movement;
	[SerializeField] 
	private LayerMask m_WhatIsGround;
	[SerializeField] 
	private Transform m_CeilingCheck;

	[SerializeField]
	private float m_RunSpeed = 40f;

	private float m_HorizontalMove = 0f;
	private bool m_Jump = false;
	private bool m_Crouch = false;
	private bool m_Sprint = false;
	private bool m_DisableMovement = false;

	[SerializeField]
	private PlayerAnimations m_Animator;
	[SerializeField]
	private PlayerCharacter m_PlayerCharacter;
	[SerializeField]
	private LevelItems m_CurrentLevelItems;

	public void DisableMovement()
	{
		m_DisableMovement = true;
		m_Movement.ResetVelocity();
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
			m_Animator.PlayerSprinting(true);
			m_Sprint = true;
		}
		else if(Input.GetButtonUp("Sprint"))
		{
			m_Animator.PlayerSprinting(false);
			m_Sprint = false;
		}

		if(Input.GetButtonDown("Attack"))
		{
			m_Animator.PlayerAttack();
		}

		if(Input.GetButtonDown("Jump"))
		{
			m_Jump = true;
			m_Animator.PlayerJumping(true);
		}

		m_Animator.PlayerSpeed(m_HorizontalMove);

		if (Input.GetButtonDown("Crouch"))
		{
			//Crouch
			if(!m_Crouch)
			{
				m_Animator.PlayerCrouching(true);
				m_Crouch = true;
			}
			//Stand Up
			else
			{
				if(!Physics2D.OverlapCircle(m_CeilingCheck.position, .2f, m_WhatIsGround))
				{
					m_Animator.PlayerCrouching(false);
					m_Crouch = false;
				}			
			}
		}

		if(Input.GetButtonDown("Reveal"))
		{
			//Reveal the closest item
			m_CurrentLevelItems.RevealClosestItem(transform);
		}

		if(Input.GetButtonDown("Interact"))
		{
			m_PlayerCharacter.InteractWithObject();
		}

		if(Input.GetButtonDown("ToggleControls"))
		{
			m_PlayerCharacter.ToggleControls();
		}
	}

	//This function is used to apply any code to the player when they land
	public void OnLanding()
	{
		m_Animator.PlayerJumping(false);
	}

	public void StartFalling()
	{
		m_Animator.PlayerFalling(true);
		m_Animator.PlayerJumping(false);
	}

	public void StopFalling()
	{
		m_Animator.PlayerFalling(false);
	}

	//Use for Physics
	private void FixedUpdate()
	{
		//Move character
		m_Movement.Move(m_HorizontalMove * Time.fixedDeltaTime, m_Crouch, m_Jump, m_Sprint);
		m_Jump = false;
	}
}
