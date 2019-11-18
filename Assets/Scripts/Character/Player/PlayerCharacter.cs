using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	[Header("External Variables")]
	[SerializeField]
	private Transform m_ActiveCheckpoint;
	[SerializeField]
	private Transform m_StartingPosition;
	[SerializeField]
	private List<HealthIcon> m_HealthIcons;
	[Header("Player Variables")]
	[SerializeField]
	private Damageable m_Damageable;
	[SerializeField]
	private PlayerAnimations m_Animator;
	[SerializeField]
	private Rigidbody2D m_RigidBody2D;
	[SerializeField]
	private PlayerAudio m_PlayerAudio;
	[SerializeField]
	private PlayerController m_PlayerController;
	[SerializeField]
	private SpriteRenderer m_PlayerSprite;
	[SerializeField, Tooltip("The Collider2D used to detect interaction with Colliders")]
	private Collider2D m_InteractableCollider;
	[SerializeField, Tooltip("The layers which this Player can interact with")]
	private LayerMask m_InteractableLayers;

	private void Start()
	{
		if (!m_ActiveCheckpoint)
			Debug.LogWarning("No active checkpoint has been set for " + gameObject.name);
		if (!m_StartingPosition)
			Debug.LogError("No starting checkpoint has been set for " + gameObject.name);
		if (!m_Damageable)
			Debug.LogError("No Damageable has been assigned to " + gameObject.name);
		if (m_HealthIcons.Count == 0)
			Debug.LogError("No health icons have been assigned to " + gameObject.name);
		else if (m_HealthIcons.Count < m_Damageable.StartingHealth)
			Debug.LogWarning("The number of health icons assigned to " + gameObject.name + " are less that the starting health of " + m_Damageable.name);
		if (!m_Animator)
			Debug.LogError("No Player Animations script has been assigned to " + gameObject.name);
		if (!m_RigidBody2D)
			Debug.LogError("No RigidBody2D has been assigned to " + gameObject.name);
		if (!m_PlayerAudio)
			Debug.LogError("No Player Audio has been assigned to " + gameObject.name);
		if (!m_PlayerController)
			Debug.LogError("No Player Controller has been assigned to " + gameObject.name);
		if (!m_PlayerSprite)
			Debug.LogWarning("No Sprite has been asigned to " + gameObject.name);
		if (!m_InteractableCollider)
			Debug.LogError("No Interactable Collider has been set for " + gameObject.name);
	}

	public void OnHurt()
	{
		m_Damageable.EnableInvulnerability();

		DamageTaken();
	}

	public void OnDie()
	{
		//Play Death animation
		m_Animator.PlayerDead(true);
		m_PlayerController.DisableMovement();
		m_Damageable.EnableInvulnerability();
		m_PlayerAudio.PlayDeathAudioClip();
		//Respawn Player
		StartCoroutine(DieRespawnCoroutine(false, true));
	}

	public void OnRespawn(bool resetHealth)
	{
		Respawn(true, resetHealth);
	}

	public void SetColliders(Collider2D circle)
	{
		m_InteractableCollider = circle;
	}

	public void DamageTaken()
	{
		m_Animator.PlayerHurt();
		m_HealthIcons[m_Damageable.CurrentHealth()].TakeDamage();
		m_PlayerAudio.PlayHurtAudioClip();
		StartCoroutine(DamageTakenCoroutine());
	}

	//This function makes the character fade for 1 second, to show that they have taken damage
	IEnumerator DamageTakenCoroutine()
	{
		m_PlayerSprite.color = new Color(255f, 255f, 255f, 0.5f);
		yield return new WaitForSeconds(m_Damageable.InvulnerableDuration);
		m_PlayerSprite.color = new Color(255f, 255f, 255f, 1f);
	}

	public void RegainHealth()
	{
		m_HealthIcons[m_Damageable.CurrentHealth() - 1].GainHealth();
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
		yield return new WaitForSeconds(1.0f);
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
		m_PlayerController.EnableMovement();
		m_Animator.PlayerDead(false);
		m_Animator.PlayerIdle();
	}

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
			foreach(Collider2D col in interactableColliders)
			{
				if(col.GetComponent<InteractableObjects>())
				{
					col.GetComponent<InteractableObjects>().Interact();
				}
			}
		}
	}
}
