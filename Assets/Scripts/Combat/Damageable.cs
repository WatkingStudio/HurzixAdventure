using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
	[Serializable]
	public class HeathEvent : UnityEvent<Damageable>
	{ }

	[Serializable]
	public class DamageEvent : UnityEvent<Damager, Damageable>
	{ }

	[Serializable]
	public class HealEvent : UnityEvent<int, Damageable>
	{ }

	[SerializeField, Tooltip("The starting health of this character")]
	private int m_StartingHealth = 5;
	[SerializeField, Tooltip("True if after taking damage from a source this character should be invulverable for a duration")]
	private bool m_InvulverableAfterDamage = true;
	[SerializeField, Tooltip("The duration that the character will be invulnerable for")]
	private float m_InvulnerableDuration = 3f;
	[SerializeField, Tooltip("If true then when the character dies the gameobject will be disabled")]
	private bool m_DisableOnDeath = true;
	[SerializeField]
	private HeathEvent m_OnHeathSet;
	[SerializeField]
	private DamageEvent m_OnTakeDamage;
	[SerializeField]
	private DamageEvent m_OnDie;
	[SerializeField]
	private HealEvent m_OnGainHealth;

	private bool m_IsInvulnerable = false;
	private float m_InvulnerabilityTimer;
	public int m_CurrentHealth { get; private set; }

	//When this gameobject is enabled reset the starting health and make sure that they are not invulnerable.
	private void OnEnable()
	{
		m_CurrentHealth = m_StartingHealth;

		DisableInvulnerability();
	}

    void Update()
    {
		//If the character is invulnerable check to see if the timer has reached 0, if so make the character vulnerable.
		if (m_IsInvulnerable)
		{
			m_InvulnerabilityTimer -= Time.deltaTime;

			if (m_InvulnerabilityTimer <= 0f)
				DisableInvulnerability();
		}
	}

	public int CurrentHealth()
	{
		return m_CurrentHealth;
	}

	//If no health value is passed into the function reset health to the starting health
	public void ResetHealth(int health = -1)
	{
		if (health != -1)
			m_CurrentHealth = health;
		else
			m_CurrentHealth = m_StartingHealth;
	}

	public void EnableInvulnerability()
	{
		m_IsInvulnerable = true;

		m_InvulnerabilityTimer = m_InvulnerableDuration;
	}

	public void DisableInvulnerability()
	{
		m_IsInvulnerable = false;
	}

	public void TakeDamage(Damager damager, bool ignoreInvincible = false)
	{
		//If the character should not take damage at this point return
		if ((m_IsInvulnerable && !ignoreInvincible) || m_CurrentHealth <= 0)
			return;

		//If the character is not invulnerable apply damage to them and invoke the OnHealthSet event to trigger any subscribed functions
		if(!m_IsInvulnerable)
		{
			m_CurrentHealth -= damager.Damage;
			m_OnHeathSet.Invoke(this);
		}

		//Invoke the OnTakeDamage event to trigger any subscribed functions
		m_OnTakeDamage.Invoke(damager, this);

		if(m_CurrentHealth <= 0)
		{
			m_OnDie.Invoke(damager, this);
			EnableInvulnerability();
			if (m_DisableOnDeath)
				gameObject.SetActive(false);
		}
	}
}
