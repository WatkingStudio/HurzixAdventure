using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCharacterController : CharacterController2D
{
	[SerializeField] private Animator m_Animator;

	private new void Awake()
	{
		base.Awake();

		//OnLandEvent.AddListener(OnLand);
	}
	//This function will be called when the player jumps
	private void OnJump()
	{

	}

	//This function will be called when the player jumps
	private void OnLand()
	{
		m_Animator.Play("Idle");
	}
}
