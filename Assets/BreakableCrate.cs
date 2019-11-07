using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		yield return new WaitForSeconds(1);
		gameObject.SetActive(false);
	}
}
