using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

/**
 * \class Damageable
 * 
 * \brief This class is for any GameObject that can be damaged.
 * 
 *  This class only holds the generic damageable functions, if there are specific interactions with
 *   GameObjects these should be called through the event system in the Inspector.
 * 
 * \date 2019/21/10
 * 
 */ 
public class Damageable : MonoBehaviour
{
	[Serializable]
	public class DamageEvent : UnityEvent<Damager, Damageable>
	{ }

	[Serializable]
	public class HeathEvent : UnityEvent<Damageable>
	{ }

	[Serializable]
	public class HealEvent : UnityEvent<int, Damageable>
	{ }

	[Serializable]
	public class RespawnEvent : UnityEvent<bool>
	{ }

	[SerializeField, Tooltip("The starting health of this character")]
	private int m_StartingHealth = 5;

	[Header("Damage Effects")]
	[SerializeField, Tooltip("If true then when the character dies the gameobject will be disabled")]
	private bool m_DisableOnDeath = true;
	[SerializeField, Tooltip("The duration that the healing buffer will last for")]
	private float m_HealingBuffer = 0.02f;
	[SerializeField, Tooltip("True if after taking damage from a source this character should be invulverable for a duration")]
	private bool m_InvulverableAfterDamage = true;
	[SerializeField, Tooltip("The duration that the character will be invulnerable for")]
	private float m_InvulnerableDuration = 3f;

	[Header("Events")]
	[SerializeField]
	private DamageEvent m_OnDie;
	[SerializeField]
	private HealEvent m_OnGainHealth;
	[SerializeField]
	private HeathEvent m_OnHeathSet;
	[SerializeField]
	private DamageEvent m_OnTakeDamage;
	[SerializeField]
	private RespawnEvent m_RespawnEvent;

	private bool m_HealingBufferActive = false;
	private float m_HealingBufferTimer;
	private float m_InvulnerabilityTimer;
	private bool m_IsInvulnerable = false;

	public int m_CurrentHealth { get; private set; }
	public int StartingHealth { get { return m_StartingHealth; }}

	public float InvulnerableDuration { get { return m_InvulnerableDuration; } }
	public float HealingBufferDuration { get { return m_HealingBuffer; } }

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
			{
				DisableInvulnerability();
			}
		}

		if(m_HealingBufferActive)
		{
			m_HealingBufferTimer -= Time.deltaTime;

			if (m_HealingBufferTimer <= 0f)
			{
				EnableHealing();
			}
		}
	}

	/// <summary>
	/// Get the current health.
	/// </summary>
	/// <returns>The current health of the object.</returns>
	public int CurrentHealth()
	{
		return m_CurrentHealth;
	}

	/// <summary>
	/// Disable healing for this object.
	/// </summary>
	public void DisableHealing()
	{
		m_HealingBufferActive = true;

		m_HealingBufferTimer = m_HealingBuffer;
	}

	/// <summary>
	/// Disable invulnerability for this object.
	/// </summary>
	public void DisableInvulnerability()
	{
		m_IsInvulnerable = false;
	}

	/// <summary>
	/// Enable healing for this object.
	/// </summary>
	public void EnableHealing()
	{
		m_HealingBufferActive = false;
	}

	/// <summary>
	/// Enable invulnerability for this object.
	/// </summary>
	public void EnableInvulnerability()
	{
		m_IsInvulnerable = true;

		m_InvulnerabilityTimer = m_InvulnerableDuration;
	}

	/// <summary>
	/// Check if this object is full health.
	/// </summary>
	/// <returns>True if the object is full health, false if not.</returns>
	public bool IsFullHealth()
	{
		if (m_CurrentHealth < m_StartingHealth)
		{
			return false;
		}
		else
		{
			return true;
		}
	}

	/// <summary>
	/// Regain one health.
	/// </summary>
	public void RegainHealth()
	{
		if (m_HealingBufferActive)
		{
			return;
		}

		if (m_CurrentHealth >= m_StartingHealth)
		{
			return;
		}

		m_CurrentHealth++;
		m_OnGainHealth.Invoke(1, this);
		DisableHealing();
	}

	/// <summary>
	/// Regain the specified amount of health.
	/// </summary>
	/// <param name="val">The amount of health to regain.</param>
	public void RegainHealth(int val)
	{
		if (m_HealingBufferActive)
		{
			return;
		}

		if (m_CurrentHealth >= m_StartingHealth)
		{
			return;
		}

		m_CurrentHealth += val;
		m_OnGainHealth.Invoke(val, this);
		DisableHealing();
	}

	/// <summary>
	/// Reset health.
	/// </summary>
	/// <param name="health">What to set the health to.</param>
	public void ResetHealth(int health = -1)
	{
		if (health != -1)
		{
			m_CurrentHealth = health;
		}
		else
		{
			m_CurrentHealth = m_StartingHealth;
		}
	}

	/// <summary>
	/// Respawn the object.
	/// </summary>
	/// <param name="resetHealth">Should the characters health be reset.</param>
	public void RespawnTarget(bool resetHealth = true)
	{
		m_RespawnEvent.Invoke(resetHealth);
	}

	/// <summary>
	/// Set the objects heath.
	/// </summary>
	/// <param name="val">The value to set the characters health to.</param>
	public void SetHealth(int val)
	{
		m_CurrentHealth = val;
	}

	/// <summary>
	/// Apply damage to this object.
	/// </summary>
	/// <param name="damager">Damager dealing damage to this object.</param>
	/// <param name="ignoreInvincible">Should invincibility be ignored.</param>
	public void TakeDamage(Damager damager, bool ignoreInvincible = false)
	{
		//If the character should not take damage at this point return
		if ((m_IsInvulnerable && !ignoreInvincible) || m_CurrentHealth <= 0)
		{
			return;
		}

		//If the character is not invulnerable apply damage to them and invoke the OnHealthSet event to trigger any subscribed functions
		if(!m_IsInvulnerable)
		{
			if (damager.Damage < m_CurrentHealth)
			{
				m_CurrentHealth -= damager.Damage;
			}
			else
			{
				m_CurrentHealth = 0;
			}
			m_OnHeathSet.Invoke(this);
		}

		//Invoke the OnTakeDamage event to trigger any subscribed functions
		m_OnTakeDamage.Invoke(damager, this);

		if(m_CurrentHealth <= 0)
		{
			m_OnDie.Invoke(damager, this);
			EnableInvulnerability();
			if (m_DisableOnDeath)
			{
				gameObject.SetActive(false);
			}
		}
	}
}
