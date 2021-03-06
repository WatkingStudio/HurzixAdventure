﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;

/** 
 * \class Player Character
 * 
 * \brief This class is used to handle any interactions between the game and character
 * 
 * \date 2019/13/10
 * 
 */
public class PlayerCharacter : MonoBehaviour
{
	[Serializable]
	public class ResetLevelEvent : UnityEvent<bool>
	{ }

	[Header("External Variables")]
	[SerializeField]
	private Transform m_ActiveCheckpoint;
	[SerializeField]
	private List<HealthIcon> m_HealthIcons;
	[SerializeField]
	private Transform m_StartingPosition;

	[Header("Player Variables")]
	[SerializeField]
	private PlayerAnimations m_Animator;
	[SerializeField]
	private Damageable m_Damageable;
	[SerializeField, Tooltip("The Collider2D used to detect interaction with Colliders")]
	private Collider2D m_InteractableCollider;
	[SerializeField, Tooltip("The layers which this Player can interact with")]
	private LayerMask m_InteractableLayers;
	[SerializeField]
	private PlayerAudio m_PlayerAudio;
	[SerializeField]
	private PlayerController m_PlayerController;
	[SerializeField]
	private SpriteRenderer m_PlayerSprite;
	[SerializeField]
	private Rigidbody2D m_RigidBody2D;

	[Header("Score")]
	[SerializeField]
	private PlayerGlobals m_PlayerGlobals;
	[SerializeField]
	private TMPro.TextMeshProUGUI m_ScoreText;

	[Header("Events")]
	[SerializeField]
	private ResetLevelEvent m_ResetLevelEvent;

	private int m_ActiveCheckpointID;
	private int m_PlayerScore;

	private void Start()
	{
		if (!m_ActiveCheckpoint)
		{
			Debug.LogWarning("No active checkpoint has been set for " + gameObject.name);
		}
		if (!m_StartingPosition)
		{
			Debug.LogError("No starting checkpoint has been set for " + gameObject.name);
		}
		if (!m_Damageable)
		{
			Debug.LogError("No Damageable has been assigned to " + gameObject.name);
		}
		if (m_HealthIcons.Count == 0)
		{
			Debug.LogError("No health icons have been assigned to " + gameObject.name);
		}
		else if (m_HealthIcons.Count < m_Damageable.StartingHealth)
		{
			Debug.LogWarning("The number of health icons assigned to " + gameObject.name + " are less that the starting health of " + m_Damageable.name);
		}
		if (!m_Animator)
		{
			Debug.LogError("No Player Animations script has been assigned to " + gameObject.name);
		}
		if (!m_RigidBody2D)
		{
			Debug.LogError("No RigidBody2D has been assigned to " + gameObject.name);
		}
		if (!m_PlayerAudio)
		{
			Debug.LogError("No Player Audio has been assigned to " + gameObject.name);
		}
		if (!m_PlayerController)
		{
			Debug.LogError("No Player Controller has been assigned to " + gameObject.name);
		}
		if (!m_PlayerSprite)
		{
			Debug.LogWarning("No Sprite has been asigned to " + gameObject.name);
		}
		if (!m_InteractableCollider)
		{
			Debug.LogError("No Interactable Collider has been set for " + gameObject.name);
		}
		if (!m_ScoreText)
		{
			Debug.LogWarning("No Score Text has been assigned to " + gameObject.name);
		}
		if (!m_PlayerGlobals)
		{
			Debug.LogError("No Player Globals have been assiged to " + gameObject.name);
		}

		m_PlayerScore = m_PlayerGlobals.PlayerScore;
		SetHealth();

		if (m_ScoreText)
		{
			m_ScoreText.SetText(m_PlayerScore.ToString());
		}
	}

	/// <summary>
	/// Perform code when a coin is collected.
	/// </summary>
	public void CollectCoin()
	{
		m_PlayerScore++;
		m_ScoreText.SetText(m_PlayerScore.ToString());
	}

	/// <summary>
	/// Perform code when damage is taken.
	/// </summary>
	public void DamageTaken()
	{
		m_Animator.PlayerHurt();
		m_HealthIcons[m_Damageable.CurrentHealth()].TakeDamage();
		m_PlayerAudio.PlayHurtAudioClip();
		if (m_Damageable.CurrentHealth() > 0)
		{
			StartCoroutine(DamageTakenCoroutine());
		}
	}

	/// <summary>
	/// This function makes the character fade for 1 second, to show that they have taken damage.
	/// </summary>
	/// <returns>The current ienumerator step.</returns>
	IEnumerator DamageTakenCoroutine()
	{
		float delay = m_Damageable.InvulnerableDuration / 7;

		for (int i = 0; i < 4; ++i)
		{
			m_PlayerSprite.color = new Color(255f, 255f, 255f, 0.2f);
			yield return new WaitForSeconds(delay);
			m_PlayerSprite.color = new Color(255f, 255f, 255f, 1f);
			yield return new WaitForSeconds(delay);
		}
	}

	/// <summary>
	/// Respawns the player after a period of time.
	/// </summary>
	/// <param name="useCheckpoint">Should a checkpoint be used.</param>
	/// <param name="resetHealth">Should the players health be reset.</param>
	/// <returns>The current ienumerator step.</returns>
	IEnumerator DieRespawnCoroutine(bool useCheckpoint, bool resetHealth)
	{
		yield return new WaitForSeconds(1.0f);
		Respawn(useCheckpoint, resetHealth);
		yield return new WaitForEndOfFrame();
	}

	/// <summary>
	/// Interact with a colliding object.
	/// </summary>
	public void InteractWithObject()
	{
		List<Collider2D> interactableColliders = new List<Collider2D>();
		ContactFilter2D contactFilter = new ContactFilter2D();
		contactFilter.layerMask = m_InteractableLayers;
		contactFilter.useLayerMask = true;
		contactFilter.useTriggers = true;
		Physics2D.OverlapCollider(m_InteractableCollider, contactFilter, interactableColliders);

		if (interactableColliders.Count > 0)
		{
			foreach (Collider2D col in interactableColliders)
			{
				if (col.GetComponent<InteractableObjects>())
				{
					col.GetComponent<InteractableObjects>().Interact();
				}
			}
		}
	}

	/// <summary>
	/// Execute this code when the player dies.
	/// </summary>
	public void OnDie()
	{
		//Play Death animation
		m_Animator.PlayerDead(true);
		m_PlayerController.DisableMovement();
		m_Damageable.EnableInvulnerability();
		m_PlayerAudio.PlayDeathAudioClip();
		//Respawn Player
		StartCoroutine(DieRespawnCoroutine(true, true));
	}

	/// <summary>
	/// Execute this code when the player is hurt.
	/// </summary>
	public void OnHurt()
	{
		m_Damageable.EnableInvulnerability();

		DamageTaken();
	}

	/// <summary>
	/// Execute this code when the player respawns.
	/// </summary>
	/// <param name="resetHealth">Should the players health be reset.</param>
	public void OnRespawn(bool resetHealth)
	{
		m_PlayerAudio.PlayHurtAudioClip();
		Respawn(true, resetHealth);
	}

	/// <summary>
	/// Update the ui to show the player has regained health.
	/// </summary>
	public void RegainHealth()
	{
		m_HealthIcons[m_Damageable.CurrentHealth() - 1].GainHealth();
	}

	/// <summary>
	/// Reset the players health.
	/// </summary>
	private void ResetHealth()
	{
		m_Damageable.ResetHealth();
		for (int i = 0; i < m_Damageable.CurrentHealth(); ++i)
		{
			m_HealthIcons[i].GainHealth();
		}
	}

	/// <summary>
	/// Respawn the player.
	/// </summary>
	/// <param name="useCheckpoint">Should a checkpoint be used.</param>
	/// <param name="resetHealth">Should the players health be reset.</param>
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
		m_PlayerController.EnableMovement();
		m_Animator.PlayerDead(false);
		m_Animator.PlayerIdle();

		m_ResetLevelEvent.Invoke(true);
	}

	/// <summary>
	/// Set the colliders for this player.
	/// </summary>
	/// <param name="circle">Set the collider to this collider.</param>
	public void SetColliders(Collider2D circle)
	{
		m_InteractableCollider = circle;
	}

	/// <summary>
	/// Set the health for this player.
	/// </summary>
	public void SetHealth()
	{
		m_Damageable.SetHealth(m_PlayerGlobals.PlayerHealth);

		int num = m_HealthIcons.Count;
		while(num != m_Damageable.CurrentHealth())
		{
			num--;
			m_HealthIcons[num].TakeDamage();
		}
	}

	/// <summary>
	/// Update the players checkpoint.
	/// </summary>
	/// <param name="newCheckpoint">The new checkpoint.</param>
	/// <param name="checkpointID">The id of the checkpoint.</param>
	public void UpdateCheckpoint(Transform newCheckpoint, int checkpointID)
	{
		if (checkpointID != m_ActiveCheckpointID)
		{
			m_ActiveCheckpoint = newCheckpoint;
			m_ActiveCheckpointID = checkpointID;
		}
	}

	/// <summary>
	/// Update the players global statistics.
	/// </summary>
	public void UpdatePlayerGlobals()
	{
		m_PlayerGlobals.PlayerHealth = m_Damageable.m_CurrentHealth;
		m_PlayerGlobals.PlayerScore = m_PlayerScore;
	}
}
