using UnityEngine;
using System;
using UnityEngine.Events;

/**
 * \class Damager
 * 
 * \brief This class is for any GameObject that can deal damage
 * 
 *  This class only holds the generic damager functions, if there are specific interactions with
 *   GameObject these should be called through the event system in the Inspector.
 * 
 * \date 2019/21/10
 */ 

public class Damager : MonoBehaviour
{
	[Serializable]
	public class DamagableEvent : UnityEvent<Damager, Damageable>
	{ }

	[Serializable]
	public class NonDamagableEvent : UnityEvent<Damager>
	{ }

	//The last collider hit
	public Collider2D LastHit { get { return m_LastHit; } }
	public int Damage { get { return m_Damage; } }

	[SerializeField, Tooltip("How much damage this object deals each hit")]
	private int m_Damage = 1;
	[Header("Collision Box")]
	[SerializeField, Tooltip("The offset of the collision box from the Game Object")]
	private Vector2 m_Offset = new Vector2(0f, 0f);
	[SerializeField, Tooltip("The size of the collision box")]
	private Vector2 m_Size = new Vector2(1f, 1f);
	[SerializeField, Tooltip("If set to true, the offset will take into account the facing of the sprite")]
	private bool m_OffsetBaseOnSpriteFacing = true;
	[Header("Damage Checks")]
	[SerializeField, Tooltip("If disabled, will ignore triggers when applying damage")]
	private bool m_CanHitTriggers;
	[SerializeField, Tooltip("If enabled, when this component has dealt damage this gameobject will be disabled")]
	private bool m_DisableDamageAfterHit = false;
	[SerializeField, Tooltip("If set causes the player to respawn from the latest checkpoint, in addition to losing a life")]
	private bool m_ForceRespawn = false;
	[SerializeField, Tooltip("If set, a invincible target will still receive the onHit message. But won't loose health")]
	private bool m_IgnoreInvincibility = false;
	[SerializeField, Tooltip("The layers which this Damager can interact with")]
	private LayerMask m_HittableLayers;
	[Header("Misc")]
	[SerializeField, Tooltip("SpriteRenderer used to read the flipX value used by m_OffsetBasedOnSpriteFacing")]
	private SpriteRenderer m_SpriteRenderer;
	[Header("Events")]
	[SerializeField]
	private DamagableEvent m_OnDamageableHit;
	[SerializeField]
	private NonDamagableEvent m_OnNonDamageableHit;

	// Set to whether the sprite was flipped by default
	protected bool m_SpriteOriginallyFlipped;
	// If set then this Damager can cause damage
	[SerializeField]
	protected bool m_CanDamage = true;
	// Filter to restrict what gameobjects this Damager interacts with
	protected ContactFilter2D m_AttackContactFilter;
	// An array to store the colliders found in a collision
	protected Collider2D[] m_AttackOverlapResults = new Collider2D[10];
	// The transform of the Damager
	protected Transform m_DamagerTransform;
	// The last collider this Damager collided with
	protected Collider2D m_LastHit;

	private void Start()
	{
		if (!m_SpriteRenderer)
			Debug.LogWarning("No Sprite Renderer has been assigned to " + gameObject.name);
	}

	private void Awake()
	{
		m_AttackContactFilter.layerMask = m_HittableLayers;
		m_AttackContactFilter.useLayerMask = true;
		m_AttackContactFilter.useTriggers = m_CanHitTriggers;

		if (m_OffsetBaseOnSpriteFacing && m_SpriteRenderer != null)
			m_SpriteOriginallyFlipped = m_SpriteRenderer.flipX;

		m_DamagerTransform = transform;
	}

	public void EnableDamage()
	{
		m_CanDamage = true;
	}

	public void DisableDamage()
	{
		m_CanDamage = false;
	}

	//Every frame this function will be called and check if this object has 
	// collided with any other object.
	private void FixedUpdate()
	{
		//If the object is set to not deal damage then simply return
		if (!m_CanDamage)
			return;
		
		//Create the collision area
		Vector2 scale = m_DamagerTransform.lossyScale;

		Vector2 facingOffset = Vector2.Scale(m_Offset, scale);
		if (m_OffsetBaseOnSpriteFacing && m_SpriteRenderer != null && m_SpriteRenderer.flipX != m_SpriteOriginallyFlipped)
			facingOffset = new Vector2(-m_Offset.x * scale.x, m_Offset.y * scale.y);

		Vector2 scaledSize = Vector2.Scale(m_Size, scale);

		Vector2 pointA = (Vector2)m_DamagerTransform.position + facingOffset - scaledSize * 0.5f;
		Vector2 pointB = pointA + scaledSize;

		//Check number of collisions
		int hitCount = Physics2D.OverlapArea(pointA, pointB, m_AttackContactFilter, m_AttackOverlapResults);

		for (int i = 0; i < hitCount; ++i)
		{
			//Store the latest collision hit
			m_LastHit = m_AttackOverlapResults[i];
			Damageable damageable = m_LastHit.GetComponent<Damageable>();
			
			//If the collided object has a Damageable component then deal damage
			if(damageable)
			{
				if (m_ForceRespawn)
				{
					damageable.RespawnTarget();
					break;
				}
				m_OnDamageableHit.Invoke(this, damageable);
				damageable.TakeDamage(this, m_IgnoreInvincibility);
				if (m_DisableDamageAfterHit)
					DisableDamage();
			}
			else
			{
				m_OnNonDamageableHit.Invoke(this);
			}
		}
	}

	private void OnDrawGizmos()
	{
		//Create the collision area
		Vector2 scale = transform.lossyScale;
		Vector2 scaledSize = Vector2.Scale(m_Size, scale);

		Gizmos.color = new Color(1f, 0f, 0f, 1f);
		Gizmos.DrawWireCube(transform.position + (Vector3)m_Offset, (Vector3)scaledSize);
	}

	private void OnDrawGizmosSelected()
	{
		Vector2 scale = transform.lossyScale;
		Vector2 scaledSize = Vector2.Scale(m_Size, scale);

		Gizmos.color = new Color(0f, 1f, 0f, 1f);
		Gizmos.DrawWireCube(transform.position + (Vector3)m_Offset, (Vector3)scaledSize);
	}
}
