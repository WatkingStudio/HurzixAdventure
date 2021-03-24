using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class HealthIcon
 * 
 * \brief This class holds the functionality for a health icon.
 * 
 * \date 2019/21/10
 */ 
public class HealthIcon : MonoBehaviour
{
	[SerializeField]
	private Animator m_Animator;

	private bool m_IsActive = true;

	private void Start()
	{
		if (!m_Animator)
		{
			Debug.LogError("No Animator has been assigned to " + gameObject.name);
		}
	}

	// Get This Health Icons Animator.
	// @return The Animator of this Health Icon.
	public Animator GetAnimator()
	{
		return m_Animator;
	}

	// Set the Health Icon to Active.
	public void GainHealth()
	{
		m_Animator.SetBool("Active", true);
		m_Animator.SetBool("InActive", false);
	}

	// Check if this Health Icon is Active.
	// @return True if the Health Icon is Active, False if Not.
	public bool IsActive()
	{
		return m_IsActive;
	}

	// Deactivate the Health Icon.
	public void TakeDamage()
	{
		m_Animator.SetBool("Active", false);
		m_Animator.SetBool("InActive", true);
	}
}
