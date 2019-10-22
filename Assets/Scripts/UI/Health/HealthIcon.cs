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

	public bool IsActive()
	{
		return m_IsActive;
	}

	public Animator GetAnimator()
	{
		return m_Animator;
	}

	public void GainHealth()
	{
		m_Animator.SetBool("Active", true);
		m_Animator.SetBool("InActive", false);
	}

	public void TakeDamage()
	{
		m_Animator.SetBool("Active", false);
		m_Animator.SetBool("InActive", true);
	}
}
