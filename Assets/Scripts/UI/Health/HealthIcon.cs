using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		m_Animator.SetTrigger("Active");
	}

	public void TakeDamage()
	{
		m_Animator.SetTrigger("InActive");
	}
}
