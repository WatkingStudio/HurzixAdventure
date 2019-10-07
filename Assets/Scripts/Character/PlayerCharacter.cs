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
	[SerializeField]
	private List<HealthIcon> m_HealthIcons;
	[SerializeField]
	private AudioSource m_AudioSource;
	[SerializeField]
	private PlayerMovement m_PlayerMovement;

	public void OnHurt()
	{
		m_Damageable.EnableInvulnerability();

		DamageTaken();
	}

	public void OnDie()
	{
		//Play Death animation
		m_Animator.SetBool("IsDead", true);
		m_PlayerMovement.DisableMovement();
		m_Damageable.EnableInvulnerability();
		//Respawn Player
		StartCoroutine(DieRespawnCoroutine(false, true));
	}

	public void DamageTaken()
	{
		m_Animator.SetTrigger("Hurt");
		m_HealthIcons[m_Damageable.CurrentHealth()].TakeDamage();
		m_AudioSource.Play();
	}

	public void RegainHealth()
	{
		m_HealthIcons[m_Damageable.CurrentHealth() + 1].GainHealth();
	}

	private void ResetHealth()
	{
		m_Damageable.ResetHealth();
		for (int i = 0; i < m_Damageable.CurrentHealth(); ++i)
		{
			m_HealthIcons[i].GainHealth();
		}
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
		ResetHealth();

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
		m_PlayerMovement.EnableMovement();
		m_Damageable.DisableInvulnerability();
		m_Animator.SetBool("IsDead", false);
		m_Animator.SetTrigger("Idle");
	}
}
