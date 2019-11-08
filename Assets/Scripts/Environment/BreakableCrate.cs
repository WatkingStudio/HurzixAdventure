﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class BreakableCrate
 * 
 * \brief This class holds the functionality for a breakable crate.
 * 
 * \date 2019/08/11
 * 
 */
public class BreakableCrate : MonoBehaviour
{
	[SerializeField]
	private Animator m_Animator;
	[SerializeField]
	private Damageable m_Damageable;
	[SerializeField]
	private int m_CrateHealth = 3;
	[SerializeField]
	private Collider2D m_Collider2D;
	[SerializeField]
	private float m_DespawnTimer = 1f;

	private void Start()
	{
		m_Damageable.ResetHealth(m_CrateHealth);
	}

	public void CrateDamaged()
	{
		m_Animator.SetInteger("Health", m_Damageable.CurrentHealth());
	}

	public void CrateBroken()
	{
		m_Collider2D.enabled = false;
		StartCoroutine(DespawnCrate());
	}

	IEnumerator DespawnCrate()
	{
		yield return new WaitForSeconds(m_DespawnTimer);
		gameObject.SetActive(false);
	}
}
