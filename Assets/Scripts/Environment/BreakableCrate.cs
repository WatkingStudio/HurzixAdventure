using System.Collections;
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
	private int m_CrateHealth = 3;
	[SerializeField]
	private float m_DespawnTimer = 1f;

	[Header("Self Variables")]
	[SerializeField]
	private Collider2D m_Collider2D;
	[SerializeField]
	private Animator m_Animator;
	[SerializeField]
	private Damageable m_Damageable;

	private void Start()
	{
		if (!m_Collider2D)
			Debug.LogError("No Collider2D has been assigned to " + gameObject.name);
		if (!m_Animator)
			Debug.LogError("No Animator has been assigned to " + gameObject.name);
		if (!m_Damageable)
			Debug.LogError("No Damageable has been assigned to " + gameObject.name);

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
