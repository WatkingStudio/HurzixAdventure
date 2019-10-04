using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
	[SerializeField]
	private Damageable m_Damageable;
	[SerializeField]
	private Animator m_Animator;
	[SerializeField]
	private Transform m_ActiveCheckpoint;
	[SerializeField]
	private Transform m_StartingPosition;
	[SerializeField]
	private Rigidbody2D m_RigidBody2D;

	public void OnHurt()
	{
		m_Damageable.EnableInvulnerability();

		m_Animator.SetTrigger("Hurt");
	}

	public void OnDie()
	{
		//Play Death animation
		m_Animator.SetTrigger("OnDeath");
		//Respawn Player
		StartCoroutine(DieRespawnCoroutine(false, true));
	}

	IEnumerator DieRespawnCoroutine(bool useCheckpoint, bool resetHealth)
	{
		yield return new WaitForSeconds(2.0f);
		Respawn(useCheckpoint, resetHealth);
		yield return new WaitForEndOfFrame();
	}

	void Respawn(bool useCheckpoint, bool resetHealth)
	{
		m_RigidBody2D.velocity = Vector2.one;
		m_Damageable.ResetHealth();

		m_Animator.SetTrigger("Idle");

		if (useCheckpoint)
		{
			gameObject.transform.position = m_ActiveCheckpoint.position;
			gameObject.transform.rotation = m_ActiveCheckpoint.rotation;
		}
		else
		{
			gameObject.transform.position = m_StartingPosition.position;
			gameObject.transform.rotation = m_StartingPosition.rotation;
		}
	}
}
